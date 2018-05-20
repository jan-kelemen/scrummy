using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Utilities;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Exceptions;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Domain.UseCases.Implementation.Person
{
    internal class CreatePersonUseCase : ICreatePersonUseCase
    {
        private readonly IPersonRepository _personRepository;

        public CreatePersonUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public ConfirmationResponse Execute(CreatePersonRequest request)
        {
            request.ThrowExceptionIfInvalid();

            if (_personRepository.CheckIfEmailExists(request.Email))
                throw new UseCaseException("Person with same email already exists.");

            var domainEntity = ToDomainEntity(request);
            var identity = _personRepository.Create(domainEntity);

            return CreateResponse(identity, domainEntity.DisplayName);
        }

        private Core.Entities.Person ToDomainEntity(CreatePersonRequest request)
        {
            return new Core.Entities.Person(
                _personRepository.GenerateNewIdentity(),
                request.FirstName,
                request.LastName,
                request.DisplayName,
                request.Email,
                SecurityUtility.HashPassword(request.Password)
            );
        }

        private ConfirmationResponse CreateResponse(Identity id, string displayName)
        {
            return new ConfirmationResponse(string.Format("Person {0} sucessfuly created.", displayName))
            {
                Id = id
            };
        }
    }
}
