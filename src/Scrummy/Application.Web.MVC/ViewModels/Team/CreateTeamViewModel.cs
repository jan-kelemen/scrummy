using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Scrummy.Application.Web.MVC.ViewModels.Team
{
    public class CreateTeamViewModel
    {
        public string Name { get; set; }

        public string TimeOfDailyScrum { get; set; }

        public List<string> SelectedMemberIds { get; set; }

        public List<string> SelectedRoles { get; set; }

        public SelectListItem[] Persons { get; set; }

        public SelectListItem[] Roles { get; set; }
    }
}
