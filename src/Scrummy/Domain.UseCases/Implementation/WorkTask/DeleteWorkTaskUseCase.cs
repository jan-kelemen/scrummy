using System.Linq;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;

namespace Scrummy.Domain.UseCases.Implementation.WorkTask
{
    internal class DeleteWorkTaskUseCase : IDeleteWorkTaskUseCase
    {
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly IProjectRepository _projectRepository;

        public DeleteWorkTaskUseCase(IWorkTaskRepository workTaskRepository, IProjectRepository projectRepository)
        {
            _workTaskRepository = workTaskRepository;
            _projectRepository = projectRepository;
        }

        public ConfirmationResponse Execute(AuthorizedIdRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var workTask = _workTaskRepository.Read(request.Id);
            var backlog = _projectRepository.ReadProductBacklog(workTask.ProjectId);

            var taskInBacklog = backlog.FirstOrDefault(x => x.WorkTaskId == workTask.Id);
            if (taskInBacklog != null)
            {
                foreach (var s in workTask.ChildTasks)
                {
                    if (!backlog.IsTaskInBacklog(s))
                    {
                        backlog.AddTaskToBacklog(new ProductBacklog.WorkTaskWithStatus(s, taskInBacklog.Status));
                    }
                }

                backlog.RemoveTaskFromBacklog(workTask.Id);
            }

            _projectRepository.UpdateProductBacklog(backlog);
            _workTaskRepository.Delete(workTask.Id);

            return new ConfirmationResponse($"WorkTask {workTask.Name} successfully deleted.")
            {
                Id = workTask.Id,
            };
        }
    }
}
