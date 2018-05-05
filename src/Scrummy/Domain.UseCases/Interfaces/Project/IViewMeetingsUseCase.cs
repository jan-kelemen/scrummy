using System;
using System.Collections.Generic;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Project
{
    public class ViewMeetingsRequest : AuthorizedRequest
    {
        public ViewMeetingsRequest(string userId) : base(userId)
        {
        }

        public Identity ProjectId { get; set; }

        public DateTime CurrentTime { get; set; } = DateTime.Now;

        protected override void ValidateCore()
        {
            if (ProjectId.IsBlankIdentity())
            {
                AddError("", "Project idenitity is invalid.");
            }
        }
    }

    public class ViewMeetingsResponse : BaseResponse
    {
        public ViewMeetingsResponse() : base(null)
        {
        }

        public Identity ProjectId { get; set; }

        public IEnumerable<Identity> Meetings { get; set; }
    }

    public interface IViewMeetingsUseCase
    {
        ViewMeetingsResponse Execute(ViewMeetingsRequest request);
    }
}
