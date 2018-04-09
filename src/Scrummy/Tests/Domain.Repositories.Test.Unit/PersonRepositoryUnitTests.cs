using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrummy.Domain.Repositories.Interfaces.Entities;

namespace Scrummy.Domain.Repositories.Test.Unit
{
    [TestClass]
    public abstract class PersonRepositoryUnitTests
    {
        protected PersonRepositoryUnitTests(IPersonRepository personRepository)
        {
            PersonRepository = personRepository;
        }

        public IPersonRepository PersonRepository { get; }
    }
}
