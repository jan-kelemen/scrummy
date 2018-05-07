using System.Linq;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
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

        public bool CanWorkTaskBeEdited(Identity id)
        {
            var workTask = _workTaskRepository.Read(id);
            var backlog = _projectRepository.ReadProductBacklog(workTask.ProjectId);
            var taskFromBacklog = backlog.First(x => x.WorkTaskId == id);

            return taskFromBacklog.Status != ProductBacklog.WorkTaskStatus.InSprint || taskFromBacklog.Status != ProductBacklog.WorkTaskStatus.Done;
        }

        public EditWorkTaskResponse Execute(EditWorkTaskRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = _workTaskRepository.Read(request.Id);
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.StoryPoints = request.StoryPoints;
            entity.ParentTask = request.ParentTask;
            entity.ChildTasks = request.ChildTasks;
            
            _workTaskRepository.Update(entity);

            return new EditWorkTaskResponse("Work task updated successfully.")
            {
                Id = entity.Id,
            };
        }
    }
}
