using System;
using Scrummy.Domain.Repositories.Exceptions;
using Scrummy.Domain.Repositories.Interfaces.Entities;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Exceptions;
using Scrummy.Domain.UseCases.Interfaces.Entities.Person;

namespace Scrummy.Domain.UseCases.Implementation.Entities.Person
{
    internal class CreatePersonUseCase : ICreatePersonUseCase
    {
        private readonly IPersonRepository _personRepository;

        public CreatePersonUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public CreatePersonResponse Execute(CreatePersonRequest request)
        {
            request.ThrowExceptionIfInvalid();

            try
            {
                if (_personRepository.CheckIfEmailExists(request.Email))
                {
                    throw new UseCaseException("Person with same email already exists.");
                }

                var entity = ToDomainEntity(request);
                var result = _personRepository.Create(entity);

                return new CreatePersonResponse(string.Format("Person {0} sucessfuly created.", entity.DisplayName))
                {
                    Id = result.ToString(),
                };
            }
            catch (InvalidEntityException)
            {
                throw new UseCaseException("Entity already exists.");
            }
            catch (Exception)
            {
                throw new UseCaseException("What?");
            }
        }

        private Core.Entities.Person ToDomainEntity(CreatePersonRequest request)
        {
            return new Core.Entities.Person(
                _personRepository.GenerateNewIdentity(),
                request.FirstName,
                request.LastName,
                request.DisplayName,
                request.Email);
        }
    }
}
