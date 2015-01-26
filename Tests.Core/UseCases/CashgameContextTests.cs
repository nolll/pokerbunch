using Core.UseCases.BunchContext;
using Core.UseCases.CashgameContext;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Core.UseCases
{
    class CashgameContextTests : TestBase
    {
        [Test]
        public void Execute_NoRunningGame_GameIsRunningIsFalse()
        {
            const string slug = "a";
            const int year = 1;
            var request = new CashgameContextRequest(slug, year);

            var result = Execute(request);

            Assert.IsFalse(result.GameIsRunning);
        }

        [Test]
        public void Execute_BunchContextIsSet()
        {
            const string slug = "a";
            var cashgameContextRequest = new CashgameContextRequest(slug);

            var result = Execute(cashgameContextRequest);

            Assert.IsInstanceOf<BunchContextResult>(result.BunchContext);
        }

        [Test]
        public void Execute_WithYear_SelectedYearIsSet()
        {
            const string slug = "a";
            const int year = 1;
            var request = new CashgameContextRequest(slug, year);

            var result = Execute(request);

            Assert.AreEqual(year, result.SelectedYear);
        }

        [Test]
        public void Execute_WithoutYear_SelectedYearIsNull()
        {
            const string slug = "a";
            var request = new CashgameContextRequest(slug);

            var result = Execute(request);

            Assert.IsNull(result.SelectedYear);
        }

        [Test]
        public void Execute_WithoutRunningGame_GameIsRunningGameIsFalse()
        {
            const string slug = "a";
            const int year = 1;
            var request = new CashgameContextRequest(slug, year);

            var result = Execute(request);

            Assert.IsFalse(result.GameIsRunning);
        }

        [Test]
        public void Execute_WithRunningGame_GameIsRunningIsTrue()
        {
            Repos.Cashgame.SetupRunningGame();

            const string slug = "a";
            var request = new CashgameContextRequest(slug);

            var result = Execute(request);

            Assert.IsTrue(result.GameIsRunning);
        }

        [TestCase(CashgamePage.Matrix)]
        [TestCase(CashgamePage.List)]
        public void Execute_SelectedPage_SelectedPageIsSet(CashgamePage selectedPage)
        {
            const string slug = "a";
            const int year = 1;
            var request = new CashgameContextRequest(slug, year, selectedPage);

            var result = Execute(request);

            Assert.AreEqual(selectedPage, result.SelectedPage);
        }

        private CashgameContextResult Execute(CashgameContextRequest request)
        {
            return CashgameContextInteractor.Execute(
                new BunchContextResultInTest(), 
                Repos.Cashgame,
                request);
        }
    }
}