using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

using ProjectValidation = Scrummy.Domain.Core.Entities.Project.Validation;

namespace Scrummy.Application.Web.MVC.ViewModels.Project
{
    public class CreateProjectViewModel
    {
        [Required(ErrorMessage = ProjectValidation.NameIsInvalidMessage)]
        [StringLength(ProjectValidation.NameMaxLength, 
            ErrorMessage = ProjectValidation.NameIsInvalidMessage, 
            MinimumLength = ProjectValidation.NameMinLength)]
        public string Name { get; set; }

        public List<string> DefinitionOfDone { get; set; } = new List<string>();

        public SelectListItem[] Teams { get; set; } = new SelectListItem[0];

        [Display(Name = "Team")]
        public string SelectedTeamId { get; set; }
    }
}
