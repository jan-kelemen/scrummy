using System.Collections.Generic;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Meeting
{
    public class ListMeetingsViewModel
    {
        public class Meeting : NavigationViewModel
        {
            public string Time { get; set; }

            public string Duration { get; set; }
        }

        public NavigationViewModel Project { get; set; }

        public IEnumerable<Meeting> Meetings { get; set; } = new Meeting[0];
    }
}
