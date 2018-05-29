using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories.Interfaces.DTO;

namespace Scrummy.Domain.Repositories.Interfaces
{
    public interface ITeamRepository : IRepository<Team>
    {
        IEnumerable<Identity> GetTeamsOfPersonAtTimePoint(Identity personId, DateTime timePoint);

        HistoryDTO<IEnumerable<Tuple<Identity, PersonRole>>> ReadTeamPersonHistory(Identity teamId);

        HistoryDTO<Tuple<Identity, IEnumerable<PersonRole>>> ReadPersonTeamHistory(Identity personId);
    }
}
