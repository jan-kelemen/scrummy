using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Team
{
    public class ViewMemberHistoryRequest : AuthorizedRequest
    {
        public ViewMemberHistoryRequest(string userId) : base(userId)
        {
        }

        public Identity Id { get; set; }

        protected override void ValidateCore()
        {
            if (Id.IsBlankIdentity())
                AddError("", "Idenitity is invalid.");
        }
    }

    public class ViewMemberHistoryResponse : BaseResponse
    {
        public class Member
        {
            public Identity Id { get; set; }

            public PersonRole Role { get; set; }
        }

        public class TeamMembers : IEnumerable<Member>
        {
            private readonly List<Member> _members;

            public TeamMembers(IEnumerable<Member> members)
            {
                _members = members.ToList();
            }

            public DateTime From { get; set; }

            public DateTime To { get; set; }

            public IEnumerator<Member> GetEnumerator() => _members.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


        public ViewMemberHistoryResponse() : base(null)
        {
        }

        public NavigationInfo Team { get; set; }

        public IEnumerable<TeamMembers> Members { get; set; }
    }

    public interface IViewMemberHistoryUseCase
    {
        ViewMemberHistoryResponse Execute(ViewMemberHistoryRequest request);
    }
}
