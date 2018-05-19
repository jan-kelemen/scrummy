using System.Collections.Generic;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Project
{
    public class ViewSprintsViewModel
    {
        public class SprintViewModel : NavigationViewModel
        {
            public string StartDate { get; set; }

            public string EndDate { get; set; }
        }

        public NavigationViewModel Project { get; set; }

        public bool StartSprintAllowed { get; set; }

        public string Type { get; set; }

        public IEnumerable<SprintViewModel> Sprints { get; set; }
    }
}
