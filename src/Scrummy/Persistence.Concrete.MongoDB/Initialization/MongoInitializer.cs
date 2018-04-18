using Scrummy.Domain.Repositories;

namespace Scrummy.Persistence.Concrete.MongoDB.Initialization
{
    public static class MongoInitializer
    {
        public static IRepositoryProvider Initialize(string connectionString, string databaseName)
        {
            var database = new ScrummyDatabase(connectionString, databaseName);
            return new RepositoryProvider(database);
        }
}
}
