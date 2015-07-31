using Core.Exceptions;
using Core.UseCases.AddCashgameForm;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class AddCashgameFormTests : TestBase
    {
        [Test]
        public void AddCashgameOptions_ReturnsResultObject()
        {
            const string slug = TestData.SlugA;
            var result = Sut.Execute(new AddCashgameFormRequest(slug));

            Assert.IsInstanceOf<AddCashgameFormResult>(result);
        }

        [Test]
        public void AddCashgameOptions_WithRunningCashgame_ThrowsException()
        {
            Repos.Cashgame.SetupRunningGame();

            const string slug = TestData.SlugA;

            Assert.Throws<CashgameRunningException>(() => Sut.Execute(new AddCashgameFormRequest(slug)));
        }

        [Test]
        public void AddCashgameOptions_LocationsAreSet()
        {
            const string slug = TestData.SlugA;
            var result = Sut.Execute(new AddCashgameFormRequest(slug));

            Assert.AreEqual(2, result.Locations.Count);
            Assert.AreEqual(TestData.LocationA, result.Locations[0]);
            Assert.AreEqual(TestData.LocationB, result.Locations[1]);
        }

        private AddCashgameFormInteractor Sut
        {
            get
            {
                return new AddCashgameFormInteractor(
                    Repos.Bunch,
                    Repos.Cashgame);
            }
        }
    }
}