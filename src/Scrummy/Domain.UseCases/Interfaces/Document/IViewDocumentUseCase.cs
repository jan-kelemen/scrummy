using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Document
{
    public class ViewDocumentRequest : AuthorizedRequest
    {
        public ViewDocumentRequest(string userId) : base(userId)
        {
        }

        public Identity Id { get; set; }

        protected override void ValidateCore()
        {
            if (Id.IsBlankIdentity())
                AddError("", "Document identity is invalid.");
        }
    }

    public class ViewDocumentResponse : BaseResponse
    {
        public ViewDocumentResponse() : base(null)
        {
        }

        public Identity Id { get; set; }

        public string Name { get; set; }

        public DocumentKind Kind { get; set; }

        public IEnumerable<string> Links { get; set; }

        public string Content { get; set; }

        public Identity Project { get; set; }

        public IEnumerable<Identity> Meetings { get; set; }

        public IEnumerable<Identity> Sprints { get; set; }

        public IEnumerable<Identity> Tasks { get; set; }
    }

    public interface IViewDocumentUseCase
    {
        ViewDocumentResponse Execute(ViewDocumentRequest request);
    }
}
