using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Sprint
{
    public class EndSprintRequest : AuthorizedIdRequest
    {
        public enum StoryDecision
        {
            Backlog, Done
        }

        public class Story
        {
            public Identity Id { get; set; }

            public StoryDecision Decision { get; set; }
        }

        public EndSprintRequest(string userId) : base(userId)
        {
        }

        public DateTime CurrentTime { get; set; } = DateTime.Now;

        public IEnumerable<Story> Stories { get; set; }

        protected override void ValidateCore()
        {
            if (Id.IsBlankIdentity())
                AddError("", "Idenitity is invalid.");
        }
    }

    public interface IEndSprintUseCase
    {
        ConfirmationResponse Execute(EndSprintRequest request);
    }
}
