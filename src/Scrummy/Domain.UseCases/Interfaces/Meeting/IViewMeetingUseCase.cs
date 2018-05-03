using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using System;
using System.Collections.Generic;

namespace Scrummy.Domain.UseCases.Interfaces.Meeting
{
    public class ViewMeetingRequest : AuthorizedRequest
    {
        public ViewMeetingRequest(string userId) : base(userId)
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

    public class ViewMeetingResponse : BaseResponse
    {
        public ViewMeetingResponse() : base(null)
        {
        }

        public Identity Id { get; set; }

        public Identity OrganizedBy { get; set; }

        public Identity ProjectId { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public string Description { get; set; }

        public IEnumerable<Identity> InvolvedPersons { get; set; }
    }

    public interface IViewMeetingUseCase
    {
        ViewMeetingResponse Execute(ViewMeetingRequest request);
    }
}
