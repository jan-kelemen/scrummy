using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories.Interfaces.DTO;

namespace Scrummy.Domain.Repositories.Interfaces
{
    public interface ISprintRepository : IRepository<Sprint>
    {
        Sprint ReadCurrentSprint(Identity projectId);

        SprintBacklog ReadSprintBacklog(Identity sprintIdentity);

        void UpdatePlannedTasks(SprintBacklog backlog);

        void UpdateCurrentTasks(SprintBacklog backlog);

        IEnumerable<Sprint> ReadSprints(Identity projectId, SprintStatus status);

        IEnumerable<SprintBacklog> ReadSprintBacklogs(Identity projectId, SprintStatus status);

        IEnumerable<SprintBacklogHistoryRecord> ReadHistoryRecords(Identity id);
    }
}
