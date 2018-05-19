using System;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Domain.UseCases.Implementation.Sprint
{
    internal class ViewSprintUseCase : IViewSprintUseCase
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly IWorkTaskRepository _workTaskRepository;

        public ViewSprintUseCase(ISprintRepository sprintRepository, IWorkTaskRepository workTaskRepository)
        {
            _sprintRepository = sprintRepository;
            _workTaskRepository = workTaskRepository;
        }

        public ViewSprintResponse Execute(ViewSprintRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var sprint = _sprintRepository.Read(request.Id);
            var backlog = _sprintRepository.GetSprintBacklog(sprint.Id);
            if (sprint.Status == SprintStatus.Planned)
            {
                var tasks = new List<SprintBacklog.WorkTaskWithStatus>();
                foreach (var storyId in backlog.Stories)
                {
                    var story = _workTaskRepository.Read(storyId);
                    tasks.AddRange(
                        story.ChildTasks
                            .Select(task => new SprintBacklog.WorkTaskWithStatus(task, storyId, SprintBacklog.WorkTaskStatus.ToDo)));
                }

                backlog.Tasks = tasks;
            }

            var responseStories = backlog.Stories.Select(x => new ViewSprintResponse.Story
            {
                Id = x,
                Tasks = backlog.Tasks
                    .Where(y => y.ParentTaskId == x)
                    .Select(y => new Tuple<Identity, SprintBacklog.WorkTaskStatus>(y.WorkTaskId, y.Status)),
            });

            return new ViewSprintResponse
            {
                Id = sprint.Id,
                Goal = sprint.Goal,
                Name = sprint.Name,
                ProjectId = sprint.ProjectId,
                TimeSpan = sprint.TimeSpan,
                Stories = responseStories,
            };
        }
    }
}
