using System.Collections.Generic;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Project
{
    public class ListProjectsViewModel
    {
        public class Project
        {
            public NavigationViewModel ProjectViewModel { get; set; }

            public NavigationViewModel TeamViewModel { get; set; }
        }

        public IEnumerable<Project> Projects { get; set; }
    }
}
