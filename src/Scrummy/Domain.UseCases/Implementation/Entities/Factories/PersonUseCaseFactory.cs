using Scrummy.Domain.Repositories.Factories;
using Scrummy.Domain.UseCases.Implementation.Entities.Person;
using Scrummy.Domain.UseCases.Interfaces.Entities.Factories;
using Scrummy.Domain.UseCases.Interfaces.Entities.Person;

namespace Scrummy.Domain.UseCases.Implementation.Entities.Factories
{
    public class PersonUseCaseFactory : IPersonUseCaseFactory
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public PersonUseCaseFactory(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public ICreatePersonUseCase CreatePerson => 
            new CreatePersonUseCase(_repositoryFactory.PersonRepository);

        public IViewPersonUseCase ViewPerson =>
            new ViewPersonUseCase(_repositoryFactory.PersonRepository);
    }
}
