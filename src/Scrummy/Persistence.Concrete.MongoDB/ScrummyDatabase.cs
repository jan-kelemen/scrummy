using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities;

namespace Scrummy.Persistence.Concrete.MongoDB
{
    internal class ScrummyDatabase
    {
        private static readonly IDictionary<Type, string> CollectionNameMap;

        static ScrummyDatabase()
        {
            CollectionNameMap = new Dictionary<Type, string>
            {
                { typeof(Person), "Persons" },
                { typeof(Project), "Projects" },
                { typeof(Team), "Teams" },
                { typeof(Meeting), "Meetings" },
                { typeof(Sprint), "Sprints" },
                { typeof(WorkTask), "WorkTasks" },
                { typeof(Document), "Documents" },
            };
        }

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly IMongoClient _client;

        private readonly IMongoDatabase _database;

        public ScrummyDatabase(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }

        public IMongoCollection<Person> PersonCollection => GetCollection<Person>();

        public IMongoCollection<Project> ProjectCollection => GetCollection<Project>();

        public IMongoCollection<Meeting> MeetingCollection => GetCollection<Meeting>();

        public IMongoCollection<Team> TeamCollection => GetCollection<Team>();

        public IMongoCollection<Sprint> SprintCollection => GetCollection<Sprint>();

        public IMongoCollection<WorkTask> WorkTaskCollection => GetCollection<WorkTask>();

        public IMongoCollection<Document> DocumentCollection => GetCollection<Document>();

        private IMongoCollection<T> GetCollection<T>() => _database.GetCollection<T>(CollectionNameMap[typeof(T)]);
    }
}
