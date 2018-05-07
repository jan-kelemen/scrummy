using System.Linq;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;

namespace Scrummy.Domain.UseCases.Implementation.WorkTask
{
    internal class CreateWorkTaskUseCase : ICreateWorkTaskUseCase
    {
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly IProjectRepository _projectRepository;

        public CreateWorkTaskUseCase(IWorkTaskRepository workTaskRepository, IProjectRepository projectRepository)
        {
            _workTaskRepository = workTaskRepository;
            _projectRepository = projectRepository;
        }

        public CreateWorkTaskResponse Execute(CreateWorkTaskRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var project = _projectRepository.Read(request.ProjectId);
            var entity = ToDomainEntity(request);
            var backlog = _projectRepository.ReadProductBacklog(project.Id);

            var hasParentThatIsInSprint = !request.ParentTask.IsBlankIdentity() && 
                                          backlog.FirstOrDefault(x => x.WorkTaskId == request.ParentTask 
                                                                      && x.Status == ProductBacklog.WorkTaskStatus.InSprint) != null;

            backlog.AddTaskToBacklog(new ProductBacklog.WorkTaskWithStatus(entity.Id, 
                hasParentThatIsInSprint ? ProductBacklog.WorkTaskStatus.InSprint : ProductBacklog.WorkTaskStatus.ToDo));

            _workTaskRepository.Create(entity);
            _projectRepository.UpdateProductBacklog(backlog);

            return new CreateWorkTaskResponse("Work task created successfully.")
            {
                Id = entity.Id,
            };
        }

        private Core.Entities.WorkTask ToDomainEntity(CreateWorkTaskRequest request)
        {
            return new Core.Entities.WorkTask(
                _workTaskRepository.GenerateNewIdentity(),
                request.ProjectId,
                request.Type,
                request.Name,
                request.StoryPoints,
                request.Description,
                request.ParentTask,
                request.ChildTasks,
                new Identity[0]);
        }
    }
}
