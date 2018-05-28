using Scrummy.Application.Web.MVC.ViewModels.Utility;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scrummy.Application.Web.MVC.ViewModels.Meeting
{
    public class ViewMeetingViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Organized by")]
        public NavigationViewModel OrganizedBy { get; set; }

        public NavigationViewModel Project { get; set; }

        public string Name { get; set; }

        public string Time { get; set; }

        public string Duration { get; set; }

        public string Description { get; set; }

        public string Log { get; set; }

        public IEnumerable<NavigationViewModel> InvolvedPersons { get; set; }

        public bool CanDelete { get; set; }
    }
}
