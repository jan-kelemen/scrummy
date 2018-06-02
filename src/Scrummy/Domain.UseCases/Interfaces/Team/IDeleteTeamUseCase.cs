using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Team
{
    public interface IDeleteTeamUseCase
    {
        ConfirmationResponse Execute(AuthorizedIdRequest request);
    }
}
