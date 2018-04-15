using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces.Entities
{
    public interface IPersonRepository : IRepository
    {
        Identity CreatePerson(Person person);

        Person ReadPerson(Identity id);

        void UpdatePerson(Person person);

        void DeletePerson(Identity id);

        bool CheckIfEmailExists(string email);
    }
}
