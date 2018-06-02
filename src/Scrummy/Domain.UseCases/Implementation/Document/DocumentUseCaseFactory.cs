using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Document;

namespace Scrummy.Domain.UseCases.Implementation.Document
{
    internal class DocumentUseCaseFactory : IDocumentUseCaseFactory
    {
        private readonly IRepositoryProvider _repositoryProvider;

        public DocumentUseCaseFactory(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }

        public ICreateDocumentUseCase Create => new CreateDocumentUseCase(_repositoryProvider.Document);
        public IEditDocumentUseCase Edit => new EditDocumentUseCase(_repositoryProvider.Document);
        public IViewDocumentUseCase View => new ViewDocumentUseCase(_repositoryProvider.Document);
        public IDeleteDocumentUseCase Delete => new DeleteDocumentUseCase(_repositoryProvider.Document);
    }
}
