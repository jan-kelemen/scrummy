using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Exceptions;

namespace Scrummy.Domain.Core.Validators.Entities
{
    public static class PersonValidator
    {
        public const string FirstNameErrorKey = nameof(Person.FirstName);
        public const int FirstNameMinLength = 1;
        public const int FirstNameMaxLength = 200;
        public const string FirstNameIsInvalidMessage = "First name '{0}' is invalid.";

        public const string LastNameErrorKey = nameof(Person.LastName);
        public const int LastNameMinLength = 1;
        public const int LastNameMaxLength = 200;
        public const string LastNameIsInvalidMessage = "Last name '{0}' is invalid.";

        public const string DisplayNameErrorKey = nameof(Person.DisplayName);
        public const int DisplayNameMinLength = 1;
        public const int DisplayNameMaxLength = 401;
        public const string DisplayNameIsInvalidMessage = "Display name '{0}' is invalid.";

        public const string EmailErrorKey = nameof(Person.Email);
        public const int EmailMinLength = 3;
        public const int EmailMaxLength = 254;
        public const string EmailIsInvalidMessage = "Email '{0}' is invalid.";

        public static bool ValidateFirstName(string firstName) =>
            TextValidator.CheckIfContentIsBetweenSpecifiedLength(firstName, FirstNameMinLength, FirstNameMaxLength);

        public static bool ValidateLastName(string lastName) =>
            TextValidator.CheckIfContentIsBetweenSpecifiedLength(lastName, LastNameMinLength, LastNameMaxLength);

        public static bool ValidateDisplayName(string displayName) => 
            TextValidator.CheckIfContentIsBetweenSpecifiedLength(displayName, DisplayNameMinLength, DisplayNameMaxLength);

        public static bool ValidateEmail(string email) =>
            TextValidator.CheckIfContentIsBetweenSpecifiedLength(email, EmailMinLength, EmailMaxLength);

        public static void CheckFirstName<T>(T person, string firstName) where T : Person
        {
            if(!ValidateFirstName(firstName)) throw CreateException(person, FirstNameErrorKey, FirstNameIsInvalidMessage, firstName);
        }

        public static void CheckLastName<T>(T persion, string lastName) where T : Person
        {
            if(!ValidateLastName(lastName)) throw CreateException(persion, LastNameErrorKey, LastNameIsInvalidMessage, lastName);
        }

        public static void CheckDisplayName<T>(T persion, string displayName) where T : Person
        {
            if (!ValidateDisplayName(displayName)) throw CreateException(persion, DisplayNameErrorKey, DisplayNameIsInvalidMessage, displayName);
        }

        public static void CheckEmail<T>(T persion, string email) where T : Person
        {
            if (!ValidateEmail(email)) throw CreateException(persion, EmailErrorKey, EmailIsInvalidMessage, email);
        }

        private static EntityException CreateException<T>(T entity, string errorKey, string message, params object[] values) where T : Person
        {
            return new EntityException
            {
                EntityName = nameof(T),
                EntityIdentity = entity.Id,
                ErrorKey = errorKey,
                ErrorMessage = string.Format(message, values),
            };
        }
    }
}
