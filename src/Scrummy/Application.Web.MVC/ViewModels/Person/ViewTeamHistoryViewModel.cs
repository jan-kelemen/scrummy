using System.Collections.Generic;
using System.Text;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Person
{
    public class ViewTeamHistoryViewModel
    {
        public class Team : NavigationViewModel
        {
            public string Roles { get; set; }

            public string From { get; set; }

            public string To { get; set; }
        }

        public NavigationViewModel Person { get; set; }

        public IEnumerable<Team> Teams { get; set; }
    }
}
