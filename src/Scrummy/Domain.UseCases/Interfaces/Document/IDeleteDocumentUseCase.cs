using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Document
{
    public class DeleteDocumentRequest : AuthorizedRequest
    {
        public DeleteDocumentRequest(string userId) : base(userId)
        {
        }

        public Identity Id { get; set; }

        protected override void ValidateCore()
        {
            if (Id.IsBlankIdentity())
                AddError("", "Document identity is invalid.");
        }
    }

    public interface IDeleteDocumentUseCase
    {
        ConfirmationResponse Execute(DeleteDocumentRequest request);
    }
}
