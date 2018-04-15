using Scrummy.Domain.Repositories.Factories;
using Scrummy.Domain.UseCases.Interfaces;

namespace Scrummy.Domain.UseCases.Implementation
{
    internal class UseCaseFactory : IUseCaseFactory
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public UseCaseFactory(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
    }
}
