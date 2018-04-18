using System;
using System.Collections.Generic;
using MongoDB.Bson;

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

        public ObjectId TeamId { get; set; }
    }
}
