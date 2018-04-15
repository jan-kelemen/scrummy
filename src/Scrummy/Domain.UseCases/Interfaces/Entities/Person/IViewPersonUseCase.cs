using Scrummy.Domain.UseCases.Boundary;

namespace Scrummy.Domain.UseCases.Interfaces.Entities.Person
{
    public class ViewPersonRequest : BaseRequest
    {
        public string Id { get; set; }

        protected override void ValidateCore()
        {
            if (string.IsNullOrEmpty(Id))
            {
                AddError(nameof(Id), "Identity of the person is invalid.");
            }
        }
    }

    public class ViewPersonResponse : BaseResponse
    {
        public ViewPersonResponse(string message) : base(message)
        {
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }
    }

    public interface IViewPersonUseCase
    {
        ViewPersonResponse Execute(ViewPersonRequest request);
    }
}
