using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.Document;

namespace Scrummy.Domain.UseCases.Implementation.Document
{
    internal class DeleteDocumentUseCase : IDeleteDocumentUseCase
    {
        private readonly IDocumentRepository _documentRepository;

        public DeleteDocumentUseCase(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public ConfirmationResponse Execute(AuthorizedIdRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var document = _documentRepository.Read(request.Id);
            _documentRepository.Delete(document.Id);

            return new ConfirmationResponse($"Document {document.Name} successfully deleted.")
            {
                Id = document.Id,
            };
        }
    }
}
