using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

using TeamValidation = Scrummy.Domain.Core.Entities.Team.Validation;

namespace Scrummy.Application.Web.MVC.ViewModels.Team
{
    public class CreateTeamViewModel
    {
        [Required(ErrorMessage = "Team name is required.")]
        [StringLength(TeamValidation.NameMaxLength, 
            ErrorMessage = TeamValidation.NameIsInvalidMessage,
            MinimumLength = TeamValidation.NameMinLength)]
        public string Name { get; set; }

        [Display(Name = "Time of daily scrum")]
        [Required(ErrorMessage = "Time of daily scrum is required.")]
        public string TimeOfDailyScrum { get; set; }

        public List<string> SelectedMemberIds { get; set; }

        public List<string> SelectedRoles { get; set; }

        public SelectListItem[] Persons { get; set; }

        public SelectListItem[] Roles { get; set; }
    }
}
