using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities
{
    internal class Meeting : BaseEntity
    {
        public ObjectId ProjectId { get; set; }

        public string Name { get; set; }

        public string Time { get; set; }

        public TimeSpan Duration { get; set; }

        public ObjectId OrganizedBy { get; set; }

        public string Description { get; set; }

        public string Log { get; set; }

        public IEnumerable<ObjectId> InvolvedPersons { get; set; }

        public IEnumerable<ObjectId> Documents { get; set; }
    }
}
