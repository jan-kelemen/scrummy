using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Utilities;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Exceptions;
using Scrummy.Domain.UseCases.Exceptions.Boundary;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Domain.UseCases.Implementation.Person
{
    internal class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly IPersonRepository _personRepository;

        public ChangePasswordUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public ChangePasswordResponse Execute(ChangePasswordRequest request)
        {
            request.ThrowExceptionIfInvalid();
            if (request.ForUserId != Identity.FromString(request.UserId))
                throw new UseCaseException("Can't change data of another user.");

            var entity = _personRepository.Read(request.ForUserId);
            var person = _personRepository.FindByEmailAndPasswordHash(entity.Email, SecurityUtility.HashPassword(request.OldPassword));
            if (person == null)
            {
                request.Errors.Add("", "Old password is invalid.");
                throw new InvalidRequestException { Errors = request.Errors };
            }

            person.PasswordHash = SecurityUtility.HashPassword(request.NewPassword);

            _personRepository.ChangePassword(person);

            return new ChangePasswordResponse("Password changed successfully.")
            {
                Id = person.Id,
            };
        }
    }
}
