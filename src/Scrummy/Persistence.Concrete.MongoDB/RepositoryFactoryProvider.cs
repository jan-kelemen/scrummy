using Scrummy.Domain.Repositories;
using Scrummy.Domain.Repositories.Interfaces.Entities;
using Scrummy.Persistence.Concrete.MongoDB.Repositories;

namespace Scrummy.Persistence.Concrete.MongoDB
{
    internal class RepositoryFactoryProvider : IRepositoryFactoryProvider
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly ScrummyDatabase _database;

        public RepositoryFactoryProvider(ScrummyDatabase database)
        {
            _database = database;

            Person = new PersonRepository(_database.PersonCollection);
            Project = new ProjectRepository(_database.ProjectCollection, _database.TeamCollection);
            Team = new TeamRepository(_database.TeamCollection);
            Meeting = new MeetingRepository(_database.MeetingCollection);
            Sprint = new SprintRepository(_database.SprintCollection);
        }

        public IPersonRepository Person { get; }

        public IProjectRepository Project { get; }

        public ITeamRepository Team { get; }

        public IMeetingRepository Meeting { get; }

        public ISprintRepository Sprint { get; }
    }
}
