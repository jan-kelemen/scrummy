using System.Linq;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;

namespace Scrummy.Domain.UseCases.Implementation.WorkTask
{
    internal class EditWorkTaskUseCase : IEditWorkTaskUseCase
    {
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly IProjectRepository _projectRepository;

        public EditWorkTaskUseCase(IWorkTaskRepository workTaskRepository, IProjectRepository projectRepository)
        {
            _workTaskRepository = workTaskRepository;
            _projectRepository = projectRepository;
        }

        public ConfirmationResponse Execute(EditWorkTaskRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = _workTaskRepository.Read(request.Id);
            var backlog = _projectRepository.ReadProductBacklog(entity.ProjectId);

            var oldChildren = entity.ChildTasks;
            UpdateWorkTask(entity, request);

            var hasBacklogChanged = false;

            if (entity.Type == WorkTaskType.UserStory)
            {
                var removedChildren = oldChildren.Except(entity.ChildTasks).ToArray();
                foreach (var task in entity.ChildTasks)
                    hasBacklogChanged |= backlog.RemoveTaskFromBacklog(task);

                foreach (var child in removedChildren)
                    backlog.AddTaskToBacklog(new ProductBacklog.WorkTaskWithStatus(child, ProductBacklog.WorkTaskStatus.ToDo));

                hasBacklogChanged |= removedChildren.Any();
            }
            else if (entity.Type != WorkTaskType.Epic && entity.ParentTask.IsBlankIdentity())
            {
                backlog.AddTaskToBacklog(new ProductBacklog.WorkTaskWithStatus(entity.Id, ProductBacklog.WorkTaskStatus.ToDo));
                hasBacklogChanged = true;
            }

            _workTaskRepository.Update(entity);
            if(hasBacklogChanged)
                _projectRepository.UpdateProductBacklog(backlog);

            return new ConfirmationResponse("Work task updated successfully.")
            {
                Id = entity.Id,
            };
        }

        private void UpdateWorkTask(Core.Entities.WorkTask entity, EditWorkTaskRequest request)
        {
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.StoryPoints = request.StoryPoints;
            entity.ParentTask = request.ParentTask;
            entity.ChildTasks = request.ChildTasks;
            entity.Steps = request.Steps;
        }
    }
}
