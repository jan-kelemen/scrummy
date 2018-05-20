using System;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Domain.UseCases.Implementation.Sprint
{
    internal class StartSprintUseCase : IStartSprintUseCase
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly IWorkTaskRepository _workTaskRepository;

        public StartSprintUseCase(ISprintRepository sprintRepository, IWorkTaskRepository workTaskRepository)
        {
            _sprintRepository = sprintRepository;
            _workTaskRepository = workTaskRepository;
        }

        public StartSprintResponse Execute(StartSprintRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var sprint = _sprintRepository.Read(request.Id);
            var currentSprintOfProject = _sprintRepository.ReadCurrentSprint(sprint.ProjectId);
            if (currentSprintOfProject != null)
            {
                request.Errors.Add("", "Project already has a active sprint.");
                throw new InvalidRequestException {Errors = request.Errors};
            }

            sprint.Status = SprintStatus.InProgress;
            sprint.TimeSpan = new Tuple<DateTime, DateTime>(request.CurrentTime, sprint.TimeSpan.Item2);

            var backlog = _sprintRepository.ReadSprintBacklog(sprint.Id);
            var tasks = new List<SprintBacklog.WorkTaskWithStatus>();
            foreach (var storyId in backlog.Stories)
            {
                var story = _workTaskRepository.Read(storyId);
                tasks.AddRange(
                    story.ChildTasks
                        .Select(task => new SprintBacklog.WorkTaskWithStatus(task, storyId, SprintBacklog.WorkTaskStatus.ToDo)));
            }

            backlog.Tasks = tasks;

            _sprintRepository.Update(sprint);
            _sprintRepository.UpdateCurrentTasks(backlog);

            return new StartSprintResponse("Sprint started successfully.")
            {
                ProjectId = sprint.ProjectId,
            };
        }
    }
}
