using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Implementation.Entities.Person;
using Scrummy.Domain.UseCases.Interfaces.Entities.Factories;
using Scrummy.Domain.UseCases.Interfaces.Entities.Person;

namespace Scrummy.Domain.UseCases.Implementation.Entities.Factories
{
    public class PersonUseCaseFactory : IPersonUseCaseFactory
    {
        private readonly IRepositoryFactoryProvider _repositoryFactoryProvider;

        public PersonUseCaseFactory(IRepositoryFactoryProvider repositoryFactoryProvider)
        {
            _repositoryFactoryProvider = repositoryFactoryProvider;
        }

        public ICreatePersonUseCase Create => 
            new CreatePersonUseCase(_repositoryFactoryProvider.Person);

        public IViewPersonUseCase View =>
            new ViewPersonUseCase(_repositoryFactoryProvider.Person);
    }
}
