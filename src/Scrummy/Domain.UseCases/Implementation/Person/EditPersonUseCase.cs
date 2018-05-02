using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Exceptions;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Domain.UseCases.Implementation.Person
{
    internal class EditPersonUseCase : IEditPersonUseCase
    {
        private readonly IPersonRepository _personRepository;

        public EditPersonUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public EditPersonResponse Execute(EditPersonRequest request)
        {
            request.ThrowExceptionIfInvalid();
            if (request.ForUserId != Identity.FromString(request.UserId))
                throw new UseCaseException("Can't change data of another user.");

            var entity = _personRepository.Read(request.ForUserId);

            if(entity.Email != request.Email && _personRepository.CheckIfEmailExists(request.Email))
                throw new UseCaseException("Person with same email already exists.");

            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.DisplayName = request.DisplayName;
            entity.Email = request.Email;

            _personRepository.Update(entity);

            return new EditPersonResponse("User updated successfully.")
            {
                Id = entity.Id,
            };
        }
    }
}
