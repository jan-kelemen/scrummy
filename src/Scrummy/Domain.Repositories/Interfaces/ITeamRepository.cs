using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces
{
    public interface ITeamRepository : IRepository<Team>
    {
        void UpdateMembers(Team team);

        IEnumerable<Identity> GetTeamsOfPersonAtTimePoint(Identity personId, DateTime timePoint);
    }
}
