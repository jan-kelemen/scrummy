using Scrummy.Domain.Repositories.Factories;
using Scrummy.Domain.UseCases.Initialization;
using Scrummy.Domain.UseCases.Interfaces;
using Scrummy.Persistence.Infrastructure;
using Scrummy.Persistence.Infrastructure.Initialization;
using Scrummy.Runtime.Common.Registry;

namespace Scrummy.Runtime.Common.Initialization
{
    public static class RuntimeInitializer
    {
        public static IRepositoryFactory RepositoryFactory { get; private set; }

        public static IUseCaseFactoryProvider UseCaseFactoryProvider { get; private set; }

        public static void Initialize()
        {
            RegistryProvider.Data = new RegistryData();

            InitializePersistence();
            InitializeUseCases();
        }

        private static void InitializePersistence()
        {
            RepositoryFactory = PersistenceInitializer.Initialize(SupportedPersistenceType.MongoDB);

            RegistryProvider.Data.Register(RepositoryFactory);
        }

        private static void InitializeUseCases()
        {
            UseCaseFactoryProvider = UseCaseInitializer.Initialize(RegistryProvider.Data.Get<IRepositoryFactory>());

            RegistryProvider.Data.Register(UseCaseFactoryProvider);
        }
    }
}
