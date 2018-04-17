using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Exceptions;

namespace Scrummy.Domain.Core.Test.Unit.Entities
{
    [TestClass]
    public class PersonUnitTests
    {
        [DataTestMethod]
        [DataRow("1", 0, Person.Validation.LastNameMinLength, Person.Validation.DisplayNameMinLength, Person.Validation.EmailMinLength)]
        [DataRow("2", Person.Validation.FirstNameMinLength - 1, Person.Validation.LastNameMinLength, Person.Validation.DisplayNameMinLength, Person.Validation.EmailMinLength)]
        [DataRow("3", Person.Validation.FirstNameMaxLength + 1, Person.Validation.LastNameMinLength, Person.Validation.DisplayNameMinLength, Person.Validation.EmailMinLength)]
        [DataRow("4", Person.Validation.FirstNameMinLength, 0, Person.Validation.DisplayNameMinLength, Person.Validation.EmailMinLength)]
        [DataRow("5", Person.Validation.FirstNameMinLength, Person.Validation.LastNameMinLength - 1, Person.Validation.DisplayNameMinLength, Person.Validation.EmailMinLength)]
        [DataRow("6", Person.Validation.FirstNameMinLength, Person.Validation.LastNameMaxLength + 1, Person.Validation.DisplayNameMinLength, Person.Validation.EmailMinLength)]
        [DataRow("7", Person.Validation.FirstNameMinLength, Person.Validation.LastNameMinLength, 0, Person.Validation.EmailMinLength)]
        [DataRow("8", Person.Validation.FirstNameMinLength, Person.Validation.LastNameMinLength, Person.Validation.DisplayNameMinLength- 1, Person.Validation.EmailMinLength)]
        [DataRow("9", Person.Validation.FirstNameMinLength, Person.Validation.LastNameMinLength, Person.Validation.DisplayNameMaxLength + 1, Person.Validation.EmailMinLength)]
        [DataRow("10", Person.Validation.FirstNameMinLength, Person.Validation.LastNameMinLength, Person.Validation.DisplayNameMinLength, 0)]
        [DataRow("11", Person.Validation.FirstNameMinLength, Person.Validation.LastNameMinLength, Person.Validation.DisplayNameMinLength, Person.Validation.EmailMinLength - 1)]
        [DataRow("12", Person.Validation.FirstNameMinLength, Person.Validation.LastNameMinLength, Person.Validation.DisplayNameMinLength, Person.Validation.EmailMaxLength + 1)]
        public void PersonConstructorValidation_Works_ForInvalidInputs(string id, int firstNameLength, int lastNameLength, int displayNameLength, int emailLength)
        {
            var identity = Identity.FromString(id);
            var firstName = new string('a', firstNameLength);
            var lastName = new string('b', lastNameLength);
            var displayName = new string('c', displayNameLength);
            var email = new string('d', emailLength);
            Assert.ThrowsException<EntityValidationException>(() => new Person(identity, firstName, lastName, displayName, email));
        }
    }
}
