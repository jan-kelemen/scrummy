using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.UseCases.Boundary.Responses
{
    public class ConfirmationResponse : BaseResponse
    {
        public ConfirmationResponse(string message) : base(message)
        {
        }

        public Identity Id { get; set; }
    }
}
