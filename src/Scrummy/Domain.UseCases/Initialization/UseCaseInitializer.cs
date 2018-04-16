using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Implementation;
using Scrummy.Domain.UseCases.Interfaces;

namespace Scrummy.Domain.UseCases.Initialization
{
    public static class UseCaseInitializer
    {
        public static IUseCaseFactoryProvider Initialize(IRepositoryFactoryProvider repositoryFactoryProvider)
        {
            return new UseCaseFactoryProvider(repositoryFactoryProvider);
        }
    }
}
