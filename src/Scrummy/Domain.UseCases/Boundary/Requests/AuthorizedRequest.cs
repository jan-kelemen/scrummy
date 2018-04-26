using System.Collections.Generic;
using Scrummy.Domain.Core.Validators;

namespace Scrummy.Domain.UseCases.Boundary.Requests
{
    public abstract class AuthorizedRequest : BaseRequest
    {
        protected AuthorizedRequest(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }

        public override bool Validate()
        {
            Errors = new Dictionary<string, string>();

            if (!TextValidator.ValidateThatTextCanRepresentIdentity(UserId))
            {
                AddError("", "User identity is invalid.");
            }

            ValidateCore();
            return this;
        }
    }
}
