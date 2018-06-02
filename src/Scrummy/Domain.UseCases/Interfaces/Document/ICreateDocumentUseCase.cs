using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Document
{
    public class CreateDocumentRequest : AuthorizedRequest
    {
        public CreateDocumentRequest(string userId) : base(userId)
        {
        }

        public Identity ProjectId { get; set; }

        public string Name { get; set; }

        public DocumentKind Kind { get; set; }

        public IEnumerable<string> Links { get; set; }

        public string Content { get; set; }

        protected override void ValidateCore()
        {
            if (ProjectId.IsBlankIdentity())
                AddError("", "Project identity is invalid.");
        }
    }

    public interface ICreateDocumentUseCase
    {
        ConfirmationResponse Execute(CreateDocumentRequest request);
    }
}
