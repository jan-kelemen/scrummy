using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Core.Validators;

namespace Scrummy.Domain.Core.Entities
{
    public class Project : Entity<Project>
    {
        public static class Validation
        {
            public const string NameErrorKey = nameof(Name);
            public const int NameMinLength = 1;
            public const int NameMaxLength = 200;
            public const string NameIsInvalidMessage = "Project name is invalid.";

            public const string TeamErrorKey = nameof(Team);
            public const string TeamIsInvalidMessage = "Team has to have one product owner, one scrum master and at least one developer.";
            public const string ProductOwnerIsInvalidMessage = "Team has to have one product owner.";
            public const string ScrumMasterIsInvalidMessage = "Team has to have one scrum master.";
            public const string DevelopersAreInvalidMessage = "Team has to have at least one developer.";
            public const string ProductOwnerDoesntHaveUniqueRoleMessage = "Product owner has to have a unique role in the team.";

            public const string DefinitionOfDoneErrorKey = nameof(DefinitionOfDone);
            public const int DefinitionOfDoneItemMinLength = 1;
            public const int DefinitionOfDoneItemMaxLength = 400;
            public const string DefinitionOfDoneConditionIsInvalid = "Condition in Definition of done is invalid.";
            public const string DefinitionOfDoneIsInvalid = "Definition of done has to have at least one condition.";

            public static bool ValidateName(string name) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(name, NameMinLength, NameMaxLength);

            public static bool ValidateDefinitionOfDoneConditions(IEnumerable<string> conditions)
            {
                var temp = conditions.ToArray();
                return temp.Length != 0 && temp.All(ValidateDefinitionOfDoneCondition);
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

            public static bool ValidateDefinitionOfDoneCondition(string condition) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(condition, DefinitionOfDoneItemMinLength, DefinitionOfDoneItemMaxLength);

            public static bool ValidateThatTeamHasOneProductOwner(IEnumerable<Team.Member> members) =>
                members.Count(m => m.Role == PersonRole.ProductOwner) == 1;

            public static bool ValidateThatTeamHasOneScrumMaster(IEnumerable<Team.Member> members) =>
                members.Count(m => m.Role == PersonRole.ScrumMaster) == 1;

            public static bool ValidateThatTeamHasAtLeastOneDeveloper(IEnumerable<Team.Member> members) =>
                members.Count(m => m.Role == PersonRole.Developer) >= 1;

            public static bool ValidateThatProductOwnerHasUniqueRole(IEnumerable<Team.Member> members, Identity productOwnerIdentity) =>
                members.Count(m => m.Id == productOwnerIdentity) == 1;
        }

        private string _name;

        private DefinitionOfDone _definitionOfDone;

        private Team _team;

        public Project(
            Identity id, 
            string name, 
            DefinitionOfDone definitionOfDone, 
            Team team) : base(id)
        {
            definitionOfDone.ProjectId = id;
            team.ProjectId = id;

            Name = name;
        }

        public string Name
        {
            get => _name;
            set => _name = CheckName(value);
        }

        public DefinitionOfDone DefinitionOfDone
        {
            get => _definitionOfDone;
            set => _definitionOfDone = CheckReferenceNotNull(value, Validation.DefinitionOfDoneErrorKey);
        }

        public Team Team
        {
            get => _team;
            set => _team = CheckReferenceNotNull(value, Validation.TeamErrorKey);
        }

        private string CheckName(string name)
        {
            if (!Validation.ValidateName(name))
                throw CreateEntityValidationException(Validation.NameErrorKey, Validation.NameIsInvalidMessage);
            return name;
        }
    }
}
