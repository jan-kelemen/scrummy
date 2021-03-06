﻿using Scrummy.Domain.Repositories;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Persistence.Concrete.MongoDB.Repositories;

namespace Scrummy.Persistence.Concrete.MongoDB
{
    internal class RepositoryProvider : IRepositoryProvider
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly ScrummyDatabase _database;

        public RepositoryProvider(ScrummyDatabase database)
        {
            _database = database;

            Person = new PersonRepository(_database.PersonCollection);
            Project = new ProjectRepository(_database.ProjectCollection, _database.MeetingCollection, _database.SprintCollection, _database.WorkTaskCollection, _database.DocumentCollection);
            Team = new TeamRepository(_database.TeamCollection);
            Meeting = new MeetingRepository(_database.MeetingCollection);
            Sprint = new SprintRepository(_database.SprintCollection, _database.ProjectCollection);
            WorkTask = new WorkTaskRepository(_database.WorkTaskCollection);
            Document = new DocumentRepository(_database.DocumentCollection, _database.SprintCollection, _database.MeetingCollection, _database.WorkTaskCollection);
        }

        public IPersonRepository Person { get; }

        public IProjectRepository Project { get; }

        public ITeamRepository Team { get; }

        public IMeetingRepository Meeting { get; }

        public ISprintRepository Sprint { get; }

        public IWorkTaskRepository WorkTask { get; }

        public IDocumentRepository Document { get; }
    }
}
