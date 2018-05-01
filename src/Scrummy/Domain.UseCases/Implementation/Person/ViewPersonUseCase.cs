using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Domain.UseCases.Implementation.Person
{
    internal class ViewPersonUseCase : IViewPersonUseCase
    {
        private readonly IPersonRepository _personRepository;

        public ViewPersonUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public ViewPersonResponse Execute(ViewPersonRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = _personRepository.Read(request.Id);

            return new ViewPersonResponse
            {
                Id = request.Id,
                DisplayName = entity.DisplayName,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                IsSameAsPersonWhoRequested = request.Id.Id == request.UserId,
            };
        }
    }
}
