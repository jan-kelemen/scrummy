using System;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Project
{
    public class ProjectReportViewModel
    {
        public class Sprint : NavigationViewModel
        {
            public string EndDate { get; set; }

            public int PlannedStories { get; set; }

            public int PlannedValue { get; set; }

            public int CompletedStories { get; set; }

            public int CompletedValue { get; set; }
        }

        public class Record
        {
            public string Date { get; set; }

            public double ToDoTasks { get; set; }

            public double ReadyTasks { get; set; }

            public double InSprintTasks { get; set; }

            public double DoneTasks { get; set; }
        }

        public NavigationViewModel Project { get; set; }

        public Sprint[] Sprints { get; set; }

        public IEnumerable<Sprint> SprintsForChart => Sprints.TakeLast(Math.Min(10, Sprints.Length));

        public Record[] Records { get; set; }

        public IEnumerable<string> BurnupNames => new[] {""}.Concat(Sprints.Select(x => x.Text));

        public IEnumerable<int> BurnupValues => new []{ 0 }.Concat(Sprints.Select((t, i) => Sprints.Take(Math.Min(i + 1, Sprints.Length)).Sum(x => x.CompletedValue)));
    }
}
