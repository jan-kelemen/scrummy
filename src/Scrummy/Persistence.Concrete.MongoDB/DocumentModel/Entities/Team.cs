using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Scrummy.Domain.Core.Entities.Enumerations;

namespace Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities
{
    internal class Team : BaseEntity
    {
        internal class Member
        {
            public ObjectId Id { get; set; }

            [BsonRepresentation(BsonType.String)]
            public PersonRole Role { get; set; }
        }

        public string Name { get; set; }

        public TimeSpan TimeOfDailyScrum { get; set; }

        public MembersHistoryRecord CurrentMembers { get; set; }

        public IEnumerable<MembersHistoryRecord> MembersHistory { get; set; }
    }

    internal class MembersHistoryRecord
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public IEnumerable<Team.Member> Members { get; set; }
    }
}
