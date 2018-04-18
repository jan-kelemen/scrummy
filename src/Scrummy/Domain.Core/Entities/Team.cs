using System;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Core.Validators;

namespace Scrummy.Domain.Core.Entities
{
    public class Team : Entity<Team>
    {
        public static class Validation
        {
            public const string NameErrorKey = nameof(Name);
            public const int NameMinLength = 1;
            public const int NameMaxLength = 200;
            public const string NameIsInvalidMessage = "Team name is invalid.";

            public const string MembersErrorKey = nameof(Members);
            public const string TeamMembersAreInvalidMessage = "Team has to have one product owner, one scrum master and at least one developer.";
            public const string ProductOwnerIsInvalidMessage = "Team has to have one product owner.";
            public const string ScrumMasterIsInvalidMessage = "Team has to have one scrum master.";
            public const string DevelopersAreInvalidMessage = "Team has to have at least one developer.";
            public const string ProductOwnerDoesntHaveUniqueRoleMessage = "Product owner has to have a unique role in the team.";

            public static bool ValidateName(string name) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(name, NameMinLength, NameMaxLength);

            public static bool ValidateTeamMembers(IEnumerable<Member> members)
            {
                var temp = members.ToArray();
                if (!ValidateThatTeamHasOneProductOwner(temp)) { return false; }
                if (!ValidateThatTeamHasOneScrumMaster(temp)) { return false; }
                if (!ValidateThatTeamHasAtLeastOneDeveloper(temp)) { return false; }

                var productOwnerIdentity = temp.First(m => m.Role == PersonRole.ProductOwner).Id;

                return ValidateThatProductOwnerHasUniqueRole(temp, productOwnerIdentity);
            }

            public static bool ValidateThatTeamHasOneProductOwner(IEnumerable<Member> members) =>
                members.Count(m => m.Role == PersonRole.ProductOwner) == 1;

            public static bool ValidateThatTeamHasOneScrumMaster(IEnumerable<Member> members) =>
                members.Count(m => m.Role == PersonRole.ScrumMaster) == 1;

            public static bool ValidateThatTeamHasAtLeastOneDeveloper(IEnumerable<Member> members) =>
                members.Count(m => m.Role == PersonRole.Developer) >= 1;

            public static bool ValidateThatProductOwnerHasUniqueRole(IEnumerable<Member> members, Identity productOwnerIdentity) =>
                members.Count(m => m.Id == productOwnerIdentity) == 1;
        }

        public class Member
        {
            public Member(Identity id, PersonRole role)
            {
                if(id.IsBlankIdentity()) throw new Exception();

                Id = id;
                Role = role;
            }

            public Identity Id { get; }

            public PersonRole Role { get; }
        }

        private string _name;

        private List<Member> _members;

        public Team(
            Identity id, 
            string name, 
            TimeSpan timeOfDailyScrum, 
            IEnumerable<Member> members) : base(id)
        {
            Name = name;
            TimeOfDailyScrum = timeOfDailyScrum;
            Members = members;
        }

        public string Name
        {
            get => _name;
            set => _name = CheckName(value);
        }

        public TimeSpan TimeOfDailyScrum { get; set; }

        public Identity ProductOwner => _members.First(m => m.Role == PersonRole.ProductOwner).Id;

        public Identity ScrumMaster => _members.First(m => m.Role == PersonRole.ScrumMaster).Id;

        public IEnumerable<Identity> Developers => _members.Where(m => m.Role == PersonRole.Developer).Select(m => m.Id);

        public IEnumerable<Member> Members
        {
            get => _members;
            private set => _members = CheckTeamMembers(value);
        }

        private string CheckName(string name)
        {
            if (!Validation.ValidateName(name))
                throw CreateEntityValidationException(Validation.NameErrorKey, Validation.NameIsInvalidMessage);
            return name;
        }

        private List<Member> CheckTeamMembers(IEnumerable<Member> members)
        {
            var temp = members.ToList();
            if (!Validation.ValidateThatTeamHasOneProductOwner(temp))
                throw CreateEntityValidationException(Validation.MembersErrorKey, Validation.ProductOwnerIsInvalidMessage);

            if (!Validation.ValidateThatTeamHasOneScrumMaster(temp))
                throw CreateEntityValidationException(Validation.MembersErrorKey, Validation.ScrumMasterIsInvalidMessage);

            if (!Validation.ValidateThatTeamHasAtLeastOneDeveloper(temp))
                throw CreateEntityValidationException(Validation.MembersErrorKey, Validation.DevelopersAreInvalidMessage);

            var productOwnerIdentity = temp.First(m => m.Role == PersonRole.ProductOwner).Id;
            if (!Validation.ValidateThatProductOwnerHasUniqueRole(temp, productOwnerIdentity))
                throw CreateEntityValidationException(Validation.MembersErrorKey, Validation.ProductOwnerDoesntHaveUniqueRoleMessage);

            return temp;
        }
    }
}
