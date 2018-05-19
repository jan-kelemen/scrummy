using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Scrummy.Application.Web.MVC.ViewModels.Utility;

using SprintValidation = Scrummy.Domain.Core.Entities.Sprint.Validation;

namespace Scrummy.Application.Web.MVC.ViewModels.Sprint
{
    public class CreateSprintViewModel
    {
        public NavigationViewModel Project { get; set; }

        [Required(ErrorMessage = "Sprint name is required.")]
        [StringLength(SprintValidation.NameMaxLength,
            ErrorMessage = SprintValidation.NameIsInvalidMessage,
            MinimumLength = SprintValidation.NameMinLength)]
        public string Name { get; set; }

        [Display(Name = "Start date")]
        [Required(ErrorMessage = "Start date is required.")]
        public string StartDate { get; set; }

        [Display(Name = "End date")]
        [Required(ErrorMessage = "End date is required.")]
        public string EndDate { get; set; }

        public string Goal { get; set; }

        public IEnumerable<string> SelectedStories { get; set; } = new string[0];

        public IEnumerable<string> Stories { get; set; } = new string[0];
    }
}
