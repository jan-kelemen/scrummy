using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Project
{
    public class DeleteProjectRequest : AuthorizedRequest
    {
        public DeleteProjectRequest(string userId) : base(userId)
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

    public interface IDeleteProjectUseCase
    {
        ConfirmationResponse Execute(DeleteProjectRequest request);
    }
}
