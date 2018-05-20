using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Meeting
{
    public class DeleteMeetingRequest : AuthorizedRequest
    {
        public DeleteMeetingRequest(string userId) : base(userId)
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

    public interface IDeleteMeetingUseCase
    {
        ConfirmationResponse Execute(DeleteMeetingRequest request);
    }
}
