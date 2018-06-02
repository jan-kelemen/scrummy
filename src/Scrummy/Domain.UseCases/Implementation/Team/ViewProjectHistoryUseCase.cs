using System.Linq;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Domain.UseCases.Implementation.Team
{
    internal class ViewProjectHistoryUseCase : IViewProjectHistoryUseCase
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IProjectRepository _projectRepository;

        public ViewProjectHistoryUseCase(ITeamRepository teamRepository, IProjectRepository projectRepository)
        {
            _teamRepository = teamRepository;
            _projectRepository = projectRepository;
        }

        public ViewProjectHistoryResponse Execute(AuthorizedIdRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var team = _teamRepository.Read(request.Id);
            var projects = _projectRepository.ReadTeamProjectHistory(team.Id);

            return new ViewProjectHistoryResponse
            {
                Team = new NavigationInfo
                {
                    Id = team.Id,
                    Name = team.Name,
                },
                Projects = projects.Records.Select(x => new ViewProjectHistoryResponse.Project
                {
                    Id = x.RecordId,
                    To = x.To,
                    From = x.From
                })
            };
        }
    }
}
