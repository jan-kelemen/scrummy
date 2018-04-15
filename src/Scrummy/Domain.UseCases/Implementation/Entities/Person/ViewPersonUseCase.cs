using System;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Exceptions;
using Scrummy.Domain.Repositories.Interfaces.Entities;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Exceptions;
using Scrummy.Domain.UseCases.Interfaces.Entities.Person;

namespace Scrummy.Domain.UseCases.Implementation.Entities.Person
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

            try
            {
                var entity = _personRepository.ReadPerson(Identity.FromString(request.Id));
                return ToResponseModel(entity);
            }
            catch (EntityNotFoundException)
            {
                throw new UseCaseException("Entity not found.");
            }
            catch (Exception)
            {
                throw new UseCaseException("What?");
            }
        }

        private ViewPersonResponse ToResponseModel(Core.Entities.Person person)
        {
            return new ViewPersonResponse("Person loaded successfully.")
            {
                Id = person.Id.ToString(),
                FirstName = person.FirstName,
                LastName = person.LastName,
                DisplayName = person.DisplayName,
                Email = person.DisplayName,
            };
        }
    }
}
