using Core.Entities;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.BunchDetailsTests
{
    public class WhenExecuteAsPlayer : Arrange
    {
        protected override Role Role => Role.Player;

        [Test]
        public void BunchNameIsSet() => Assert.AreEqual(BunchData.DisplayName1, Result.BunchName);

        [Test]
        public void DescriptionIsSet() => Assert.AreEqual(BunchData.Description1, Result.Description);

        [Test]
        public void HouseRulesIsSet() => Assert.AreEqual(BunchData.HouseRules1, Result.HouseRules);

        [Test]
        public void CanEditIsFalse() => Assert.IsFalse(Result.CanEdit);
    }
}
