using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Validators;

namespace Scrummy.Domain.Core.Entities
{
    public class Person : Entity<Person>
    {
        public class Validation
        {
            public const string FirstNameErrorKey = nameof(FirstName);
            public const int FirstNameMinLength = 1;
            public const int FirstNameMaxLength = 200;
            public const string FirstNameIsInvalidMessage = "First name is invalid.";

            public const string LastNameErrorKey = nameof(LastName);
            public const int LastNameMinLength = 1;
            public const int LastNameMaxLength = 200;
            public const string LastNameIsInvalidMessage = "Last name is invalid.";

            public const string DisplayNameErrorKey = nameof(DisplayName);
            public const int DisplayNameMinLength = 1;
            public const int DisplayNameMaxLength = 401;
            public const string DisplayNameIsInvalidMessage = "Display name is invalid.";

            public const string EmailErrorKey = nameof(Email);
            public const int EmailMinLength = 3;
            public const int EmailMaxLength = 254;
            public const string EmailIsInvalidMessage = "Email is invalid.";

            public static bool ValidateFirstName(string firstName) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(firstName, FirstNameMinLength, FirstNameMaxLength);

            public static bool ValidateLastName(string lastName) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(lastName, LastNameMinLength, LastNameMaxLength);

            public static bool ValidateDisplayName(string displayName) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(displayName, DisplayNameMinLength, DisplayNameMaxLength);

            public static bool ValidateEmail(string email) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(email, EmailMinLength, EmailMaxLength);
        }

        private string _firstName;
        private string _lastName;
        private string _displayName;
        private string _email;

        public Person(
            Identity id,
            string firstName,
            string lastName,
            string displayName,
            string email) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            DisplayName = displayName;
            Email = email;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = CheckFirstName(value);
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = CheckLastName(value);
        }

        public string DisplayName
        {
            get => _displayName;
            set => _displayName = CheckDisplayName(value);
        }

        public string Email
        {
            get => _email;
            set => _email = CheckEmail(value);
        }

        private string CheckFirstName(string firstName)
        {
            if (!Validation.ValidateFirstName(firstName))
                throw CreateEntityValidationException(Validation.FirstNameErrorKey, Validation.FirstNameIsInvalidMessage);
            return firstName;
        }

        private string CheckLastName(string lastName)
        {
            if (!Validation.ValidateLastName(lastName))
                throw CreateEntityValidationException(Validation.LastNameErrorKey, Validation.LastNameIsInvalidMessage);
            return lastName;
        }

        private string CheckDisplayName(string displayName)
        {
            if (!Validation.ValidateDisplayName(displayName))
                throw CreateEntityValidationException(Validation.DisplayNameErrorKey, Validation.DisplayNameIsInvalidMessage);
            return displayName;
        }

        private string CheckEmail(string email)
        {
            if (!Validation.ValidateEmail(email))
                throw CreateEntityValidationException(Validation.EmailErrorKey, Validation.EmailIsInvalidMessage);
            return email;
        }
    }
}
