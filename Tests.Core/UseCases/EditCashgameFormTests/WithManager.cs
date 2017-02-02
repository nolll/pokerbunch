using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases.EditCashgameFormTests
{
    public class WithPlayer : Arrange
    {
        [Test]
        public void HasBaseProperties()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(Id, result.CashgameId);
            Assert.AreEqual(LocationId, result.LocationId);
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