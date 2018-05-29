using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Project
{
    public class ViewTeamHistoryRequest : AuthorizedRequest
    {
        public ViewTeamHistoryRequest(string userId) : base(userId)
        {
        }

        public Identity Id { get; set; }

        protected override void ValidateCore()
        {
            if (Id.IsBlankIdentity())
                AddError("", "Idenitity is invalid.");
        }
    }

    public class ViewTeamHistoryResponse : BaseResponse
    {
        public class Team
        {
            public Identity Id { get; set; }

            public DateTime From { get; set; }

            public DateTime To { get; set; }
        }


        public ViewTeamHistoryResponse() : base(null)
        {
        }

        public NavigationInfo Project { get; set; }

        public IEnumerable<Team> Teams { get; set; }
    }

    public interface IViewTeamHistoryUseCase
    {
        ViewTeamHistoryResponse Execute(ViewTeamHistoryRequest request);
    }
}
