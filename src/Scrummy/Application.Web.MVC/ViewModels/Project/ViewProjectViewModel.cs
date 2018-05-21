using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Scrummy.Application.Web.MVC.ViewModels.Sprint;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Project
{
    public class ViewProjectViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Team")]
        public NavigationViewModel Team { get; set; }

        [Display(Name = "Definition of done")]
        public IEnumerable<string> DefinitionOfDone { get; set; }

        public ViewSprintViewModel Sprint { get; set; }

        public bool CanDelete { get; set; }
    }
}
