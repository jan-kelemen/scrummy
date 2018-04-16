using System;
using Scrummy.Domain.Repositories;
using Scrummy.Persistence.Concrete.MongoDB.Initialization;

namespace Scrummy.Persistence.Infrastructure.Initialization
{
    public static class PersistenceInitializer
    {
        public static IRepositoryFactoryProvider Initialize(SupportedPersistenceType type)
        {
            switch (type)
            {
                case SupportedPersistenceType.MongoDB:
                    return MongoInitializer.Initialize("mongodb://localhost:27017", "Scrummy");
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
