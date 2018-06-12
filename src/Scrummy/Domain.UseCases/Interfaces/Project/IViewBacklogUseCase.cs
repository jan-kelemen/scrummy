using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.Repositories.Interfaces.DTO;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Project
{
    public class ViewBacklogRequest : AuthorizedRequest
    {
        public ViewBacklogRequest(string userId) : base(userId)
        {
        }

        public Identity ProjectId { get; set; }

        public Func<ProductBacklog.WorkTaskStatus, bool> Include { get; set; } =
            status => status == ProductBacklog.WorkTaskStatus.ToDo || status == ProductBacklog.WorkTaskStatus.Ready;

        protected override void ValidateCore()
        {
            if (ProjectId.IsBlankIdentity())
            {
                AddError("", "Project idenitity is invalid.");
            }
        }
    }

    public class ViewBacklogResponse : BaseResponse
    {
        public class Task
        {
            public NavigationInfo ParentTask { get; set; }

            public Identity Id { get; set; }

            public ProductBacklog.WorkTaskStatus Status { get; set; }

            public WorkTaskType Type { get; set; }

            public string Name { get; set; }

            public int? StoryPoints { get; set; }
        }

        public ViewBacklogResponse() : base(null)
        {
        }

        public Identity ProjectId { get; set; }

        public IEnumerable<Task> Tasks { get; set; }

        public bool CanManageBacklog { get; set; }
    }

    public interface IViewBacklogUseCase
    {
        ViewBacklogResponse Execute(ViewBacklogRequest request);
    }
}
