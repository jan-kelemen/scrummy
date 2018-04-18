using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces.Entities
{
    public interface IProjectRepository : IRepository<Project>
    {
        void UpdateDefinitionOfDone(Identity projectIdentity, DefinitionOfDone definitionOfDone);

        void UpdateTeam(Identity projectIdentity, Identity teamIdentity);

        bool CheckIfProjectWithNameExists(string name);
    }
}
