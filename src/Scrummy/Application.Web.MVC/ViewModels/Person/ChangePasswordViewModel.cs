using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using PersonValidation = Scrummy.Domain.Core.Entities.Person.Validation;

namespace Scrummy.Application.Web.MVC.ViewModels.Person
{
    public class ChangePasswordViewModel
    {
        [HiddenInput]
        public string Id { get; set; }

        [Display(Name = "Old password")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(int.MaxValue,
            ErrorMessage = PersonValidation.PasswordIsInvalidMessage,
            MinimumLength = PersonValidation.PasswordMinLength)]
        public string OldPassword { get; set; }

        [Display(Name = "New password")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(int.MaxValue,
            ErrorMessage = PersonValidation.PasswordIsInvalidMessage,
            MinimumLength = PersonValidation.PasswordMinLength)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(int.MaxValue,
            ErrorMessage = PersonValidation.PasswordIsInvalidMessage,
            MinimumLength = PersonValidation.PasswordMinLength)]
        public string ConfirmedPassword { get; set; }
    }
}
