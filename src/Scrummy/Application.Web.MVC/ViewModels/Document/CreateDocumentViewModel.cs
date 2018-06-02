using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Document
{
    public class CreateDocumentViewModel
    {
        public NavigationViewModel Project { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public string[] Links { get; set; } = new string[0];

        public string Content { get; set; }

        [Display(Name = "Document type")]
        public string DocumentType { get; set; }

        public SelectListItem[] DocumentTypes { get; set; } = new SelectListItem[0];
    }
}
