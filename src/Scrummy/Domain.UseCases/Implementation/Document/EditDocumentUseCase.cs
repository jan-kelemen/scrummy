using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.Document;

namespace Scrummy.Domain.UseCases.Implementation.Document
{
    public class EditDocumentUseCase : IEditDocumentUseCase
    {
        private readonly IDocumentRepository _documentRepository;

        public EditDocumentUseCase(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public ConfirmationResponse Execute(EditDocumentRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = _documentRepository.Read(request.Id);
            entity.Name = request.Name;
            entity.Content = request.Content;
            entity.Links = request.Links;

            _documentRepository.Update(entity);

            return new ConfirmationResponse("Document updated successfully.")
            {
                Id = entity.Id,
            };
        }
    }
}
