using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities
{
    internal class BaseEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
