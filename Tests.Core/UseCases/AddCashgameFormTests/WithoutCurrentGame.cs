using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.AddCashgameFormTests
{
    public class WithoutCurrentGame : Arrange
    {
        [Test]
        public void ReturnsResultObject() => Assert.IsInstanceOf<AddCashgameForm.Result>(Execute());

        [Test]
        public void LocationsAreSet() => Assert.AreEqual(2, Execute().Locations.Count);
    }
}