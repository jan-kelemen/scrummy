using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Validators;
using Scrummy.Domain.UseCases.Boundary;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Person
{
    public class ViewCurrentWorkRequest : AuthorizedRequest
    {
        public ViewCurrentWorkRequest(string userId) : base(userId)
        {
        }

        public string ForUserId { get; set; }

        public DateTime CurrentTime { get; set; } = DateTime.UtcNow;

        protected override void ValidateCore()
        {
            if (!TextValidator.ValidateThatTextCanRepresentIdentity(ForUserId))
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
