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
    public class EditSprintRequest : AuthorizedIdRequest
    {
        public EditSprintRequest(string userId) : base(userId)
        {
        }

        public string Name { get; set; }

        public Tuple<DateTime, DateTime> TimeSpan { get; set; }

        public string Goal { get; set; }

        public IEnumerable<Identity> Stories { get; set; }

        public IEnumerable<Identity> Documents { get; set; }

        protected override void ValidateCore()
        {

            base.ValidateCore();
            if (!SprintValidation.ValidateName(Name))
                AddError(SprintValidation.NameErrorKey, SprintValidation.NameIsInvalidMessage);

            if (!SprintValidation.ValidateTimeSpan(TimeSpan))
                AddError(SprintValidation.TimeSpanErrorKey, SprintValidation.TimeSpanIsInvalidMessage);

            if (!SetValidator.ValidateItemsAreUnique(Stories))
                AddError(BacklogValidation.BacklogErrorKey, BacklogValidation.SprintBacklogContainsDuplicateItems);
        }
    }

    public interface IEditSprintUseCase
    {
        ConfirmationResponse Execute(EditSprintRequest request);
    }
}
