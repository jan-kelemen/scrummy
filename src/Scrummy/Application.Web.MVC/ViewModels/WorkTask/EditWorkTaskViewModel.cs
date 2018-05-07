using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.WorkTask
{
    public class EditWorkTaskViewModel
    {
        [HiddenInput]
        public string Id { get; set; }

        public NavigationViewModel Project { get; set; }

        public string Name { get; set; }

        [HiddenInput]
        public string Type { get; set; }

        [Display(Name = "Story points")]
        public int? StoryPoints { get; set; }

        public string Description { get; set; }

        [Display(Name = "Parent task")]
        public string ParentTaskId { get; set; }

        public List<string> ChildTaskIds { get; set; } = new List<string>();

        public SelectListItem[] ParentTasks { get; set; } = new SelectListItem[0];

        public SelectListItem[] ChildTasks { get; set; } = new SelectListItem[0];
    }
}
