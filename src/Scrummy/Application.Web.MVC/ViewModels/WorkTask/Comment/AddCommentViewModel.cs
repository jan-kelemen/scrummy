using System.ComponentModel.DataAnnotations;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.WorkTask.Comment
{
    public class AddCommentViewModel
    {
        public NavigationViewModel WorkTask { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
