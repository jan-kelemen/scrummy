namespace Scrummy.Domain.UseCases.Boundary
{
    public abstract class BaseResponse
    {
        protected BaseResponse(string message = null)
        {
            Message = message;
        }

        public string Message { get; }
    }
}