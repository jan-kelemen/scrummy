using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces
{
    public interface IMeetingRepository : IRepository<Meeting>
    {
        IEnumerable<Identity> GetMeetingsOfPersonInTimeRange(Identity personId, DateTime fromTime, DateTime toTime);
    }
}
