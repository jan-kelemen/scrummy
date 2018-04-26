﻿using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        void UpdateDefinitionOfDone(Identity projectIdentity, DefinitionOfDone definitionOfDone);

        void UpdateTeam(Identity projectIdentity, Identity teamIdentity);

        ProductBacklog GetProductBacklog(Identity projectIdentity);

        void UpdateProductBacklog(ProductBacklog productBacklog);

        bool CheckIfProjectWithNameExists(string name);

        IEnumerable<(Identity teamId, Identity projectId)> GetProjectsOfTeamsAtTimePoint(IEnumerable<Identity> teamIds, DateTime timePoint);
    }
}
