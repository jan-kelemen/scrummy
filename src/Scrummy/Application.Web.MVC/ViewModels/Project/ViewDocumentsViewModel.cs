using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Project
{
    public class ViewDocumentsViewModel
    {
        public class Document : NavigationViewModel
        {
            public string Type { get; set; }
        }

        public NavigationViewModel Project { get; set; }

        public string Flavor { get; set; }

        public Document[] Documents { get; set; }
    }
}
