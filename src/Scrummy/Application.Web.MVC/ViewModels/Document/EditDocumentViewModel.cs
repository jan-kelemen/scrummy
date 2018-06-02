using System.ComponentModel.DataAnnotations;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.Document
{
    public class EditDocumentViewModel
    {
        public string Id { get; set; }

        public NavigationViewModel Project { get; set; }

        [Display(Name = "Document type")]
        public string DocumentType { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public string[] Links { get; set; } = new string[0];

        public string Content { get; set; }
    }
}
