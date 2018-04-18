using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces.Entities
{
    public interface ITeamRepository : IRepository
    {
        Identity CreateTeam(Team meeting);

        Team ReadTeam(Identity id);

        void UpdateTeam(Team meeting);

        void UpdateMembers(Team team);

        void DeleteTeam(Identity id);
    }
}
