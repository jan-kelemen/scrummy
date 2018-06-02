using System;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Sprint
{
    public class StartSprintRequest : AuthorizedIdRequest
    {
        public StartSprintRequest(string userId) : base(userId)
        {
        }

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
