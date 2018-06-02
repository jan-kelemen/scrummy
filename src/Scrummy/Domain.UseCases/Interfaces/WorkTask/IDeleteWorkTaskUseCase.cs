using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.WorkTask
{
    public interface IDeleteWorkTaskUseCase
    {
        ConfirmationResponse Execute(AuthorizedIdRequest request);
    }
}
