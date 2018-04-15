using Scrummy.Domain.UseCases.Exceptions.Boundary;

namespace Scrummy.Domain.UseCases.Boundary.Extensions
{
    public static class RequestExtensions
    {
        public static void ThrowExceptionIfInvalid(this BaseRequest request)
        {
            if (!request.Validate())
            {
                throw new InvalidRequestException();
            }
        }
    }
}
