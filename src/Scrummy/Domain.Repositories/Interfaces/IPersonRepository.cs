using Scrummy.Domain.Core.Entities;

namespace Scrummy.Domain.Repositories.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        bool CheckIfEmailExists(string email);
    }
}
