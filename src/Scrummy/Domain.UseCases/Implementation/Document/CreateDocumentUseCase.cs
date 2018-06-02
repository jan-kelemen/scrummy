using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.Document;

namespace Scrummy.Domain.UseCases.Implementation.Document
{
    public class CreateDocumentUseCase : ICreateDocumentUseCase
    {
        private readonly IDocumentRepository _documentRepository;

        public CreateDocumentUseCase(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public ConfirmationResponse Execute(CreateDocumentRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = ToDomainEntity(request);
            var result = _documentRepository.Create(entity);

            return new ConfirmationResponse("Document created successfully.")
            {
                Id = result,
            };
        }

        private Core.Entities.Document ToDomainEntity(CreateDocumentRequest request)
        {
            return new Core.Entities.Document(_documentRepository.GenerateNewIdentity(), 
                request.Kind, request.Name, request.ProjectId, request.Links, request.Content);
        }
    }
}
