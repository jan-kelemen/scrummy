using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Person
{
    public class ViewCurrentWorkRequest : AuthorizedRequest
    {
        public ViewCurrentWorkRequest(string userId) : base(userId)
        {
        }

        public Identity ForUserId { get; set; }

        public DateTime CurrentTime { get; set; } = DateTime.Now;

        protected override void ValidateCore()
        {
            if (ForUserId.IsBlankIdentity())
            {
                AddError("", "User identity is invalid.");
            }
        }
    }

    public class ViewCurrentWorkResponse : BaseResponse
    {
        public ViewCurrentWorkResponse() : base(null)
        {
        }
        
        public IEnumerable<Identity> UpcomingMeetings { get; set; }

        public IEnumerable<Identity> CurrentProjects { get; set; }
    }

    public interface IViewCurrentWorkUseCase
    {
        ViewCurrentWorkResponse Execute(ViewCurrentWorkRequest request);
    }
}
