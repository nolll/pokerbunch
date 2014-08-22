using System.Collections.Generic;
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
        private CashgameContextInteractor _sut;

        [SetUp]
        public virtual void SetUp()
        {
            _sut = new CashgameContextInteractor(
                GetMock<IBunchContextInteractor>().Object,
                GetMock<ICashgameRepository>().Object);
        }

        [Test]
        public void Execute_NoRunningGame_GameIsRunningIsFalse()
        {
            const string slug = "a";
            const string name = "b";
            const int year = 1;
            var years = new List<int>();
            var request = new CashgameContextRequest(slug, year);
            var contextResult = new BunchContextResultInTest(slug: slug, bunchName: name);

            GetMock<IBunchContextInteractor>().Setup(o => o.Execute(It.IsAny<BunchContextRequest>())).Returns(contextResult);
            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = _sut.Execute(request);

            Assert.IsFalse(result.GameIsRunning);
        }

        [Test]
        public void Execute_WithBunch_BunchNameIsSet()
        {
            const string slug = "a";
            const string name = "b";
            var years = new List<int>();
            var cashgameContextRequest = new CashgameContextRequest(slug);
            var contextResult = new BunchContextResultInTest(slug: slug, bunchName: name);

            GetMock<IBunchContextInteractor>().Setup(o => o.Execute(It.IsAny<BunchContextRequest>())).Returns(contextResult);
            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = _sut.Execute(cashgameContextRequest);

            Assert.AreEqual(name, result.Context.BunchName);
        }

        [Test]
        public void Execute_WithYear_SelectedYearIsSet()
        {
            const string slug = "a";
            const string name = "b";
            const int year = 1;
            var years = new List<int>();
            var request = new CashgameContextRequest(slug, year);
            var contextResult = new BunchContextResultInTest(slug: slug, bunchName: name);

            GetMock<IBunchContextInteractor>().Setup(o => o.Execute(It.IsAny<BunchContextRequest>())).Returns(contextResult);
            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = _sut.Execute(request);

            Assert.AreEqual(year, result.SelectedYear);
        }

        [Test]
        public void Execute_WithoutYear_SelectedYearIsNull()
        {
            const string slug = "a";
            const string name = "b";
            var years = new List<int>();
            var request = new CashgameContextRequest(slug);
            var contextResult = new BunchContextResultInTest(slug: slug, bunchName: name);

            GetMock<IBunchContextInteractor>().Setup(o => o.Execute(It.IsAny<BunchContextRequest>())).Returns(contextResult);
            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = _sut.Execute(request);

            Assert.IsNull(result.SelectedYear);
        }

        [Test]
        public void Execute_WithoutRunningGame_GameIsRunningGameIsFalse()
        {
            const string slug = "a";
            const string name = "b";
            const int year = 1;
            var years = new List<int>();
            var request = new CashgameContextRequest(slug, year);
            var contextResult = new BunchContextResultInTest(slug: slug, bunchName: name);

            GetMock<IBunchContextInteractor>().Setup(o => o.Execute(It.IsAny<BunchContextRequest>())).Returns(contextResult);
            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = _sut.Execute(request);

            Assert.AreEqual(slug, result.Context.Slug);
        }

        [Test]
        public void Execute_WithRunningGame_GameIsRunningIsTrue()
        {
            const string slug = "a";
            const string name = "b";
            var years = new List<int>(); 
            var cashgame = new CashgameInTest();
            var request = new CashgameContextRequest(slug);
            var contextResult = new BunchContextResultInTest(slug: slug, bunchName: name);

            GetMock<IBunchContextInteractor>().Setup(o => o.Execute(It.IsAny<BunchContextRequest>())).Returns(contextResult);
            GetMock<ICashgameRepository>().Setup(o => o.GetRunning(It.IsAny<int>())).Returns(cashgame);
            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = _sut.Execute(request);

            Assert.IsTrue(result.GameIsRunning);
        }

        [Test]
        public void Execute_WithoutYears_YearsAreEmptyAndLatestYearIsNull()
        {
            const string slug = "a";
            const string name = "b";
            var years = new List<int>();
            var request = new CashgameContextRequest(slug);
            var contextResult = new BunchContextResultInTest(slug: slug, bunchName: name);

            GetMock<IBunchContextInteractor>().Setup(o => o.Execute(It.IsAny<BunchContextRequest>())).Returns(contextResult);
            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = _sut.Execute(request);

            Assert.IsEmpty(result.Years);
            Assert.IsNull(result.LatestYear);
        }

        [Test]
        public void Execute_WithYears_YearsContainsThemAndLatestYearIsCorrect()
        {
            const string slug = "a";
            const string name = "b";
            var years = new List<int>{1, 2, 3};
            var request = new CashgameContextRequest(slug);
            var contextResult = new BunchContextResultInTest(slug: slug, bunchName: name);

            GetMock<IBunchContextInteractor>().Setup(o => o.Execute(It.IsAny<BunchContextRequest>())).Returns(contextResult);
            GetMock<ICashgameRepository>().Setup(o => o.GetYears(It.IsAny<int>())).Returns(years);

            var result = _sut.Execute(request);

            Assert.AreEqual(3, result.Years.Count);
            Assert.AreEqual(3, result.LatestYear);
        }
    }
}