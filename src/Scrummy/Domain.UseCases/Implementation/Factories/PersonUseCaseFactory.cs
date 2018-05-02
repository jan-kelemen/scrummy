using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Implementation.Person;
using Scrummy.Domain.UseCases.Interfaces.Factories;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Domain.UseCases.Implementation.Factories
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
            new ViewPersonUseCase(_repositoryProvider.Person, _repositoryProvider.Team);

        public IEditPersonUseCase Edit =>
            new EditPersonUseCase(_repositoryProvider.Person);

        public IViewCurrentWorkUseCase ViewCurrentWork => 
            new ViewCurrentWorkUseCase(_repositoryProvider);
    }
}
