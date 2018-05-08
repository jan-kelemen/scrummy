using System.Collections.Generic;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Project
{
    public class ViewBacklogViewModel
    {
        public class Task
        {
            public NavigationViewModel ParentTask { get; set; }

            public string Id { get; set; }

            public string Status { get; set; }

            public string Type { get; set; }

            public string Name { get; set; }
        }

        public NavigationViewModel Project { get; set; }

        public IEnumerable<Task> Tasks { get; set; }
    }
}
