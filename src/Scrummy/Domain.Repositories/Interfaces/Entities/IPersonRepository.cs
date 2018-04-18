using Scrummy.Domain.Core.Entities;

namespace Scrummy.Domain.Repositories.Interfaces.Entities
{
    public interface IPersonRepository : IRepository<Person>
    {
        bool CheckIfEmailExists(string email);
    }
}
