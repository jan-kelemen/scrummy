using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.WorkTask
{
    public class ViewWorkTaskViewModel
    {
        public class Comment
        {
            public string Id { get; set; }

            public NavigationViewModel Author { get; set; }

            public string Content { get; set; }
        }

        public string Id { get; set; }

        public NavigationViewModel Project { get; set; }

        public string Name { get; set; }

        [HiddenInput]
        public string Type { get; set; }

        [Display(Name = "Story points")]
        public int? StoryPoints { get; set; }

        public string Description { get; set; }

        [Display(Name = "Parent task")]
        public NavigationViewModel ParentTask { get; set; }

        public IEnumerable<NavigationViewModel> ChildTasks { get; set; } = new List<NavigationViewModel>();

        public IEnumerable<Comment> Comments { get; set; }

        public List<string> Steps { get; set; } = new List<string>();

        public bool CanEdit { get; set; }

        public bool CanEditParent { get; set; }

        public bool CanDelete => CanEdit && CanEditParent;

        public IEnumerable<NavigationViewModel> Documents { get; set; } = new NavigationViewModel[0];
    }
}
