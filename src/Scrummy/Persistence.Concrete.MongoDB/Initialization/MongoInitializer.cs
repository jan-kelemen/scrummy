using Scrummy.Domain.Repositories.Factories;
using Scrummy.Persistence.Concrete.MongoDB.Factories;

namespace Scrummy.Persistence.Concrete.MongoDB.Initialization
{
    public static class MongoInitializer
    {
        public static IRepositoryFactory Initialize(string connectionString, string databaseName)
        {
            var database = new ScrummyDatabase(connectionString, databaseName);
            return new RepositoryFactory(database);
        }
}
}
