using Scrummy.Domain.UseCases.Boundary;

using PersonValidation = Scrummy.Domain.Core.Entities.Person.Validation;

namespace Scrummy.Domain.UseCases.Interfaces.Entities.Person
{
    public class CreatePersonRequest : BaseRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

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
        }
    }

    public class CreatePersonResponse : BaseResponse
    {
        public CreatePersonResponse(string message) : base(message) { }

        public string Id { get; set; }
    }

    public interface ICreatePersonUseCase
    {
        CreatePersonResponse Execute(CreatePersonRequest request);
    }
}
