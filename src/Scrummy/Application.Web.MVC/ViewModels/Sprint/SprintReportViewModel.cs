using System;
using System.ComponentModel.DataAnnotations;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Sprint
{
    public class SprintReportViewModel
    {
        public class Story : NavigationViewModel
        {
            public int StoryPoints { get; set; }

            public bool Completed { get; set; }

            public int CompletedTasks { get; set; }

            public int TotalTasks { get; set; }
        }

        public class Record
        {
            public string Date { get; set; }

            public double ToDoTasks { get; set; }

            public double InProgressTasks { get; set; }

            public double DoneTasks { get; set; }
        }

        public NavigationViewModel Project { get; set; }

        public NavigationViewModel Sprint { get; set; }

        [Display(Name = "Start date")]
        public string StartDate { get; set; }

        [Display(Name = "End date")]
        public string EndDate { get; set; }

        public string Goal { get; set; }

        public Story[] Stories { get; set; } = new Story[0];

        public Record[] Records { get; set; } = new Record[0];
    }
}
