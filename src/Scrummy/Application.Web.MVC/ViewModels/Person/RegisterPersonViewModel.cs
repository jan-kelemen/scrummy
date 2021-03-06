﻿using System.ComponentModel.DataAnnotations;
using PersonValidation = Scrummy.Domain.Core.Entities.Person.Validation;

namespace Scrummy.Application.Web.MVC.ViewModels.Person
{
    public class RegisterPersonViewModel
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name message is required.")]
        [StringLength(PersonValidation.FirstNameMaxLength,
            ErrorMessage = PersonValidation.FirstNameIsInvalidMessage,
            MinimumLength = PersonValidation.FirstNameMinLength)]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(PersonValidation.LastNameMaxLength,
            ErrorMessage = PersonValidation.LastNameIsInvalidMessage,
            MinimumLength = PersonValidation.LastNameMinLength)]
        public string LastName { get; set; }

        [Display(Name = "Display name")]
        [Required(ErrorMessage = "Display name is required.")]
        [StringLength(PersonValidation.DisplayNameMaxLength, 
            ErrorMessage = PersonValidation.DisplayNameIsInvalidMessage,
            MinimumLength = PersonValidation.DisplayNameMinLength)]
        public string DisplayName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = PersonValidation.EmailIsInvalidMessage)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(int.MaxValue, 
            ErrorMessage = PersonValidation.PasswordIsInvalidMessage, 
            MinimumLength = PersonValidation.PasswordMinLength)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(int.MaxValue,
            ErrorMessage = PersonValidation.PasswordIsInvalidMessage,
            MinimumLength = PersonValidation.PasswordMinLength)]
        public string ConfirmedPassword { get; set; }
    }
}
