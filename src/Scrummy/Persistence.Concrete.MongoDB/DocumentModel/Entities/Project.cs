using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Scrummy.Domain.Core.Entities;

namespace Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities
{
    internal class Project : BaseEntity
    {
        internal class BacklogHistoryRecord
        {
            public int ToDoTasks { get; set; }

            public int ReadyTasks { get; set; }

            public int InSprintTasks { get; set; }

            public int DoneTasks { get; set; }
        }

        internal class BacklogItem
        {
            public ObjectId WorkTaskId { get; set; }

            [BsonRepresentation(BsonType.String)]
            public ProductBacklog.WorkTaskStatus Status { get; set; }
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> DefinitionOfDoneConditions { get; set; }

        public TeamHistoryRecord CurrentTeam { get; set; }

        public IEnumerable<TeamHistoryRecord> TeamHistory { get; set; }

        public IEnumerable<BacklogItem> Backlog { get; set; }

        public IEnumerable<BacklogHistoryRecord> BacklogHistory { get; set; }
    }

    internal class TeamHistoryRecord
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public ObjectId TeamId { get; set; }
    }
}
