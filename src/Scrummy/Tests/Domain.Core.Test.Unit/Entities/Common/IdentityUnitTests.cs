using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Core.Test.Unit.Entities.Common
{
    [TestClass]
    public class IdentityUnitTests
    {
        [TestMethod]
        public void BlankIdentitiesDontMatch()
        {
            var id1 = Identity.BlankIdentity;
            var id2 = Identity.BlankIdentity;

            Assert.AreNotEqual(id1, id2);
            Assert.IsFalse(id1 == id2);
            Assert.IsTrue(id1 != id2);
        }

        [DataTestMethod]
        [DataRow("id1", "id1", true)]
        [DataRow("id1", "id2", false)]
        public void IdentityEqualityTest(string first, string second, bool expected)
        {
            var id1 = Identity.FromString(first);
            var id2 = Identity.FromString(second);

            Assert.AreEqual(expected, id1 == id2);
        }
    }
}
