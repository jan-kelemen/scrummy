using Scrummy.Domain.Repositories.Factories;
using Scrummy.Domain.UseCases.Implementation;
using Scrummy.Domain.UseCases.Interfaces;

namespace Scrummy.Domain.UseCases.Initialization
{
    public static class UseCaseInitializer
    {
        public static IUseCaseFactoryProvider Initialize(IRepositoryFactory repositoryFactory)
        {
            return new UseCaseFactoryProvider(repositoryFactory);
        }
    }
}
