using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Project
{
    public class PlanBacklogRequest : AuthorizedRequest
    {
        public class BacklogItem
        {
            public Identity Id { get; set; }

            public Identity Sprint { get; set; }
        }

        public PlanBacklogRequest(string userId) : base(userId)
        {
        }

        public Identity ProjectId { get; set; }

        public IList<BacklogItem> BacklogItems { get; set; }

        protected override void ValidateCore()
        {
            if (ProjectId.IsBlankIdentity())
            {
                AddError("", "Project idenitity is invalid.");
            }
        }
    }

    public class PlanBacklogResponse : BaseResponse
    {
        public PlanBacklogResponse(string message) : base(message)
        {
        }

        public Identity ProjectId { get; set; }
    }

    public interface IPlanBacklogUseCase
    {
        PlanBacklogResponse Execute(PlanBacklogRequest request);
    }
}
