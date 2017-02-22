using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.AddCashgameFormTests
{
    public class WithoutCurrentGame : Arrange
    {
        [Test]
        public void AddCashgameOptions_ReturnsResultObject()
        {
            var result = Sut.Execute(Request);

            Assert.IsInstanceOf<AddCashgameForm.Result>(result);
        }

        [Test]
        public void AddCashgameOptions_LocationsAreSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(2, result.Locations.Count);
        }
    }
}