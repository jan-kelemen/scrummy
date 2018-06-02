using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Meeting
{
    public interface IDeleteMeetingUseCase
    {
        ConfirmationResponse Execute(AuthorizedIdRequest request);
    }
}
