using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using PersonValidation = Scrummy.Domain.Core.Entities.Person.Validation;

namespace Scrummy.Domain.UseCases.Interfaces.Person
{
    public class EditPersonRequest : AuthorizedRequest
    {
        public EditPersonRequest(string userId) : base(userId)
        {
        }

        public Identity ForUserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        protected override void ValidateCore()
        {
            if (ForUserId.IsBlankIdentity())
            {
                AddError("", "User identity is invalid.");
            }

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

    public class EditPersonResponse : BaseResponse
    {
        public EditPersonResponse(string message) : base(message)
        {
        }

        public Identity Id { get; set; }
    }

    public interface IEditPersonUseCase
    {
        EditPersonResponse Execute(EditPersonRequest request);
    }
}
