using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Implementation.Entities.Factories;
using Scrummy.Domain.UseCases.Interfaces.Entities.Factories;

namespace Scrummy.Domain.UseCases.Implementation
{
    internal class UseCaseFactoryProvider : IUseCaseFactoryProvider
    {
        private readonly IRepositoryProvider _repositoryProvider;

        public UseCaseFactoryProvider(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;

            Person = new PersonUseCaseFactory(_repositoryProvider);
        }

        public IPersonUseCaseFactory Person { get; }
    }
}
