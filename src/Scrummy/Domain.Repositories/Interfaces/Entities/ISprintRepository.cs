using System;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces.Entities
{
    public interface ISprintRepository : IRepository<Sprint>
    {
        SprintBacklog GetSprintBacklog(Identity sprintIdentity);

        void UpdatePlannedTasks(SprintBacklog backlog);

        void UpdateCurrentTasks(SprintBacklog backlog);

        bool CheckIfSprintOverlapsWithOtherSprint(Identity projectIdentity, Tuple<DateTime, DateTime> timeSpan);
    }
}
