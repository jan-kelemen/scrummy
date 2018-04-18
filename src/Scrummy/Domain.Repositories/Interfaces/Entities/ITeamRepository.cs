using Scrummy.Domain.Core.Entities;

namespace Scrummy.Domain.Repositories.Interfaces.Entities
{
    public interface ITeamRepository : IRepository<Team>
    {
        void UpdateMembers(Team team);
    }
}
