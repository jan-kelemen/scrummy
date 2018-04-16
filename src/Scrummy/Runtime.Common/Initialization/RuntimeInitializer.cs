using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases;
using Scrummy.Domain.UseCases.Initialization;
using Scrummy.Domain.UseCases.Interfaces;
using Scrummy.Persistence.Infrastructure;
using Scrummy.Persistence.Infrastructure.Initialization;

namespace Scrummy.Runtime.Common.Initialization
{
    public static class RuntimeInitializer
    {
        public static IRepositoryFactoryProvider RepositoryFactoryProvider { get; private set; }

        public static IUseCaseFactoryProvider UseCaseFactoryProvider { get; private set; }

        public static void Initialize()
        {
            InitializePersistence();
            InitializeUseCases();
        }

        private static void InitializePersistence()
        {
            RepositoryFactoryProvider = PersistenceInitializer.Initialize(SupportedPersistenceType.MongoDB);
        }

        private static void InitializeUseCases()
        {
            UseCaseFactoryProvider = UseCaseInitializer.Initialize(RepositoryFactoryProvider);
        }
    }
}
