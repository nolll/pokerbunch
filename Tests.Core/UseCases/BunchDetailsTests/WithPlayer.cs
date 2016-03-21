using NUnit.Framework;

namespace Tests.Core.UseCases.BunchDetailsTests
{
    public class WithPlayer : Arrange
    {
        [Test]
        public void BunchNameIsSet()
        {
            Assert.AreEqual(DisplayName, Result.BunchName);
        }

        [Test]
        public void DescriptionIsSet()
        {
            Assert.AreEqual(Description, Result.Description);
        }

        [Test]
        public void HouseRulesIsSet()
        {
            Assert.AreEqual(HouseRules, Result.HouseRules);
        }

        [Test]
        public void CanEditIsFalse()
        {
            Assert.IsFalse(Result.CanEdit);
        }
    }
}
