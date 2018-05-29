using System.Linq;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Implementation.Project
{
    public class ViewTeamHistoryUseCase : IViewTeamHistoryUseCase
    {
        private readonly IProjectRepository _projectRepository;

        public ViewTeamHistoryUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public ViewTeamHistoryResponse Execute(ViewTeamHistoryRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var project = _projectRepository.Read(request.Id);
            var teams = _projectRepository.ReadProjectTeamHistory(project.Id);

            return new ViewTeamHistoryResponse
            {
                Project = new NavigationInfo
                {
                    Id = project.Id,
                    Name = project.Name,
                },
                Teams = teams.Records.Select(x => new ViewTeamHistoryResponse.Team
                {
                    Id = x.RecordId,
                    To = x.To,
                    From = x.From,
                }),
            };
        }
    }
}
