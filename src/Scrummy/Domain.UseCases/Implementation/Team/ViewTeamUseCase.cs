using System;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Domain.UseCases.Implementation.Team
{
    internal class ViewTeamUseCase : IViewTeamUseCase
    {
        private readonly ITeamRepository _teamRepository;

        public ViewTeamUseCase(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public ViewTeamResponse Execute(ViewTeamRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = _teamRepository.Read(request.Id);

            return new ViewTeamResponse
            {
                Id = request.Id,
                Name = entity.Name,
                TimeOfDailyScrum = entity.TimeOfDailyScrum,
                Members = entity.Members,
            };
        }
    }
}
