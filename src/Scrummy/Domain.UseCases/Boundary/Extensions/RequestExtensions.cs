using Scrummy.Domain.UseCases.Exceptions;
using Scrummy.Domain.UseCases.Exceptions.Boundary;

namespace Scrummy.Domain.UseCases.Boundary.Extensions
{
    public static class RequestExtensions
    {
        public static void ThrowExceptionIfInvalid(this BaseRequest request)
        {
            if (!request)
            {
                throw new InvalidRequestException();
            }
        }
    }
}
