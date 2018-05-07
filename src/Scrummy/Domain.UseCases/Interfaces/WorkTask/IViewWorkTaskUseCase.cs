using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.WorkTask
{
    public class ViewWorkTaskRequest : AuthorizedRequest
    {
        public ViewWorkTaskRequest(string userId) : base(userId)
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

    public class ViewWorkTaskResponse : BaseResponse
    {
        public ViewWorkTaskResponse() : base(null)
        {
        }

        public Identity Id { get; set; }

        public Identity ProjectId { get; set; }

        public string Name { get; set; }

        public WorkTaskType Type { get; set; }

        public int? StoryPoints { get; set; }

        public string Description { get; set; }

        public Identity ParentTask { get; set; }

        public IEnumerable<Identity> ChildTasks { get; set; }
    }

    public interface IViewWorkTaskUseCase
    {
        ViewWorkTaskResponse Execute(ViewWorkTaskRequest request);
    }
}
