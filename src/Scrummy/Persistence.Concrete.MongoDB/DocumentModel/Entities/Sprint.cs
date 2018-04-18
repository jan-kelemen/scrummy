using System;
using MongoDB.Bson;

namespace Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities
{
    internal class Sprint : BaseEntity
    {
        public ObjectId ProjectId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Goal { get; set; }

    }
}
