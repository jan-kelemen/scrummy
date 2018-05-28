using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Team
{
    public class ViewProjectHistoryRequest : AuthorizedRequest
    {
        public ViewProjectHistoryRequest(string userId) : base(userId)
        {
        }

        public Identity Id { get; set; }

        protected override void ValidateCore()
        {
            if (Id.IsBlankIdentity())
                AddError("", "Idenitity is invalid.");
        }
    }

    public class ViewProjectHistoryResponse : BaseResponse
    {
        public class Project
        {
            public Identity Id { get; set; }

            public DateTime From { get; set; }

            public DateTime To { get; set; }
        }


        public ViewProjectHistoryResponse() : base(null)
        {
        }

        public NavigationInfo Team { get; set; }

        public IEnumerable<Project> Projects { get; set; }
    }

    public interface IViewProjectHistoryUseCase
    {
        ViewProjectHistoryResponse Execute(ViewProjectHistoryRequest request);
    }
}
