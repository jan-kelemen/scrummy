using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces.DTO;

namespace Scrummy.Domain.Repositories.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        ProductBacklog ReadProductBacklog(Identity projectIdentity);

        void UpdateProductBacklog(ProductBacklog productBacklog);

        bool CheckIfProjectWithNameExists(string name);

        IEnumerable<Identity> GetProjectsOfTeamAtTimePoint(Identity teamId, DateTime timePoint);

        HistoryDTO<Identity> ReadProjectTeamHistory(Identity projectId);

        HistoryDTO<Identity> ReadTeamProjectHistory(Identity teamId);

        IEnumerable<ProjectBacklogHistoryRecord> ReadHistoryRecords(Identity id);
    }
}
