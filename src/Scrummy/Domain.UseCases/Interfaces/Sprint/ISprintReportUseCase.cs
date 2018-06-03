using System;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.Repositories.Interfaces.DTO;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Sprint
{
    public class SprintReportResponse : BaseResponse
    {
        public class Story : NavigationInfo
        {
            public int? StoryPoints { get; set; }

            public bool Completed { get; set; }

            public int CompletedTasks { get; set; }

            public int TotalTasks { get; set; }
        }

        public class Record
        {
            public DateTime Date { get; set; }

            public double ToDoTasks { get; set; }

            public double InProgressTasks { get; set; }

            public double DoneTasks { get; set; }
        }

        public SprintReportResponse() : base(null)
        {
        }

        public Identity ProjectId { get; set; }

        public NavigationInfo Sprint { get; set; }

        public Tuple<DateTime, DateTime> TimeSpan { get; set; }

        public string Goal { get; set; }

        public Story[] Stories { get; set; }

        public Record[] Records { get; set; }
    }

    public interface ISprintReportUseCase
    {
        SprintReportResponse Execute(AuthorizedIdRequest request);
    }
}
