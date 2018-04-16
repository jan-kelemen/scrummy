using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Core.Exceptions;

namespace Scrummy.Domain.Core.Validators.Entities
{
    public static class ProjectValidator
    {
        public const string NameErrorKey = nameof(Project.Name);
        public const int NameMinLength = 1;
        public const int NameMaxLength = 200;
        public const string NameIsInvalidMessage = "Project name is invalid.";

        public const string TeamErrorKey = nameof(Project.Team);
        public const string TeamIsInvalidMessage = "Team has to have one product owner, one scrum master and at least one developer.";
        public const string ProductOwnerIsInvalidMessage = "Team has to have one product owner.";
        public const string ScrumMasterIsInvalidMessage = "Team has to have one scrum master.";
        public const string DevelopersAreInvalidMessage = "Team has to have at least one developer.";
        public const string ProductOwnerDoesntHaveUniqueRoleMessage = "Product owner has to have a unique role in the team.";

        public const string DefinitionOfDoneErrorKey = nameof(Project.DefinitionOfDone);
        public const int DefinitionOfDoneItemMinLength = 1;
        public const int DefinitionOfDoneItemMaxLength = 400;
        public const string DefinitionOfDoneConditionIsInvalid = "Condition in Definition of done is invalid.";
        public const string DefinitionOfDoneIsInvalid = "Definition of done has to have at least one condition.";

        public static bool ValidateName(string name) =>
            TextValidator.CheckIfContentIsBetweenSpecifiedLength(name, NameMinLength, NameMaxLength);

        public static void CheckName(Identity projectIdentity, string name)
        {
            if (!ValidateName(name)) throw CreateException(projectIdentity, NameErrorKey, NameIsInvalidMessage);
        }

        public static bool ValidateDefinitionOfDoneConditions(IEnumerable<string> conditions)
        {
            var temp = conditions.ToArray();
            return temp.Length != 0 && temp.All(ValidateDefinitionOfDoneCondition);
        }

        public static void CheckDefinitionOfDoneConditions(Identity projectIdentity, IEnumerable<string> conditions)
        {
            var temp = conditions.ToArray();
            if(temp.Length == 0) throw CreateException(projectIdentity, DefinitionOfDoneErrorKey, DefinitionOfDoneIsInvalid);

            if (temp.Any(condition => !ValidateDefinitionOfDoneCondition(condition)))
            {
                throw CreateException(projectIdentity, DefinitionOfDoneErrorKey, DefinitionOfDoneConditionIsInvalid);
            }
        }

        public static bool ValidateTeamMembers(IEnumerable<Team.Member> members)
        {
            var temp = members.ToArray();
            if (!ValidateThatTeamHasOneProductOwner(temp)) { return false; }
            if (!ValidateThatTeamHasOneScrumMaster(temp)) { return false; }
            if (!ValidateThatTeamHasAtLeastOneDeveloper(temp)) { return false; }

            var productOwnerIdentity = temp.First(m => m.Role == PersonRole.ProductOwner).Id;

            return ValidateThatProductOwnerHasUniqueRole(temp, productOwnerIdentity);
        }

        public static void CheckTeamMembers(Identity projectIdentity, IEnumerable<Team.Member> members)
        {
            var temp = members.ToArray();
            if (!ValidateThatTeamHasOneProductOwner(temp))
            {
                throw CreateException(projectIdentity, TeamErrorKey, ProductOwnerIsInvalidMessage);
            }

            if (!ValidateThatTeamHasOneScrumMaster(temp))
            {
                throw CreateException(projectIdentity, TeamErrorKey, ScrumMasterIsInvalidMessage);
            }

            if (!ValidateThatTeamHasAtLeastOneDeveloper(temp))
            {
                throw CreateException(projectIdentity, TeamErrorKey, DevelopersAreInvalidMessage);
            }

            var productOwnerIdentity = temp.First(m => m.Role == PersonRole.ProductOwner).Id;
            if (!ValidateThatProductOwnerHasUniqueRole(temp, productOwnerIdentity))
            {
                throw CreateException(projectIdentity, TeamErrorKey, ProductOwnerDoesntHaveUniqueRoleMessage);
            }
        }

        private static EntityValidationException CreateException(Identity projectIdentity, string errorKey, string message, params object[] values)
        {
            return new EntityValidationException
            {
                EntityName = nameof(Project),
                Identity = projectIdentity,
                ErrorKey = errorKey,
                ErrorMessage = string.Format(message, values),
            };
        }

        private static bool ValidateDefinitionOfDoneCondition(string condition) =>
            TextValidator.CheckIfContentIsBetweenSpecifiedLength(condition, DefinitionOfDoneItemMinLength, DefinitionOfDoneItemMaxLength);

        private static bool ValidateThatTeamHasOneProductOwner(IEnumerable<Team.Member> members) => 
            members.Count(m => m.Role == PersonRole.ProductOwner) == 1;

        private static bool ValidateThatTeamHasOneScrumMaster(IEnumerable<Team.Member> members) => 
            members.Count(m => m.Role == PersonRole.ScrumMaster) == 1;

        private static bool ValidateThatTeamHasAtLeastOneDeveloper(IEnumerable<Team.Member> members) => 
            members.Count(m => m.Role == PersonRole.Developer) >= 1;

        private static bool ValidateThatProductOwnerHasUniqueRole(IEnumerable<Team.Member> members, Identity productOwnerIdentity) => 
            members.Count(m => m.Id == productOwnerIdentity) == 1;
    }
}
