using Scrummy.Domain.Repositories;
using Scrummy.Domain.Repositories.Interfaces.Entities;
using Scrummy.Persistence.Concrete.MongoDB.Repositories;

namespace Scrummy.Persistence.Concrete.MongoDB
{
    internal class RepositoryFactoryProvider : IRepositoryFactoryProvider
    {
        private readonly ScrummyDatabase _database;

        public RepositoryFactoryProvider(ScrummyDatabase database)
        {
            _database = database;

            Person = new PersonRepository(_database.PersonCollection);
            Project = new ProjectRepository(_database.ProjectCollection);
            Meeting = new MeetingRepository(_database.MeetingCollection);
        }

        public IPersonRepository Person { get; }

        public IProjectRepository Project { get; }

        public IMeetingRepository Meeting { get; }
    }
}
