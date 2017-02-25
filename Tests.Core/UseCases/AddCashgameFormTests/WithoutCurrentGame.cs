using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.AddCashgameFormTests
{
    public class WithoutCurrentGame : Arrange
    {
        [Test]
        public void ReturnsResultObject() => Assert.IsInstanceOf<AddCashgameForm.Result>(Result);

        [Test]
        public void LocationsAreSet() => Assert.AreEqual(2, Result.Locations.Count);
    }
}