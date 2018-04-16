using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Scrummy.Domain.Core.Entities.Enumerations;

namespace Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities
{
    internal class Project : BaseEntity
    {
        public string Name { get; set; }

        public IEnumerable<string> DefinitionOfDoneConditions { get; set; }

        public TeamHistoryRecord CurrentTeam { get; set; }

        public IEnumerable<TeamHistoryRecord> TeamHistory { get; set; }
    }

    internal class TeamHistoryRecord
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public IEnumerable<TeamMember> Members { get; set; }
    }

    internal class TeamMember
    {
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public PersonRole Role { get; set; }
    }
}
