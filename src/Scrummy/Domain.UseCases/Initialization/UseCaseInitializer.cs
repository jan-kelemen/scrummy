using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Implementation;

namespace Scrummy.Domain.UseCases.Initialization
{
    public static class UseCaseInitializer
    {
        public static IUseCaseFactoryProvider Initialize(IRepositoryProvider repositoryProvider)
        {
            return new UseCaseFactoryProvider(repositoryProvider);
        }
    }
}
