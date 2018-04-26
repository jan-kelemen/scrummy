using System.Collections.Generic;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Person
{
    public class ViewCurrentWorkViewModel
    {
        public IEnumerable<NavigationViewModel> UpcomingMeetings { get; set; }

        public IEnumerable<NavigationViewModel> CurrentProjects { get; set; }
    }
}
