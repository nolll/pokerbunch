using System.Collections.Generic;
using Application.Services;
using Application.UseCases.AppContext;
using Application.UseCases.BaseContext;
using Application.UseCases.BunchContext;
using Application.UseCases.CashgameContext;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class CashgameContextTests : MockContainer
    {
        [Test]
        public void Execute_NoRunningGame_GameIsRunningIsFalse()
        {
            const string slug = "a";
            const int year = 1;
            var years = new List<int>();
            var request = new CashgameContextRequest(slug, year);

            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = Execute(request);

            Assert.IsFalse(result.GameIsRunning);
        }

        [Test]
        public void Execute_BunchContextIsSet()
        {
            const string slug = "a";
            var years = new List<int>();
            var cashgameContextRequest = new CashgameContextRequest(slug);

            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = Execute(cashgameContextRequest);

            Assert.IsInstanceOf<BunchContextResult>(result.BunchContext);
        }

        [Test]
        public void Execute_WithYear_SelectedYearIsSet()
        {
            const string slug = "a";
            const int year = 1;
            var years = new List<int>();
            var request = new CashgameContextRequest(slug, year);

            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = Execute(request);

            Assert.AreEqual(year, result.SelectedYear);
        }

        [Test]
        public void Execute_WithoutYear_SelectedYearIsNull()
        {
            const string slug = "a";
            var years = new List<int>();
            var request = new CashgameContextRequest(slug);

            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = Execute(request);

            Assert.IsNull(result.SelectedYear);
        }

        [Test]
        public void Execute_WithoutRunningGame_GameIsRunningGameIsFalse()
        {
            const string slug = "a";
            const int year = 1;
            var years = new List<int>();
            var request = new CashgameContextRequest(slug, year);

            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = Execute(request);

            Assert.IsFalse(result.GameIsRunning);
        }

        [Test]
        public void Execute_WithRunningGame_GameIsRunningIsTrue()
        {
            const string slug = "a";
            var years = new List<int>(); 
            var cashgame = new CashgameInTest();
            var request = new CashgameContextRequest(slug);

            GetMock<ICashgameRepository>().Setup(o => o.GetRunning(It.IsAny<int>())).Returns(cashgame);
            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = Execute(request);

            Assert.IsTrue(result.GameIsRunning);
        }

        [Test]
        public void Execute_WithoutYears_YearsAreEmptyAndLatestYearIsNull()
        {
            const string slug = "a";
            var years = new List<int>();
            var request = new CashgameContextRequest(slug);

            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = Execute(request);

            Assert.IsEmpty(result.Years);
            Assert.IsNull(result.LatestYear);
        }

        [Test]
        public void Execute_WithYears_YearsContainsThemAndLatestYearIsCorrect()
        {
            const string slug = "a";
            var years = new List<int>{1, 2, 3};
            var request = new CashgameContextRequest(slug);

            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = Execute(request);

            Assert.AreEqual(3, result.Years.Count);
            Assert.AreEqual(3, result.LatestYear);
        }

        [TestCase(CashgamePage.Matrix)]
        [TestCase(CashgamePage.List)]
        public void Execute_SelectedPage_SelectedPageIsSet(CashgamePage selectedPage)
        {
            const string slug = "a";
            const int year = 1;
            var years = new List<int>();
            var request = new CashgameContextRequest(slug, year, selectedPage);

            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = Execute(request);

            Assert.AreEqual(selectedPage, result.SelectedPage);
        }

        private CashgameContextResult Execute(CashgameContextRequest request)
        {
            return CashgameContextInteractor.Execute(
                BunchContextFunc,
                GetMock<ICashgameRepository>().Object,
                request);
        }

        private BunchContextResult BunchContextFunc(BunchContextRequest request)
        {
            return new BunchContextResultInTest();
        }
    }
}