using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;

namespace Scrummy.Domain.UseCases.Implementation.WorkTask
{
    internal class ViewWorkTaskUseCase : IViewWorkTaskUseCase
    {
        private readonly IWorkTaskRepository _workTaskRepository;

        public ViewWorkTaskUseCase(IWorkTaskRepository workTaskRepository)
        {
            _workTaskRepository = workTaskRepository;
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
            };
        }
    }
}
