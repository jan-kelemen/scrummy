using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Implementation.Entities.Factories;
using Scrummy.Domain.UseCases.Interfaces.Entities.Factories;

namespace Scrummy.Domain.UseCases.Implementation
{
    internal class UseCaseFactoryProvider : IUseCaseFactoryProvider
    {
        private readonly IRepositoryFactoryProvider _repositoryFactoryProvider;

        public UseCaseFactoryProvider(IRepositoryFactoryProvider repositoryFactoryProvider)
        {
            _repositoryFactoryProvider = repositoryFactoryProvider;

            Person = new PersonUseCaseFactory(_repositoryFactoryProvider);
        }

        public IPersonUseCaseFactory Person { get; }
    }
}
