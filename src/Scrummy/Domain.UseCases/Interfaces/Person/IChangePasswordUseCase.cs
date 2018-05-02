using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using PersonValidation = Scrummy.Domain.Core.Entities.Person.Validation;

namespace Scrummy.Domain.UseCases.Interfaces.Person
{
    public class ChangePasswordRequest : AuthorizedRequest
    {
        public ChangePasswordRequest(string userId) : base(userId)
        {
        }

        public Identity ForUserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        protected override void ValidateCore()
        {
            if (ForUserId.IsBlankIdentity())
            {
                AddError("", "User identity is invalid.");
            }

            if (!PersonValidation.ValidatePassword(OldPassword))
            {
                AddError(PersonValidation.PasswordErrorKey, PersonValidation.PasswordIsInvalidMessage);
            }

            if (!PersonValidation.ValidatePassword(NewPassword))
            {
                AddError(PersonValidation.PasswordErrorKey, PersonValidation.PasswordIsInvalidMessage);
            }
        }
    }

    public class ChangePasswordResponse : BaseResponse
    {
        public ChangePasswordResponse(string message) : base(message)
        {
        }

        public Identity Id { get; set; }
    }

    public interface IChangePasswordUseCase
    {
        ChangePasswordResponse Execute(ChangePasswordRequest request);
    }
}
