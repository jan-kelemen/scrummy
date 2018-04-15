namespace Scrummy.Domain.UseCases.Boundary
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