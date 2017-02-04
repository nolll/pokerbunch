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
            var result = Sut.Execute(Request);

            Assert.AreEqual(CashgameData.Id1, result.CashgameId);
            Assert.AreEqual(LocationData.Id1, result.LocationId);
            Assert.AreEqual("2001-01-01", result.Date);
        }

        [Test]
        public void HasTwoLocations()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(2, result.Locations.Count);
        }

        [Test]
        public void HasTwoEvents()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(2, result.Events.Count);
        }
    }
}