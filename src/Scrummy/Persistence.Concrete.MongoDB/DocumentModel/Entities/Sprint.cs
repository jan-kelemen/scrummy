using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Enumerations;

namespace Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities
{
    internal class Sprint : BaseEntity
    {
        internal class BacklogHistoryRecord
        {
            public int ToDoTasks { get; set; }

            public int InProgressTasks { get; set; }

            public int DoneTasks { get; set; }
        }

        internal class BacklogItem
        {
            public ObjectId WorkTaskId { get; set; }

            public ObjectId ParentTaskId { get; set; }

            [BsonRepresentation(BsonType.String)]
            public SprintBacklog.WorkTaskStatus Status { get; set; }
        }

        public ObjectId ProjectId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Goal { get; set; }

        [BsonRepresentation(BsonType.String)]
        public SprintStatus Status { get; set; }

        public IEnumerable<ObjectId> PlannedTasks { get; set; }

        public IEnumerable<BacklogItem> Backlog { get; set; }

        public IEnumerable<BacklogHistoryRecord> BacklogHistory { get; set; }
    }
}
