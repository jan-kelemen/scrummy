using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Implementation.Factories;
using Scrummy.Domain.UseCases.Interfaces.Factories;

namespace Scrummy.Domain.UseCases.Implementation
{
    internal class UseCaseFactoryProvider : IUseCaseFactoryProvider
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly IRepositoryProvider _repositoryProvider;

        public UseCaseFactoryProvider(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;

            Person = new PersonUseCaseFactory(_repositoryProvider);
            Project = new ProjectUseCaseFactory(_repositoryProvider);
        }

        public IPersonUseCaseFactory Person { get; }
        public IProjectUseCaseFactory Project { get; }
    }
}
