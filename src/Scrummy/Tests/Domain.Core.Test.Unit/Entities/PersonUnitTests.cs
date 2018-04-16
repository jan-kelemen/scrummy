using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Exceptions;
using Scrummy.Domain.Core.Validators.Entities;

namespace Scrummy.Domain.Core.Test.Unit.Entities
{
    [TestClass]
    public class PersonUnitTests
    {
        [DataTestMethod]
        [DataRow("1", 0, PersonValidator.LastNameMinLength, PersonValidator.DisplayNameMinLength, PersonValidator.EmailMinLength)]
        [DataRow("2", PersonValidator.FirstNameMinLength - 1, PersonValidator.LastNameMinLength, PersonValidator.DisplayNameMinLength, PersonValidator.EmailMinLength)]
        [DataRow("3", PersonValidator.FirstNameMaxLength + 1, PersonValidator.LastNameMinLength, PersonValidator.DisplayNameMinLength, PersonValidator.EmailMinLength)]
        [DataRow("4", PersonValidator.FirstNameMinLength, 0, PersonValidator.DisplayNameMinLength, PersonValidator.EmailMinLength)]
        [DataRow("5", PersonValidator.FirstNameMinLength, PersonValidator.LastNameMinLength - 1, PersonValidator.DisplayNameMinLength, PersonValidator.EmailMinLength)]
        [DataRow("6", PersonValidator.FirstNameMinLength, PersonValidator.LastNameMaxLength + 1, PersonValidator.DisplayNameMinLength, PersonValidator.EmailMinLength)]
        [DataRow("7", PersonValidator.FirstNameMinLength, PersonValidator.LastNameMinLength, 0, PersonValidator.EmailMinLength)]
        [DataRow("8", PersonValidator.FirstNameMinLength, PersonValidator.LastNameMinLength, PersonValidator.DisplayNameMinLength- 1, PersonValidator.EmailMinLength)]
        [DataRow("9", PersonValidator.FirstNameMinLength, PersonValidator.LastNameMinLength, PersonValidator.DisplayNameMaxLength + 1, PersonValidator.EmailMinLength)]
        [DataRow("10", PersonValidator.FirstNameMinLength, PersonValidator.LastNameMinLength, PersonValidator.DisplayNameMinLength, 0)]
        [DataRow("11", PersonValidator.FirstNameMinLength, PersonValidator.LastNameMinLength, PersonValidator.DisplayNameMinLength, PersonValidator.EmailMinLength - 1)]
        [DataRow("12", PersonValidator.FirstNameMinLength, PersonValidator.LastNameMinLength, PersonValidator.DisplayNameMinLength, PersonValidator.EmailMaxLength + 1)]
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
