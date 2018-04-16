using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Artifacts;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces.Entities
{
    public interface IProjectRepository : IRepository
    {
        Identity CreateProject(Project project);

        Project ReadProject(Identity id);

        void UpdateProject(Project project);

        void UpdateDefinitionOfDone(Identity projectIdentity, DefinitionOfDone definitionOfDone);

        void UpdateTeam(Identity projectIdentity, Team team);

        void DeleteProject(Identity id);

        bool CheckIfProjectWithNameExists(string name);
    }
}
