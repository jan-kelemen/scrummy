using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Interfaces.Document;

namespace Scrummy.Domain.UseCases.Implementation.Document
{
    internal class ViewDocumentUseCase : IViewDocumentUseCase
    {
        private readonly IDocumentRepository _documentRepository;

        public ViewDocumentUseCase(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public ViewDocumentResponse Execute(AuthorizedIdRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = _documentRepository.ReadWithReferences(request.Id);

            return new ViewDocumentResponse
            {
                Id = entity.Document.Id,
                Name = entity.Document.Name,
                Project = entity.Document.Project,
                Tasks = entity.Tasks,
                Content = entity.Document.Content,
                Kind = entity.Document.Kind,
                Meetings = entity.Meetings,
                Links = entity.Document.Links,
                Sprints = entity.Sprints,
            };
        }
    }
}
