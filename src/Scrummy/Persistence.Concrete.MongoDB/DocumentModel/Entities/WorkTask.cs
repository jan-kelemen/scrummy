﻿using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Scrummy.Domain.Core.Entities.Enumerations;

namespace Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities
{
    internal class WorkTask : BaseEntity
    {
        [BsonRepresentation(BsonType.String)]
        public WorkTaskType Type { get; set; }

        public string Name { get; set; }

        public int? StoryPoints { get; set; }

        public string Description { get; set; }

        public IEnumerable<ObjectId> LinkedFrom { get; set; }

        public IEnumerable<ObjectId> LinkedTo { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }

    internal class Comment : BaseEntity
    {
        public ObjectId AuthorId { get; set; }

        public string Content { get; set; }
    }
}