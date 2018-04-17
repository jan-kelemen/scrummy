using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Core.Utilities;

namespace Scrummy.Domain.Core.Entities
{
    public class Team : IEnumerable<Team.Member>
    {
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

        private List<Member> _members;

        public Team(IEnumerable<Member> members)
        {
            Members = members;
        }

        public Identity ProjectId { get; internal set; } = Identity.BlankIdentity;

        public IEnumerable<Member> Members
        {
            get => _members;
            private set => _members = CheckTeamMembers(value);
        }

        public Identity GetProductOwnerIdentity() => _members.First(m => m.Role == PersonRole.ProductOwner).Id;

        public Identity GetScrumMasterIdentity() => _members.First(m => m.Role == PersonRole.ScrumMaster).Id;

        public IEnumerable<Identity> GetDevelopersIdentities() => _members.Where(m => m.Role == PersonRole.Developer).Select(m => m.Id);

        private List<Member> CheckTeamMembers(IEnumerable<Member> members)
        {
            var temp = members.ToList();
            if (!Project.Validation.ValidateThatTeamHasOneProductOwner(temp))
                throw ExceptionUtility.CreateEntityValidationException<Project>(ProjectId, Project.Validation.TeamErrorKey, Project.Validation.ProductOwnerIsInvalidMessage);

            if (!Project.Validation.ValidateThatTeamHasOneScrumMaster(temp))
                throw ExceptionUtility.CreateEntityValidationException<Project>(ProjectId, Project.Validation.TeamErrorKey, Project.Validation.ScrumMasterIsInvalidMessage);

            if (!Project.Validation.ValidateThatTeamHasAtLeastOneDeveloper(temp))
                throw ExceptionUtility.CreateEntityValidationException<Project>(ProjectId, Project.Validation.TeamErrorKey, Project.Validation.DevelopersAreInvalidMessage);

            var productOwnerIdentity = temp.First(m => m.Role == PersonRole.ProductOwner).Id;
            if (!Project.Validation.ValidateThatProductOwnerHasUniqueRole(temp, productOwnerIdentity))
                throw ExceptionUtility.CreateEntityValidationException<Project>(ProjectId, Project.Validation.TeamErrorKey, Project.Validation.ProductOwnerDoesntHaveUniqueRoleMessage);

            return temp;
        }

        public IEnumerator<Member> GetEnumerator() => _members.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
