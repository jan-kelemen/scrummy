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
        public static IRepositoryProvider RepositoryProvider { get; private set; }

        public static IUseCaseFactoryProvider UseCaseFactoryProvider { get; private set; }

        public static void Initialize()
        {
            InitializePersistence();
            InitializeUseCases();
        }

        private static void InitializePersistence()
        {
            RepositoryProvider = PersistenceInitializer.Initialize(SupportedPersistenceType.MongoDB);
        }

        private static void InitializeUseCases()
        {
            UseCaseFactoryProvider = UseCaseInitializer.Initialize(RepositoryProvider);
        }
    }
}
