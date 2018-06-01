using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Validators;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using MeetingValidation = Scrummy.Domain.Core.Entities.Meeting.Validation;

namespace Scrummy.Domain.UseCases.Interfaces.Meeting
{
    public class EditMeetingRequest : AuthorizedRequest
    {
        public EditMeetingRequest(string userId) : base(userId)
        {
        }

        public Identity Id { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public TimeSpan Duration { get; set; }

        public string Description { get; set; }

        public string Log { get; set; }

        public IEnumerable<Identity> InvolvedPersons { get; set; }

        public IEnumerable<Identity> Documents { get; set; }

        protected override void ValidateCore()
        {
            if (Id.IsBlankIdentity())
                AddError("", "Meeting identity is invalid.");

            if (!SetValidator.ValidateItemsAreUnique(InvolvedPersons))
                AddError(MeetingValidation.InvolvedPersonsErrorKey, MeetingValidation.InvolvedPersonsAreInvalidMessage);

            if(!MeetingValidation.ValidateName(Name))
                AddError(MeetingValidation.NameErrorKey, MeetingValidation.NameIsInvalidMessage);
        }
    }

    public interface IEditMeetingUseCase
    {
        ConfirmationResponse Execute(EditMeetingRequest request);
    }
}
