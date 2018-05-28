using System;
using System.Collections.Generic;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Team
{
    public class ViewProjectHistoryViewModel
    {
        public class Project : NavigationViewModel
        {
            public string From { get; set; }

            public string To { get; set; }
        }

        public NavigationViewModel Team { get; set; }

        public IEnumerable<Project> Projects { get; set; }
    }
}
