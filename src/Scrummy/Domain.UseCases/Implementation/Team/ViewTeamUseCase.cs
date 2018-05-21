using System.Linq;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Domain.UseCases.Implementation.Team
{
    internal class ViewTeamUseCase : IViewTeamUseCase
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IProjectRepository _projectRepository;

        public ViewTeamUseCase(ITeamRepository teamRepository, IProjectRepository projectRepository)
        {
            _teamRepository = teamRepository;
            _projectRepository = projectRepository;
        }

        public ViewTeamResponse Execute(ViewTeamRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = _teamRepository.Read(request.Id);
            var historyRecord = _projectRepository.ReadTeamProjectHistory(entity.Id);

            return new ViewTeamResponse
            {
                Id = request.Id,
                Name = entity.Name,
                TimeOfDailyScrum = entity.TimeOfDailyScrum,
                Members = entity.Members,
                CurrentProjects = _projectRepository.GetProjectsOfTeamAtTimePoint(request.Id, request.CurrentTime),
                CanDelete = !historyRecord.Records.Any()
            };
        }
    }
}
