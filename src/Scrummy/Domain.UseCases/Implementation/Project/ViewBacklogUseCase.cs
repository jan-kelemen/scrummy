using System.Linq;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Implementation.Project
{
    public class ViewBacklogUseCase : IViewBacklogUseCase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IWorkTaskRepository _workTaskRepository;

        public ViewBacklogUseCase(IProjectRepository projectRepository, IWorkTaskRepository workTaskRepository)
        {
            _projectRepository = projectRepository;
            _workTaskRepository = workTaskRepository;
        }

        public ViewBacklogResponse Execute(ViewBacklogRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var backlog = _projectRepository.ReadProductBacklog(request.ProjectId);

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
                    ParentTask = parentTask == null ? null : new NavigationInfo
                    {
                        Id = parentTask.Id,
                        Name = parentTask.Name
                    }
                };
            });

            var activeBacklogTasks = backlogTasks
                .Where(x => x.Status == ProductBacklog.WorkTaskStatus.ToDo || x.Status == ProductBacklog.WorkTaskStatus.Ready);

            return new ViewBacklogResponse
            {
                ProjectId = request.ProjectId,
                Tasks = activeBacklogTasks,
            };
        }
    }
}
