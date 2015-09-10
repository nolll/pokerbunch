using Core.Exceptions;
using Core.UseCases;
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
            var result = Sut.Execute(new AddCashgameForm.Request(TestData.UserNameA, slug));

            Assert.IsInstanceOf<AddCashgameForm.Result>(result);
        }

        [Test]
        public void AddCashgameOptions_WithRunningCashgame_ThrowsException()
        {
            Repos.Cashgame.SetupRunningGame();

            const string slug = TestData.SlugA;

            Assert.Throws<CashgameRunningException>(() => Sut.Execute(new AddCashgameForm.Request(TestData.UserNameA, slug)));
        }

        [Test]
        public void AddCashgameOptions_LocationsAreSet()
        {
            const string slug = TestData.SlugA;
            var result = Sut.Execute(new AddCashgameForm.Request(TestData.UserNameA, slug));

            Assert.AreEqual(2, result.Locations.Count);
            Assert.AreEqual(TestData.LocationA, result.Locations[0]);
            Assert.AreEqual(TestData.LocationB, result.Locations[1]);
        }

        private AddCashgameForm Sut
        {
            get
            {
                return new AddCashgameForm(
                    Services.BunchService,
                    Services.CashgameService,
                    Services.UserService,
                    Repos.Player);
            }
        }
    }
}