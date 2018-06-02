using System;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Project
{
    public class ProjectReportResponse : BaseResponse
    {
        public class Sprint : NavigationInfo
        {
            public DateTime EndDate { get; set; }

            public int PlannedStories { get; set; }

            public int PlannedValue { get; set; }

            public int CompletedStories { get; set; }

            public int CompletedValue { get; set; }
        }

        public class Record
        {
            public DateTime Date { get; set; }

            public double ToDoTasks { get; set; }

            public double ReadyTasks { get; set; }

            public double InSprintTasks { get; set; }

            public double DoneTasks { get; set; }
        }

        public ProjectReportResponse() : base(null)
        {
        }

        public NavigationInfo Project { get; set; }

        public Sprint[] Sprints { get; set; }

        public Record[] Records { get; set; }
    }

    public interface IProjectReportUseCase
    {
        ProjectReportResponse Execute(AuthorizedIdRequest request);
    }
}
