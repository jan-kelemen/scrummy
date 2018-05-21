using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces.DTO;

namespace Scrummy.Domain.Repositories.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        void ChangePassword(Person person);

        bool CheckIfEmailExists(string email);

        Person FindByEmailAndPasswordHash(string email, string passwordHash);
    }
}
