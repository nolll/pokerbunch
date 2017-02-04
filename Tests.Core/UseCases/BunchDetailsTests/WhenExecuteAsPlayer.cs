using Core.Entities;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.BunchDetailsTests
{
    public class WhenExecuteAsPlayer : Arrange
    {
        protected override Role Role => Role.Player;

        [Test]
        public void BunchNameIsSet()
        {
            Assert.AreEqual(BunchData.DisplayName1, Sut.Execute(Request).BunchName);
        }

        [Test]
        public void DescriptionIsSet()
        {
            Assert.AreEqual(BunchData.Description1, Sut.Execute(Request).Description);
        }

        [Test]
        public void HouseRulesIsSet()
        {
            Assert.AreEqual(BunchData.HouseRules1, Sut.Execute(Request).HouseRules);
        }

        [Test]
        public void CanEditIsFalse()
        {
            Assert.IsFalse(Sut.Execute(Request).CanEdit);
        }
    }
}
