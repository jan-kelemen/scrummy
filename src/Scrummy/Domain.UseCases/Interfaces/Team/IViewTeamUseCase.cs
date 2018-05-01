using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Team
{
    public class ViewTeamRequest : AuthorizedRequest
    {
        public ViewTeamRequest(string userId) : base(userId)
        {
        }

        public Identity Id { get; set; }

        protected override void ValidateCore()
        {
            if (Id.IsBlankIdentity())
            {
                AddError("", "Idenitity is invalid.");
            }
        }
    }

    public class ViewTeamResponse : BaseResponse
    {
        public ViewTeamResponse() : base(null)
        {
        }

        public Identity Id { get; set; }

        public string Name { get; set; }

        public TimeSpan TimeOfDailyScrum { get; set; }

        public IEnumerable<Core.Entities.Team.Member> Members { get; set; }
    }

    public interface IViewTeamUseCase
    {
        ViewTeamResponse Execute(ViewTeamRequest request);
    }
}
