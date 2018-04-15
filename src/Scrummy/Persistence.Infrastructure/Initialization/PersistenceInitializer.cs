using System;
using Scrummy.Domain.Repositories.Factories;
using Scrummy.Persistence.Concrete.MongoDB.Initialization;

namespace Scrummy.Persistence.Infrastructure.Initialization
{
    public static class PersistenceInitializer
    {
        public static IRepositoryFactory Initialize(SupportedPersistenceType type)
        {
            switch (type)
            {
                case SupportedPersistenceType.MongoDB:
                    return MongoInitializer.Initialize();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
