using System.Linq;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Implementation.Project
{
    public class PlanBacklogUseCase : IPlanBacklogUseCase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ISprintRepository _sprintRepository;

        public PlanBacklogUseCase(IProjectRepository projectRepository, ISprintRepository sprintRepository)
        {
            _projectRepository = projectRepository;
            _sprintRepository = sprintRepository;
        }

        public ConfirmationResponse Execute(PlanBacklogRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var sprintBacklogs = _sprintRepository
                .ReadSprintBacklogs(request.ProjectId, SprintStatus.Planned)
                .ToArray();

            var backlog = _projectRepository.ReadProductBacklog(request.ProjectId);
            foreach (var item in request.BacklogItems)
            {
                var fromBacklog = backlog.First(x => x.WorkTaskId == item.Id);
                if (fromBacklog.Status == ProductBacklog.WorkTaskStatus.ToDo ||
                    fromBacklog.Status == ProductBacklog.WorkTaskStatus.Done)
                {
                    request.Errors.Add("", "Task can't be planned.");
                    throw new InvalidRequestException { Errors = request.Errors };
                }

                var inSprint = false;
                foreach (var sprintBacklog in sprintBacklogs)
                {
                    sprintBacklog.Stories.Remove(item.Id);
                    if (sprintBacklog.SprintId != item.Sprint) continue;
                    sprintBacklog.Stories.Add(item.Id);
                    inSprint = true;
                }

                fromBacklog.Status = inSprint 
                    ? ProductBacklog.WorkTaskStatus.InSprint 
                    : ProductBacklog.WorkTaskStatus.Ready;
            }

            foreach (var sprintBacklog in sprintBacklogs)
            {
                _sprintRepository.UpdatePlannedTasks(sprintBacklog);
            }
            _projectRepository.UpdateProductBacklog(backlog);

            return new ConfirmationResponse("Backlog updated successfully.")
            {
                Id = request.ProjectId,
            };
        }
    }
}
