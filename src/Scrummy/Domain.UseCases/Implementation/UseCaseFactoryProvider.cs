using Scrummy.Domain.Repositories.Factories;
using Scrummy.Domain.UseCases.Implementation.Entities.Factories;
using Scrummy.Domain.UseCases.Interfaces;
using Scrummy.Domain.UseCases.Interfaces.Entities.Factories;

namespace Scrummy.Domain.UseCases.Implementation
{
    internal class UseCaseFactoryProvider : IUseCaseFactoryProvider
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public UseCaseFactoryProvider(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;

            PersonUseCaseFactory = new PersonUseCaseFactory(_repositoryFactory);
        }

        public IPersonUseCaseFactory PersonUseCaseFactory { get; }
    }
}
