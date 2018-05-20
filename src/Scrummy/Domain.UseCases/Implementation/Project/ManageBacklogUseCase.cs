using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Implementation.Project
{
    public class ManageBacklogUseCase : IManageBacklogUseCase
    {
        private readonly IProjectRepository _projectRepository;

        public ManageBacklogUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public ConfirmationResponse Execute(ManageBacklogRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var backlog = _projectRepository.ReadProductBacklog(request.ProjectId);

            foreach (var backlogItem in request.BacklogItems)
            {
                backlog.RemoveTaskFromBacklog(backlogItem.Id);
            }

            foreach (var backlogItem in request.BacklogItems)
            {
                backlog.AddTaskToBacklog(new ProductBacklog.WorkTaskWithStatus(backlogItem.Id, backlogItem.Status));
            }

            _projectRepository.UpdateProductBacklog(backlog);

            return new ConfirmationResponse("Backlog updated successfully.")
            {
                Id = request.ProjectId,
            };
        }
    }
}
