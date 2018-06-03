using System.Linq;
using Scrummy.Domain.Repositories.Extensions;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.Repositories.Interfaces.DTO;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Requests;
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

        public ViewTeamHistoryResponse Execute(AuthorizedIdRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var project = _projectRepository.Read(request.Id);
            var teams = _projectRepository.ReadProjectTeamHistory(project.Id);

            return new ViewTeamHistoryResponse
            {
                Project = project.ToInfo(),
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
