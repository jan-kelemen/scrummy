using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Scrummy.Domain.Repositories.Factories;
using Scrummy.Domain.UseCases.Initialization;
using Scrummy.Domain.UseCases.Interfaces;
using Scrummy.Persistence.Infrastructure;
using Scrummy.Persistence.Infrastructure.Initialization;

namespace Runtime.Common.Initialization
{
    public static class RuntimeInitializer
    {
        public static IRepositoryFactory RepositoryFactory { get; private set; }

        public static IUseCaseFactoryProvider UseCaseFactoryProvider { get; private set; }

        public static void Initialize()
        {
            InitializePersistence();
            InitializeUseCases();
        }

        private static void InitializePersistence()
        {
            RepositoryFactory = PersistenceInitializer.Initialize(SupportedPersistenceType.MongoDB);
        }

        private static void InitializeUseCases()
        {
            UseCaseFactoryProvider = UseCaseInitializer.Initialize(RepositoryFactory);
        }
    }
}
