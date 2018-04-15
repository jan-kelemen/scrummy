using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Validators.Entities;
using Scrummy.Domain.UseCases.Boundary;

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
            if (!PersonValidator.ValidateFirstName(FirstName))
            {
                AddError(PersonValidator.FirstNameErrorKey, PersonValidator.FirstNameIsInvalidMessage, FirstName);
            }

            if (!PersonValidator.ValidateLastName(LastName))
            {
                AddError(PersonValidator.LastNameErrorKey, PersonValidator.LastNameIsInvalidMessage, LastName);
            }

            if (!PersonValidator.ValidateDisplayName(DisplayName))
            {
                AddError(PersonValidator.DisplayNameErrorKey, PersonValidator.DisplayNameIsInvalidMessage, DisplayName);
            }

            if (!PersonValidator.ValidateEmail(Email))
            {
                AddError(PersonValidator.EmailErrorKey, PersonValidator.EmailIsInvalidMessage, Email);
            }
        }
    }

    public class CreatePersonResponse : BaseResponse
    {
        public CreatePersonResponse(string message) : base(message) { }

        public Identity Id { get; set; }
    }

    public interface ICreatePersonUseCase
    {
        CreatePersonResponse Execute(CreatePersonRequest request);
    }
}
