using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using WorkTaskValidation = Scrummy.Domain.Core.Entities.WorkTask.Validation;

namespace Scrummy.Domain.UseCases.Interfaces.WorkTask
{
    public class EditWorkTaskRequest : AuthorizedRequest
    {
        public EditWorkTaskRequest(string userId) : base(userId)
        {
        }

        public Identity Id { get; set; }

        public string Name { get; set; }

        public int? StoryPoints { get; set; }

        public string Description { get; set; }

        public Identity ParentTask { get; set; }

        public IEnumerable<Identity> ChildTasks { get; set; }

        public IEnumerable<string> Steps { get; set; }

        public IEnumerable<Identity> Documents { get; set; }

        protected override void ValidateCore()
        {
            if (!WorkTaskValidation.ValidateName(Name))
                AddError(WorkTaskValidation.NameErrorKey, WorkTaskValidation.NameIsInvalidMessage);

            if (!WorkTaskValidation.ValidateStoryPoints(StoryPoints))
                AddError(WorkTaskValidation.StoryPointsErrorKey, WorkTaskValidation.StoryPointsAreInvalidMessage);

            if (!WorkTaskValidation.ValidateChildTasks(ChildTasks))
                AddError(WorkTaskValidation.LinkErrorKey, WorkTaskValidation.LinksAreDuplicatedMessage);
        }
    }

    public interface IEditWorkTaskUseCase
    {
        ConfirmationResponse Execute(EditWorkTaskRequest request);
    }
}
