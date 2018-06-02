using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.UseCases.Boundary.Requests
{
    public class AuthorizedIdRequest : AuthorizedRequest
    {
        public AuthorizedIdRequest(string userId) : base(userId)
        {
        }

        public Identity Id { get; set; }

        protected override void ValidateCore()
        {
            if (Id.IsBlankIdentity())
                AddError("", "Identity is invalid.");
        }
    }
}
