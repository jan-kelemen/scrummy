using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Sprint
{
    public class ViewSprintViewModel
    {
        public class StoryViewModel : NavigationViewModel
        {
            public Dictionary<string, IEnumerable<NavigationViewModel>> Tasks { get; set; }
        }

        public string Id { get; set; }

        public NavigationViewModel Project { get; set; }

        public string Name { get; set; }

        [Display(Name = "Start date")]
        public string StartDate { get; set; }

        [Display(Name = "End date")]
        public string EndDate { get; set; }

        public string Goal { get; set; }

        public IEnumerable<StoryViewModel> Stories { get; set; } = new StoryViewModel[0];
    }
}
