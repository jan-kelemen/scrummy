using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Core.Validators.Entities;

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

        public Identity ProjectIdentity { get; internal set; } = Identity.BlankIdentity;

        public IEnumerable<Member> Members
        {
            get => _members;
            private set
            {
                var temp = value.ToList();
                ProjectValidator.CheckTeamMembers(ProjectIdentity, temp);
                _members = temp;
            }
        }

        public Identity GetProductOwnerIdentity() => _members.First(m => m.Role == PersonRole.ProductOwner).Id;

        public Identity GetScrumMasterIdentity() => _members.First(m => m.Role == PersonRole.ScrumMaster).Id;

        public IEnumerable<Identity> GetDevelopersIdentities() => 
            _members.Where(m => m.Role == PersonRole.Developer).Select(m => m.Id);

        public IEnumerator<Member> GetEnumerator()
        {
            return _members.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
