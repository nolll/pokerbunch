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
            const string slug = Constants.SlugA;
            var result = Execute(new AddCashgameFormRequest(slug));

            Assert.IsInstanceOf<AddCashgameFormResult>(result);
        }

        [Test]
        public void AddCashgameOptions_WithRunningCashgame_ThrowsException()
        {
            Repos.Cashgame.SetupRunningGame();

            const string slug = Constants.SlugA;

            Assert.Throws<CashgameRunningException>(() => Execute(new AddCashgameFormRequest(slug)));
        }

        [Test]
        public void AddCashgameOptions_LocationsAreSet()
        {
            const string slug = Constants.SlugA;
            var result = Execute(new AddCashgameFormRequest(slug));

            Assert.AreEqual(2, result.Locations.Count);
            Assert.AreEqual(Constants.LocationA, result.Locations[0]);
            Assert.AreEqual(Constants.LocationB, result.Locations[1]);
        }

        private AddCashgameFormResult Execute(AddCashgameFormRequest request)
        {
            return AddCashgameFormInteractor.Execute(
                Repos.Bunch,
                Repos.Cashgame,
                request);
        }
    }
}