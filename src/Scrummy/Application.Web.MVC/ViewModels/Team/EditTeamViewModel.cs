using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Scrummy.Application.Web.MVC.ViewModels.Team
{
    public class EditTeamViewModel
    {
        [HiddenInput]
        public string Id { get; set; }

        [Required(ErrorMessage = "Team name is required.")]
        [StringLength(Domain.Core.Entities.Team.Validation.NameMaxLength,
            ErrorMessage = Domain.Core.Entities.Team.Validation.NameIsInvalidMessage,
            MinimumLength = Domain.Core.Entities.Team.Validation.NameMinLength)]
        public string Name { get; set; }

        [Display(Name = "Time of daily scrum")]
        [Required(ErrorMessage = "Time of daily scrum is required.")]
        public string TimeOfDailyScrum { get; set; }

        public List<string> SelectedMemberIds { get; set; } = new List<string>();

        public List<string> SelectedRoles { get; set; } = new List<string>();

        public SelectListItem[] Persons { get; set; } = new SelectListItem[0];

        public SelectListItem[] Roles { get; set; } = new SelectListItem[0];
    }
}
