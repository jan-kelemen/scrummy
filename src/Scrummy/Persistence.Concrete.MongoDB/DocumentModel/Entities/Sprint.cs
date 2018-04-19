using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Scrummy.Domain.Core.Entities;

namespace Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities
{
    internal class Sprint : BaseEntity
    {
        internal class BacklogHistoryRecord
        {
            public DateTime From { get; set; }

            public DateTime To { get; set; }

            public IEnumerable<BacklogItem> Tasks { get; set; }
        }

        internal class BacklogItem
        {
            public ObjectId WorkTaskId { get; set; }

            [BsonRepresentation(BsonType.String)]
            public SprintBacklog.WorkTaskStatus Status { get; set; }
        }

        public ObjectId ProjectId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Goal { get; set; }

        public IEnumerable<ObjectId> PlannedTasks { get; set; }

        public BacklogHistoryRecord CurrentBacklog { get; set; }

        public IEnumerable<BacklogHistoryRecord> BacklogHistory { get; set; }
    }
}
