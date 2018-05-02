using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using ProjectValidation = Scrummy.Domain.Core.Entities.Project.Validation;

namespace Scrummy.Application.Web.MVC.ViewModels.Project
{
    public class EditProjectViewModel
    {
        [HiddenInput]
        public string Id { get; set; }

        [Required(ErrorMessage = ProjectValidation.NameIsInvalidMessage)]
        [StringLength(ProjectValidation.NameMaxLength, 
            ErrorMessage = ProjectValidation.NameIsInvalidMessage, 
            MinimumLength = ProjectValidation.NameMinLength)]
        public string Name { get; set; }

        public List<string> DefinitionOfDone { get; set; }

        public SelectListItem[] Teams { get; set; }

        [Display(Name = "Team")]
        public string SelectedTeamId { get; set; }
    }
}
