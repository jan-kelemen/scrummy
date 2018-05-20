using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Project
{
    public class ManageBacklogViewModel
    {
        public NavigationViewModel Project { get; set; }

        public string[] Ids { get; set; } = new string[0];

        public string[] Name { get; set; } = new string[0];

        public string[] Type { get; set; } = new string[0];

        public string[] StoryPoints { get; set; } = new string[0];

        public string[] Status { get; set; } = new string[0];

        public SelectListItem[] Statuses { get; set; } = {
            new SelectListItem
            {
                Value = "ToDo", Text = "ToDo"
            },
            new SelectListItem
            {
                Value = "Ready", Text = "Ready"
            }
        };
    }
}
