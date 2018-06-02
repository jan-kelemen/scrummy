using System.Linq;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Implementation.Project
{
    internal class ProjectReportUseCase : IProjectReportUseCase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ISprintRepository _sprintRepository;
        private readonly IWorkTaskRepository _workTaskRepository;

        public ProjectReportUseCase(IProjectRepository projectRepository, ISprintRepository sprintRepository, IWorkTaskRepository workTaskRepository)
        {
            _projectRepository = projectRepository;
            _sprintRepository = sprintRepository;
            _workTaskRepository = workTaskRepository;
        }

        public ProjectReportResponse Execute(AuthorizedIdRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var project = _projectRepository.Read(request.Id);
            var records = _projectRepository.ReadHistoryRecords(project.Id);
            var sprints = _sprintRepository.ReadSprints(project.Id, SprintStatus.Done);

            var recordsByDate = records.GroupBy(x => x.Date).Select(x => new ProjectReportResponse.Record
            {
                Date = x.Key,
                DoneTasks = x.Average(y => y.DoneTasks),
                ToDoTasks = x.Average(y => y.ToDoTasks),
                InSprintTasks = x.Average(y => y.InSprintTasks),
                ReadyTasks = x.Average(y => y.ReadyTasks),
            });

            var sprintReports = sprints.Select(x =>
            {
                var sprintBacklog = _sprintRepository.ReadSprintBacklog(x.Id);

                return new ProjectReportResponse.Sprint
                {
                    Id = x.Id,
                    Name = x.Name,
                    EndDate = x.TimeSpan.Item2,
                    PlannedStories = sprintBacklog.Stories.Count,
                    PlannedValue = sprintBacklog.Stories.Sum(y => _workTaskRepository.Read(y).StoryPoints ?? 0),
                    CompletedStories = sprintBacklog.CompletedStories.Count,
                    CompletedValue = sprintBacklog.CompletedStories.Sum(y => _workTaskRepository.Read(y).StoryPoints ?? 0),
                };
            });

            return new ProjectReportResponse
            {
                Project = new NavigationInfo
                {
                    Id = project.Id,
                    Name = project.Name,
                },
                Records = recordsByDate.ToArray(),
                Sprints = sprintReports.ToArray(),
            };
        }
    }
}
