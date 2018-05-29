using System.Collections.Generic;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Project
{
    public class ViewTeamHistoryViewModel
    {
        public class Team : NavigationViewModel
        {
            public string From { get; set; }

            public string To { get; set; }
        }

        public NavigationViewModel Project { get; set; }

        public IEnumerable<Team> Teams { get; set; }
    }
}
