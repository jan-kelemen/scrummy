using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Factories;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Implementation.Factories
{
    internal class ProjectUseCaseFactory : IProjectUseCaseFactory
    {
        private readonly IRepositoryProvider _repositoryProvider;

        public ProjectUseCaseFactory(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }

        public ICreateProjectUseCase Create { get; } = null;
    }
}
