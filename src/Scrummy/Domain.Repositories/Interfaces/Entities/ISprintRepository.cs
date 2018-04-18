using System;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces.Entities
{
    public interface ISprintRepository : IRepository<Sprint>
    {
        bool CheckIfSprintOverlapsWithOtherSprint(Identity projectIdentity, Tuple<DateTime, DateTime> timeSpan);
    }
}
