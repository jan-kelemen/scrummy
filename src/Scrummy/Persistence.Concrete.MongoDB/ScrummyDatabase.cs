﻿using System;
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
                { typeof(Meeting), "Meetings" }
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

        public IMongoCollection<Person> PersonCollection => 
            _database.GetCollection<Person>(CollectionNameMap[typeof(Person)]);

        public IMongoCollection<Project> ProjectCollection =>
            _database.GetCollection<Project>(CollectionNameMap[typeof(Project)]);

        public IMongoCollection<Meeting> MeetingCollection =>
            _database.GetCollection<Meeting>(CollectionNameMap[typeof(Meeting)]);
    }
}
