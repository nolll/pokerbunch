using Core.Entities;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.EditCashgameFormTests
{
    public class WithPlayer : Arrange
    {
        protected override Role Role => Role.Player;

        [Test]
        public void HasBaseProperties()
        {
            Assert.AreEqual(CashgameData.Id1, Result.CashgameId);
            Assert.AreEqual(LocationData.Id1, Result.LocationId);
            Assert.AreEqual("2001-01-01", Result.Date);
        }

        [Test]
        public void HasTwoLocations() => Assert.AreEqual(2, Result.Locations.Count);

        [Test]
        public void HasTwoEvents() => Assert.AreEqual(2, Result.Events.Count);
    }
}