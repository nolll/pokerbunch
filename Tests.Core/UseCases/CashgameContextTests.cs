using System;
using System.Linq;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class CashgameContextTests : TestBase
    {
        [Test]
        public void Execute_NoRunningGame_GameIsRunningIsFalse()
        {
            const string slug = "a";
            const int year = 1;
            var request = new CashgameContext.Request(TestData.UserNameA, slug, DateTime.UtcNow, CashgameContext.CashgamePage.Unknown, year);

            var result = Sut.Execute(request);

            Assert.IsFalse(result.GameIsRunning);
        }

        [Test]
        public void Execute_BunchContextIsSet()
        {
            const string slug = "a";
            var cashgameContextRequest = new CashgameContext.Request(TestData.UserNameA, slug, DateTime.UtcNow);

            var result = Sut.Execute(cashgameContextRequest);

            Assert.IsInstanceOf<BunchContext.Result>(result.BunchContext);
        }

        [Test]
        public void Execute_WithYear_SelectedYearIsSet()
        {
            const string slug = "a";
            const int year = 1;
            var request = new CashgameContext.Request(TestData.UserNameA, slug, DateTime.UtcNow, CashgameContext.CashgamePage.Unknown, year);

            var result = Sut.Execute(request);

            Assert.AreEqual(year, result.SelectedYear);
        }

        [Test]
        public void Execute_WithoutYear_SelectedYearIsNull()
        {
            const string slug = "a";
            var request = new CashgameContext.Request(TestData.UserNameA, slug, DateTime.UtcNow);

            var result = Sut.Execute(request);

            Assert.IsNull(result.SelectedYear);
        }

        [Test]
        public void Execute_WithoutRunningGame_GameIsRunningGameIsFalse()
        {
            const string slug = "a";
            const int year = 1;
            var request = new CashgameContext.Request(TestData.UserNameA, slug, DateTime.UtcNow, CashgameContext.CashgamePage.Unknown, year);

            var result = Sut.Execute(request);

            Assert.IsFalse(result.GameIsRunning);
        }

        [Test]
        public void Execute_WithRunningGame_GameIsRunningIsTrue()
        {
            Repos.Cashgame.SetupRunningGame();

            const string slug = "a";
            var request = new CashgameContext.Request(TestData.UserNameA, slug, DateTime.UtcNow);

            var result = Sut.Execute(request);

            Assert.IsTrue(result.GameIsRunning);
        }

        [TestCase(CashgameContext.CashgamePage.Matrix, "/cashgame/matrix/a")]
        [TestCase(CashgameContext.CashgamePage.Toplist, "/cashgame/toplist/a")]
        [TestCase(CashgameContext.CashgamePage.Chart, "/cashgame/chart/a")]
        [TestCase(CashgameContext.CashgamePage.List, "/cashgame/list/a")]
        [TestCase(CashgameContext.CashgamePage.Facts, "/cashgame/facts/a")]
        public void Execute_SelectedPage_SelectedPageAndLastYearUrlIsCorrect(CashgameContext.CashgamePage selectedPage, string url)
        {
            const string slug = "a";
            const int year = 1;
            var request = new CashgameContext.Request(TestData.UserNameA, slug, DateTime.UtcNow, selectedPage, year);

            var result = Sut.Execute(request);

            Assert.AreEqual(selectedPage, result.SelectedPage);
            Assert.AreEqual(url, result.YearItems.Last().Url.Relative);
        }

        private CashgameContext Sut
        {
            get
            {
                return new CashgameContext(
                    Repos.User,
                    Repos.Bunch,
                    Repos.Cashgame);
            }
        }
    }
}