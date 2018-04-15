using System.ComponentModel.DataAnnotations;
using Scrummy.Domain.Core.Validators.Entities;

namespace Scrummy.Application.Web.Core.ViewModels.Entities.Person
{
    public class CreatePersonViewModel
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name message is required.")]
        [StringLength(PersonValidator.FirstNameMaxLength,
            ErrorMessage = "First name is invalid.",
            MinimumLength = PersonValidator.FirstNameMinLength)]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(PersonValidator.LastNameMaxLength,
            ErrorMessage = "Last name is invalid.",
            MinimumLength = PersonValidator.LastNameMinLength)]
        public string LastName { get; set; }

        [Display(Name = "Display name")]
        [Required(ErrorMessage = "Display name is required.")]
        [StringLength(PersonValidator.DisplayNameMaxLength, 
            ErrorMessage = "Display name is invalid.", 
            MinimumLength = PersonValidator.DisplayNameMinLength)]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
