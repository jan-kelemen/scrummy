using System.Collections.Generic;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Project
{
    public class ManageBacklogRequest : AuthorizedRequest
    {
        public class BacklogItem
        {
            public Identity Id { get; set; }

            public ProductBacklog.WorkTaskStatus Status { get; set; }
        }

        public ManageBacklogRequest(string userId) : base(userId)
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

    public class ManageBacklogResponse : BaseResponse
    {
        public ManageBacklogResponse(string message) : base(message)
        {
        }

        public Identity ProjectId { get; set; }
    }

    public interface IManageBacklogUseCase
    {
        ManageBacklogResponse Execute(ManageBacklogRequest request);
    }
}
