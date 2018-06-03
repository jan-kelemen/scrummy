using System.Linq;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Repositories.Extensions;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.Repositories.Interfaces.DTO;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Domain.UseCases.Implementation.Sprint
{
    internal class SprintReportUseCase : ISprintReportUseCase
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly IWorkTaskRepository _workTaskRepository;

        public SprintReportUseCase(ISprintRepository sprintRepository, IWorkTaskRepository workTaskRepository)
        {
            _sprintRepository = sprintRepository;
            _workTaskRepository = workTaskRepository;
        }

        public SprintReportResponse Execute(AuthorizedIdRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var sprint = _sprintRepository.Read(request.Id);
            var backlog = _sprintRepository.ReadSprintBacklog(sprint.Id);
            var records = _sprintRepository.ReadHistoryRecords(sprint.Id);

            var groupByDate = records.GroupBy(x => x.Date).Select(x => new SprintReportResponse.Record
            {
                Date = x.Key,
                DoneTasks = x.Average(y => y.DoneTasks),
                ToDoTasks = x.Average(y => y.ToDoTasks),
                InProgressTasks = x.Average(y => y.InProgressTasks),
            }).OrderBy(x => x.Date).ToList();

            var stories = backlog.Stories.Select(x =>
            {
                var story = _workTaskRepository.Read(x);

                return new SprintReportResponse.Story
                {
                    Id = story.Id,
                    Name = story.Name,
                    StoryPoints = story.StoryPoints,
                    Completed = backlog.CompletedStories.Contains(x),
                    CompletedTasks = backlog.Tasks.Count(y => y.ParentTaskId == x && y.Status == SprintBacklog.WorkTaskStatus.Done),
                    TotalTasks = backlog.Tasks.Count(y => y.ParentTaskId == x),
                };
            });

            if (groupByDate.Last().Date.Date != sprint.TimeSpan.Item2.Date)
            {
                var diff = sprint.TimeSpan.Item2.Date.Subtract(groupByDate.Last().Date.Date).Days;
                for (var i = 1; i <= diff; ++i)
                {
                    var item = groupByDate.Last();
                    groupByDate.Add(new SprintReportResponse.Record
                    {
                        Date = item.Date.AddDays(i),
                        DoneTasks = item.DoneTasks,
                        ToDoTasks = item.ToDoTasks,
                        InProgressTasks = item.InProgressTasks,
                    });
                }
            }

            return new SprintReportResponse
            {
                Sprint = sprint.ToInfo(),
                Goal = sprint.Goal,
                ProjectId = sprint.ProjectId,
                TimeSpan = sprint.TimeSpan,
                Stories = stories.ToArray(),
                Records = groupByDate.ToArray(),
            };
        }
    }
}
