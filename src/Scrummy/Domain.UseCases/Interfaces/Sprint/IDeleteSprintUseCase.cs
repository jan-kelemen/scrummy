using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Sprint
{
    public interface IDeleteSprintUseCase
    {
        ConfirmationResponse Execute(AuthorizedIdRequest request);
    }
}
