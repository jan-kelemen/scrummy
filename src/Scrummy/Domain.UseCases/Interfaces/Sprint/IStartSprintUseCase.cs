using System;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Sprint
{
    public class StartSprintRequest : AuthorizedRequest
    {
        public StartSprintRequest(string userId) : base(userId)
        {
        }

        public Identity Id { get; set; }

        public DateTime CurrentTime { get; set; } = DateTime.Now;

        protected override void ValidateCore()
        {
            if (Id.IsBlankIdentity())
                AddError("", "Idenitity is invalid.");
        }
    }

    public interface IStartSprintUseCase
    {
        ConfirmationResponse Execute(StartSprintRequest request);
    }
}
