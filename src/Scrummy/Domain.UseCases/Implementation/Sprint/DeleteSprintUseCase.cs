using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Domain.UseCases.Implementation.Sprint
{
    internal class DeleteSprintUseCase : IDeleteSprintUseCase
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly IProjectRepository _projectRepository;

        public DeleteSprintUseCase(ISprintRepository sprintRepository, IProjectRepository projectRepository)
        {
            _sprintRepository = sprintRepository;
            _projectRepository = projectRepository;
        }

        public ConfirmationResponse Execute(DeleteSprintRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var sprint = _sprintRepository.Read(request.Id);
            var sprintBacklog = _sprintRepository.ReadSprintBacklog(sprint.Id);

            var projectBacklog = _projectRepository.ReadProductBacklog(sprint.ProjectId);
            foreach (var s in sprintBacklog.Stories)
                projectBacklog.UpdateTask(new ProductBacklog.WorkTaskWithStatus(s, ProductBacklog.WorkTaskStatus.Ready));

            _sprintRepository.Delete(sprint.Id);
            _projectRepository.UpdateProductBacklog(projectBacklog);

            return new ConfirmationResponse($"Sprint {sprint.Name} successfully deleted.")
            {
                Id = sprint.Id,
            };
        }
    }
}
