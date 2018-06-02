using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Domain.UseCases.Implementation.Team
{
    internal class DeleteTeamUseCase : IDeleteTeamUseCase
    {
        private readonly ITeamRepository _teamRepository;

        public DeleteTeamUseCase(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public ConfirmationResponse Execute(AuthorizedIdRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var team = _teamRepository.Read(request.Id);
            _teamRepository.Delete(team.Id);

            return new ConfirmationResponse($"Team {team.Name} successfully deleted.")
            {
                Id = team.Id,
            };
        }
    }
}
