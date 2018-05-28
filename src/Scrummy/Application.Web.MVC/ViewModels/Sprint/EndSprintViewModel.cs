using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Sprint
{
    public class EndSprintViewModel
    {
        public NavigationViewModel Sprint { get; set; }

        public string[] Ids { get; set; }

        public string[] Names { get; set; }

        public string[] Decisions { get; set; }

        public SelectListItem[] Statuses { get; set; } = new SelectListItem[0];
    }
}
