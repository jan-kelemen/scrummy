using System.Collections.Generic;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Project
{
    public class ViewMeetingsViewModel
    {
        public class Meeting : NavigationViewModel
        {
            public string Time { get; set; }
        }

        public NavigationViewModel Project { get; set; }

        public IEnumerable<Meeting> UpcomingMeetings { get; set; }
    }
}
