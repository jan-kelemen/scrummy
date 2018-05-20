using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Team
{
    public class DeleteTeamRequest : AuthorizedRequest
    {
        public DeleteTeamRequest(string userId) : base(userId)
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

    public interface IDeleteTeamUseCase
    {
        ConfirmationResponse Execute(DeleteTeamRequest request);
    }
}
