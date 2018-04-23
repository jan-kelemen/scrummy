using System.ComponentModel.DataAnnotations;
using PersonValidation = Scrummy.Domain.Core.Entities.Person.Validation;

namespace Scrummy.Application.Web.MVC.ViewModels.Home
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = PersonValidation.EmailIsInvalidMessage)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(int.MaxValue,
            ErrorMessage = PersonValidation.PasswordIsInvalidMessage,
            MinimumLength = PersonValidation.PasswordMinLength)]
        public string Password { get; set; }
    }
}
