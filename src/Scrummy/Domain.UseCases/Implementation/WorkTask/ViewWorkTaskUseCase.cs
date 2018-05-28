using System.Linq;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;

namespace Scrummy.Domain.UseCases.Implementation.WorkTask
{
    internal class ViewWorkTaskUseCase : IViewWorkTaskUseCase
    {
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly IProjectRepository _projectRepository;

        public ViewWorkTaskUseCase(IWorkTaskRepository workTaskRepository, IProjectRepository projectRepository)
        {
            _workTaskRepository = workTaskRepository;
            _projectRepository = projectRepository;
        }

        public ViewWorkTaskResponse Execute(ViewWorkTaskRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = _workTaskRepository.Read(request.Id);

            return new ViewWorkTaskResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ProjectId = entity.ProjectId,
                ParentTask = entity.ParentTask,
                Type = entity.Type,
                ChildTasks = entity.ChildTasks,
                StoryPoints = entity.StoryPoints,
                Steps = entity.Steps,
                CanEdit = CanWorkTaskBeEdited(entity),
                CanEditParent = entity.ParentTask.IsBlankIdentity() || CanWorkTaskBeEdited(_workTaskRepository.Read(entity.ParentTask)),
            };
        }

        private bool CanWorkTaskBeEdited(Core.Entities.WorkTask workTask)
        {
            var backlog = _projectRepository.ReadProductBacklog(workTask.ProjectId);
            var taskFromBacklog = backlog.FirstOrDefault(x => x.WorkTaskId == workTask.Id) ?? backlog.First(x => x.WorkTaskId == workTask.ParentTask);

            return taskFromBacklog.Status != ProductBacklog.WorkTaskStatus.InSprint && taskFromBacklog.Status != ProductBacklog.WorkTaskStatus.Done;
        }
    }
}
