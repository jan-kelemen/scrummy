using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Document
{
    public class EditDocumentRequest : AuthorizedRequest
    {
        public EditDocumentRequest(string userId) : base(userId)
        {
        }

        public Identity Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Links { get; set; }

        public string Content { get; set; }

        protected override void ValidateCore()
        {
            if (Id.IsBlankIdentity())
                AddError("", "Document identity is invalid.");
        }
    }

    public interface IEditDocumentUseCase
    {
        ConfirmationResponse Execute(EditDocumentRequest request);
    }
}
