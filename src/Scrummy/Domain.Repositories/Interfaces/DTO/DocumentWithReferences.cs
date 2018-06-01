using System.Collections.Generic;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces.DTO
{
    public class DocumentWithReferences
    {
        public Document Document { get; set; }

        public IEnumerable<Identity> Meetings { get; set; }

        public IEnumerable<Identity> Sprints { get; set; }

        public IEnumerable<Identity> Tasks { get; set; }
    }
}
