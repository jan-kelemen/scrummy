using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Project
{
    public interface IDeleteProjectUseCase
    {
        ConfirmationResponse Execute(AuthorizedIdRequest request);
    }
}
