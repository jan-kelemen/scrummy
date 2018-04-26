namespace Scrummy.Domain.UseCases.Boundary.Responses
{
    public abstract class BaseResponse
    {
        protected BaseResponse(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}