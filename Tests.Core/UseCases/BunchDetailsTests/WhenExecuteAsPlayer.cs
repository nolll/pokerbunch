using Core.Entities;
using NUnit.Framework;

namespace Tests.Core.UseCases.BunchDetailsTests
{
    public class WhenExecuteAsPlayer : Arrange
    {
        protected override Role Role => Role.Player;

        [Test]
        public void BunchNameIsSet()
        {
            Assert.AreEqual(DisplayName, Sut.Execute(Request).BunchName);
        }

        [Test]
        public void DescriptionIsSet()
        {
            Assert.AreEqual(Description, Sut.Execute(Request).Description);
        }

        [Test]
        public void HouseRulesIsSet()
        {
            Assert.AreEqual(HouseRules, Sut.Execute(Request).HouseRules);
        }

        [Test]
        public void CanEditIsFalse()
        {
            Assert.IsFalse(Sut.Execute(Request).CanEdit);
        }
    }
}
