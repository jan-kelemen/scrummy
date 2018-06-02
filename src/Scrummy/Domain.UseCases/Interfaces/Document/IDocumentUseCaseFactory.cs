namespace Scrummy.Domain.UseCases.Interfaces.Document
{
    public interface IDocumentUseCaseFactory
    {
        ICreateDocumentUseCase Create { get; }

        IEditDocumentUseCase Edit { get; }

        IViewDocumentUseCase View { get; }
        
        IDeleteDocumentUseCase Delete { get; }
    }
}
