using Scrummy.Domain.Core.Entities;

namespace Scrummy.Domain.Repositories.Interfaces
{
    public interface ITeamRepository : IRepository<Team>
    {
        void UpdateMembers(Team team);
    }
}
