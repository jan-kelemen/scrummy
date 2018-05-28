using System;
using System.Collections.Generic;
using System.Text;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Validators;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using MeetingValidation = Scrummy.Domain.Core.Entities.Meeting.Validation;

namespace Scrummy.Domain.UseCases.Interfaces.Meeting
{
    public class CreateMeetingRequest : AuthorizedRequest
    {
        public CreateMeetingRequest(string userId) : base(userId)
        {
        }

        public Identity OrganizedBy { get; set; }

        public Identity ProjectId { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public TimeSpan Duration { get; set; }

        public string Description { get; set; }

        public string Log { get; set; }

        public IEnumerable<Identity> InvolvedPersons { get; set; }

        protected override void ValidateCore()
        {
            if(OrganizedBy.IsBlankIdentity())
                AddError("", "User identity is invalid.");

            if(ProjectId.IsBlankIdentity())
                AddError("", "Project identity is invalid.");

            if(!SetValidator.ValidateItemsAreUnique(InvolvedPersons))
                AddError(MeetingValidation.InvolvedPersonsErrorKey, MeetingValidation.InvolvedPersonsAreInvalidMessage);

            if(!MeetingValidation.ValidateName(Name))
                AddError(MeetingValidation.NameErrorKey, MeetingValidation.NameIsInvalidMessage);
        }
    }

    public interface ICreateMeetingUseCase
    {
        ConfirmationResponse Execute(CreateMeetingRequest request);
    }
}
