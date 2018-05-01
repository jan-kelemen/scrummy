using System.Collections.Generic;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Person
{
    public class ViewCurrentWorkViewModel
    {
        public class Meeting : NavigationViewModel
        {
            public string Time { get; set; }
        }

        public IEnumerable<NavigationViewModel> CurrentProjects { get; set; }

        public IEnumerable<Meeting> UpcomingMeetings { get; set; }
    }
}
