using Scrummy.Domain.UseCases.Boundary.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.UseCases.Boundary.Responses;

using WorkTaskValidation = Scrummy.Domain.Core.Entities.WorkTask.Validation;

namespace Scrummy.Domain.UseCases.Interfaces.WorkTask
{
    public class CreateWorkTaskRequest : AuthorizedRequest
    {
        public CreateWorkTaskRequest(string userId) : base(userId)
        {
        }

        public Identity ProjectId { get; set; }

        public string Name { get; set; }

        public WorkTaskType Type { get; set; }

        public int? StoryPoints { get; set; }

        public string Description { get; set; }

        public Identity ParentTask { get; set; }

        public IEnumerable<Identity> ChildTasks { get; set; }

        public IEnumerable<string> Steps { get; set; }

        protected override void ValidateCore()
        {
            if(ProjectId.IsBlankIdentity())
                AddError("", "Project identity is invalid.");

            if(!WorkTaskValidation.ValidateName(Name))
                AddError(WorkTaskValidation.NameErrorKey, WorkTaskValidation.NameIsInvalidMessage);

            if(!WorkTaskValidation.ValidateStoryPoints(StoryPoints))
                AddError(WorkTaskValidation.StoryPointsErrorKey, WorkTaskValidation.StoryPointsAreInvalidMessage);

            if(!WorkTaskValidation.ValidateChildTasks(ChildTasks))
                AddError(WorkTaskValidation.LinkErrorKey, WorkTaskValidation.LinksAreDuplicatedMessage);
        }
    }

    public interface ICreateWorkTaskUseCase
    {
        ConfirmationResponse Execute(CreateWorkTaskRequest request);
    }
}
