using System.Linq;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Domain.UseCases.Implementation.Sprint
{
    internal class ChangeTaskStatusUseCase : IChangeTaskStatusUseCase
    {
        private readonly ISprintRepository _sprintRepository;

        public ChangeTaskStatusUseCase(ISprintRepository sprintRepository)
        {
            _sprintRepository = sprintRepository;
        }

        public ConfirmationResponse Execute(ChangeTaskStatusRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var sprintBacklog = _sprintRepository.ReadSprintBacklog(request.SprintId);
            sprintBacklog.Tasks.First(x => x.WorkTaskId == request.TaskId).Status = request.Status;

            _sprintRepository.UpdateCurrentTasks(sprintBacklog);

            var sprint = _sprintRepository.Read(sprintBacklog.SprintId);

            return new ConfirmationResponse("Task status updated successfully.")
            {
                Id = sprint.ProjectId,
            };
        }
    }
}
