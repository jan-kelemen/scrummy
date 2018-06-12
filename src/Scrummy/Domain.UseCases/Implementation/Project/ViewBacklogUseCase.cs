using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Extensions;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.Repositories.Interfaces.DTO;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Implementation.Project
{
    public class ViewBacklogUseCase : IViewBacklogUseCase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly ITeamRepository _teamRepository;

        public ViewBacklogUseCase(IProjectRepository projectRepository, IWorkTaskRepository workTaskRepository, ITeamRepository teamRepository)
        {
            _projectRepository = projectRepository;
            _workTaskRepository = workTaskRepository;
            _teamRepository = teamRepository;
        }

        public ViewBacklogResponse Execute(ViewBacklogRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var project = _projectRepository.Read(request.ProjectId);
            var team = _teamRepository.Read(project.TeamId);
            var backlog = _projectRepository.ReadProductBacklog(project.Id);

            var backlogTasks = backlog.Select(x =>
            {
                var task = _workTaskRepository.Read(x.WorkTaskId);
                var parentTask = task.ParentTask.IsBlankIdentity() ? null : _workTaskRepository.Read(task.ParentTask);

                return new ViewBacklogResponse.Task
                {
                    Id = task.Id,
                    Name = task.Name,
                    Type = task.Type,
                    Status = x.Status,
                    StoryPoints = task.StoryPoints,
                    ParentTask = parentTask?.ToInfo(),
                };
            });

            var activeBacklogTasks = backlogTasks.Where(x => request.Include(x.Status));

            return new ViewBacklogResponse
            {
                ProjectId = request.ProjectId,
                Tasks = activeBacklogTasks,
                CanManageBacklog = team.Members.Any(x => x.Id == Identity.FromString(request.UserId))
            };
        }
    }
}
