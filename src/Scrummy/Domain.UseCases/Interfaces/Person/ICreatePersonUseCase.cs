using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using PersonValidation = Scrummy.Domain.Core.Entities.Person.Validation;

namespace Scrummy.Domain.UseCases.Interfaces.Person
{
    public class CreatePersonRequest : BaseRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        protected override void ValidateCore()
        {
            if (!PersonValidation.ValidateFirstName(FirstName))
            {
                AddError(PersonValidation.FirstNameErrorKey, PersonValidation.FirstNameIsInvalidMessage);
            }

            if (!PersonValidation.ValidateLastName(LastName))
            {
                AddError(PersonValidation.LastNameErrorKey, PersonValidation.LastNameIsInvalidMessage);
            }

            if (!PersonValidation.ValidateDisplayName(DisplayName))
            {
                AddError(PersonValidation.DisplayNameErrorKey, PersonValidation.DisplayNameIsInvalidMessage);
            }

            if (!PersonValidation.ValidateEmail(Email))
            {
                AddError(PersonValidation.EmailErrorKey, PersonValidation.EmailIsInvalidMessage);
            }

            if (!PersonValidation.ValidatePassword(Password))
            {
                AddError(PersonValidation.PasswordErrorKey, PersonValidation.PasswordIsInvalidMessage);
            }
        }
    }

    public interface ICreatePersonUseCase
    {
        ConfirmationResponse Execute(CreatePersonRequest request);
    }
}
