using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

namespace Scrummy.Application.Web.MVC.ViewModels.WorkTask.Comment
{
    public class EditCommentViewModel
    {
        public NavigationViewModel WorkTask { get; set; }

        [HiddenInput]
        public string CommentId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
