using System.Collections.Generic;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Team
{
    public class ViewMemberHistoryViewModel
    {
        public class Member : NavigationViewModel
        {
            public string Role { get; set; }
        }

        public class TeamMembers
        {
            public string From { get; set; }

            public string To { get; set; }

            public IEnumerable<Member> Members { get; set; }
        }

        public NavigationViewModel Team { get; set; }

        public IEnumerable<TeamMembers> Members { get; set; }
    }
}
