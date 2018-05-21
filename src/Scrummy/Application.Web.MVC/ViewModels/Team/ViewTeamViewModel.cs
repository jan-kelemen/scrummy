using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Team
{
    public class ViewTeamViewModel
    {
        public class Member : NavigationViewModel
        {
            public string Role { get; set; }
        }

        public string Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Time of daily scrum")]
        public string TimeOfDailyScrum { get; set; }

        public IEnumerable<Member> Members { get; set; }

        public IEnumerable<NavigationViewModel> CurrentProjects { get; set; }

        public bool CanDelete { get; set; }
    }
}
