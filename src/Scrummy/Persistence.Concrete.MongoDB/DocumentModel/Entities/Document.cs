using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Scrummy.Domain.Core.Entities.Enumerations;

namespace Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities
{
    internal class Document : BaseEntity
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public ObjectId ProjectId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public DocumentKind Kind { get; set; }

        public IEnumerable<string> Links { get; set; }
    }
}
