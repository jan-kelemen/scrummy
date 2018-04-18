using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Implementation.Entities.Person;
using Scrummy.Domain.UseCases.Interfaces.Entities.Factories;
using Scrummy.Domain.UseCases.Interfaces.Entities.Person;

namespace Scrummy.Domain.UseCases.Implementation.Entities.Factories
{
    public class PersonUseCaseFactory : IPersonUseCaseFactory
    {
        private readonly IRepositoryProvider _repositoryProvider;

        public PersonUseCaseFactory(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }

        public ICreatePersonUseCase Create => 
            new CreatePersonUseCase(_repositoryProvider.Person);

        public IViewPersonUseCase View =>
            new ViewPersonUseCase(_repositoryProvider.Person);
    }
}
