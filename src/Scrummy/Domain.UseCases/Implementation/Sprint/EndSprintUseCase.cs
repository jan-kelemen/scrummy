using System;
using System.Linq;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Domain.UseCases.Implementation.Sprint
{
    internal class EndSprintUseCase : IEndSprintUseCase
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly IProjectRepository _projectRepository;

        public EndSprintUseCase(ISprintRepository sprintRepository, IProjectRepository projectRepository)
        {
            _sprintRepository = sprintRepository;
            _projectRepository = projectRepository;
        }

        public ConfirmationResponse Execute(EndSprintRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var sprint = _sprintRepository.Read(request.Id);

            sprint.Status = SprintStatus.Done;
            sprint.TimeSpan = new Tuple<DateTime, DateTime>(sprint.TimeSpan.Item2, request.CurrentTime);

            var backlog = _sprintRepository.ReadSprintBacklog(sprint.Id);
            var projectBacklog = _projectRepository.ReadProductBacklog(sprint.ProjectId);
            var tasksByStory = backlog.Tasks.GroupBy(x => x.ParentTaskId);

            foreach (var story in tasksByStory)
            {
                projectBacklog.UpdateTask(new ProductBacklog.WorkTaskWithStatus(story.Key,
                    story.All(x => x.Status == SprintBacklog.WorkTaskStatus.Done) 
                        ? ProductBacklog.WorkTaskStatus.Done 
                        : ProductBacklog.WorkTaskStatus.Ready));
            }

            _sprintRepository.Update(sprint);

            return new ConfirmationResponse("Sprint ended successfully.")
            {
                Id = sprint.ProjectId,
            };
        }
    }
}
