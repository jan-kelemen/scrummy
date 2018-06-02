using System.ComponentModel.DataAnnotations;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Application.Web.MVC.ViewModels.Document
{
    public class ViewDocumentViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Document type")]
        public string DocumentType { get; set; }

        public string[] Links { get; set; }

        public string Content { get; set; }

        public NavigationViewModel Project { get; set; }

        public NavigationViewModel[] Meetings { get; set; }

        public NavigationViewModel[] Sprints { get; set; }

        public NavigationViewModel[] Tasks { get; set; }
    }
}
