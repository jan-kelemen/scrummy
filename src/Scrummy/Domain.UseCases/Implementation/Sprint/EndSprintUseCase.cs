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
            sprint.TimeSpan = new Tuple<DateTime, DateTime>(sprint.TimeSpan.Item1, request.CurrentTime);

            var backlog = _sprintRepository.ReadSprintBacklog(sprint.Id);
            var projectBacklog = _projectRepository.ReadProductBacklog(sprint.ProjectId);

            var doneStories = request.Stories
                .Where(x => x.Decision == EndSprintRequest.StoryDecision.Done)
                .Select(x => x.Id)
                .ToList();

            var backlogStories = request.Stories
                .Where(x => x.Decision == EndSprintRequest.StoryDecision.Backlog)
                .Select(x => x.Id);

            foreach (var done in doneStories)
            {
                foreach (var task in backlog.Tasks.Where(x => x.ParentTaskId == done))
                {
                    task.Status = SprintBacklog.WorkTaskStatus.Done;
                }
                projectBacklog.First(x => x.WorkTaskId == done).Status = ProductBacklog.WorkTaskStatus.Done;
            }
            backlog.CompletedStories = doneStories;

            foreach (var ready in backlogStories)
            {
                projectBacklog.First(x => x.WorkTaskId == ready).Status = ProductBacklog.WorkTaskStatus.Ready;
            }

            _sprintRepository.UpdateCurrentTasks(backlog);
            _projectRepository.UpdateProductBacklog(projectBacklog);
            _sprintRepository.Update(sprint);

            return new ConfirmationResponse("Sprint ended successfully.")
            {
                Id = sprint.ProjectId,
            };
        }
    }
}
