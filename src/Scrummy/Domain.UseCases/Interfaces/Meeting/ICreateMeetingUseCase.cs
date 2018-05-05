﻿using System;
using System.Collections.Generic;
using System.Text;
using Scrummy.Domain.Core.Entities.Common;
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

        public string Description { get; set; }

        public IEnumerable<Identity> InvolvedPersons { get; set; }

        protected override void ValidateCore()
        {
            if(OrganizedBy.IsBlankIdentity())
                AddError("", "User identity is invalid.");

            if(ProjectId.IsBlankIdentity())
                AddError("", "Project identity is invalid.");

            if(!MeetingValidation.ValidateInvolvedPersons(InvolvedPersons))
                AddError(MeetingValidation.InvolvedPersonsErrorKey, MeetingValidation.InvolvedPersonsAreInvalidMessage);

            if(!MeetingValidation.ValidateName(Name))
                AddError(MeetingValidation.NameErrorKey, MeetingValidation.NameIsInvalidMessage);
        }
    }

    public class CreateMeetingResponse : BaseResponse
    {
        public CreateMeetingResponse(string message) : base(message)
        {
        }

        public Identity Id { get; set; }
    }

    public interface ICreateMeetingUseCase
    {
        CreateMeetingResponse Execute(CreateMeetingRequest request);
    }
}