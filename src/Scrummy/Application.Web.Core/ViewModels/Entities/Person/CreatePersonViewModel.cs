using System.ComponentModel.DataAnnotations;
using Scrummy.Domain.Core.Validators.Entities;

namespace Scrummy.Application.Web.Core.ViewModels.Entities.Person
{
    public class CreatePersonViewModel
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name message is required.")]
        [StringLength(PersonValidator.FirstNameMaxLength,
            ErrorMessage = PersonValidator.FirstNameIsInvalidMessage,
            MinimumLength = PersonValidator.FirstNameMinLength)]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(PersonValidator.LastNameMaxLength,
            ErrorMessage = PersonValidator.LastNameIsInvalidMessage,
            MinimumLength = PersonValidator.LastNameMinLength)]
        public string LastName { get; set; }

        [Display(Name = "Display name")]
        [Required(ErrorMessage = "Display name is required.")]
        [StringLength(PersonValidator.DisplayNameMaxLength, 
            ErrorMessage = PersonValidator.DisplayNameIsInvalidMessage,
            MinimumLength = PersonValidator.DisplayNameMinLength)]
        public string DisplayName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = PersonValidator.EmailIsInvalidMessage)]
        public string Email { get; set; }
    }
}
