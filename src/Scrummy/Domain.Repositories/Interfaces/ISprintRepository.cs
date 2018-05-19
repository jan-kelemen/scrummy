using System;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces
{
    public interface ISprintRepository : IRepository<Sprint>
    {
        SprintBacklog GetSprintBacklog(Identity sprintIdentity);

        void UpdatePlannedTasks(SprintBacklog backlog);

        void UpdateCurrentTasks(SprintBacklog backlog);
    }
}
