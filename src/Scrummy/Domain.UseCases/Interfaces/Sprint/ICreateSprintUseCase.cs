using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Validators;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using SprintValidation = Scrummy.Domain.Core.Entities.Sprint.Validation;
using BacklogValidation = Scrummy.Domain.Core.Entities.SprintBacklog.Validation;

namespace Scrummy.Domain.UseCases.Interfaces.Sprint
{
    public class CreateSprintRequest : AuthorizedRequest
    {
        public CreateSprintRequest(string userId) : base(userId)
        {
        }

        public Identity ProjectId { get; set; }

        public string Name { get; set; }

        public Tuple<DateTime, DateTime> TimeSpan { get; set; }

        public string Goal { get; set; }

        public IEnumerable<Identity> Stories { get; set; }

        protected override void ValidateCore()
        {
            if (ProjectId.IsBlankIdentity())
                AddError(SprintValidation.ProjectErrorKey, SprintValidation.ProjectIsInvalid);

            if(!SprintValidation.ValidateName(Name))
                AddError(SprintValidation.NameErrorKey, SprintValidation.NameIsInvalidMessage);

            if(!SprintValidation.ValidateTimeSpan(TimeSpan))
                AddError(SprintValidation.TimeSpanErrorKey, SprintValidation.TimeSpanIsInvalidMessage);

            if(!SetValidator.ValidateItemsAreUnique(Stories))
                AddError(BacklogValidation.BacklogErrorKey, BacklogValidation.SprintBacklogContainsDuplicateItems);
        }
    }

    public class CreateSprintResponse : BaseResponse
    {
        public CreateSprintResponse(string message) : base(message)
        {
        }

        public Identity Id { get; set; }
    }

    public interface ICreateSprintUseCase
    {
        CreateSprintResponse Execute(CreateSprintRequest request);
    }
}
