using Scrummy.Domain.Core.Entities;

namespace Scrummy.Domain.Repositories.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        void ChangePassword(Person person);

        bool CheckIfEmailExists(string email);

        Person FindByEmailAndPasswordHash(string email, string passwordHash);
    }
}
