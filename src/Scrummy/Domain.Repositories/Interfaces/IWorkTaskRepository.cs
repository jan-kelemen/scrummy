using System.Collections.Generic;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces
{
    public interface IWorkTaskRepository : IRepository<WorkTask>
    {
        Identity AddComment(WorkTask.Comment comment);

        WorkTask.Comment ReadComment(Identity workTaskIdentity, Identity commentIdentity);

        IEnumerable<WorkTask.Comment> ReadCommentsOfTask(Identity id);

        void UpdateComment(WorkTask.Comment comment);

        void DeleteComment(Identity workTaskIdentity, Identity commentIdentity);
    }
}
