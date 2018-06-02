using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Document
{
    public interface IDeleteDocumentUseCase
    {
        ConfirmationResponse Execute(AuthorizedIdRequest request);
    }
}
