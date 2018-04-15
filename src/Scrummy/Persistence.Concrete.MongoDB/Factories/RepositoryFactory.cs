using Scrummy.Domain.Repositories.Factories;
using Scrummy.Domain.Repositories.Interfaces.Entities;
using Scrummy.Persistence.Concrete.MongoDB.Repositories;

namespace Scrummy.Persistence.Concrete.MongoDB.Factories
{
    internal class RepositoryFactory : IRepositoryFactory
    {
        private readonly ScrummyDatabase _database;

        public RepositoryFactory(ScrummyDatabase database)
        {
            _database = database;
        }

        public IPersonRepository PersonRepository => new PersonRepository(_database.PersonCollection);
    }
}
