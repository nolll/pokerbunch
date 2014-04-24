using System.Collections.Generic;
using Application.UseCases.CashgameContext;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class CashgameContextInteractorTests : MockContainer
    {
        private CashgameContextInteractor _sut;

        [SetUp]
        public virtual void SetUp()
        {
            _sut = new CashgameContextInteractor(
                GetMock<IHomegameRepository>().Object,
                GetMock<ICashgameRepository>().Object);
        }

        [Test]
        public void Execute_WithSlug_SlugIsSet()
        {
            const string slug = "a";
            const int year = 1;
            var homegame = new FakeHomegame(); 
            var request = new CashgameContextRequest { Slug = slug, Year = year };

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);

            var result = _sut.Execute(request);

            Assert.IsFalse(result.GameIsRunning);
        }

        [Test]
        public void Execute_WithBunch_BunchNameIsSet()
        {
            const string slug = "a";
            const string name = "b";
            var homegame = new FakeHomegame(displayName: name);
            var request = new CashgameContextRequest { Slug = slug };

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            
            var result = _sut.Execute(request);

            Assert.AreEqual(name, result.BunchName);
        }

        [Test]
        public void Execute_WithYear_SelectedYearIsSet()
        {
            const string slug = "a";
            const int year = 1;
            var homegame = new FakeHomegame();
            var request = new CashgameContextRequest { Slug = slug, Year = year };

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);

            var result = _sut.Execute(request);

            Assert.AreEqual(year, result.SelectedYear);
        }

        [Test]
        public void Execute_WithoutYear_SelectedYearIsNull()
        {
            const string slug = "a";
            var homegame = new FakeHomegame();
            var request = new CashgameContextRequest { Slug = slug };

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            
            var result = _sut.Execute(request);

            Assert.IsNull(result.SelectedYear);
        }

        [Test]
        public void Execute_WithoutRunningGame_GameIsRunningGameIsFalse()
        {
            const string slug = "a";
            const int year = 1;
            var homegame = new FakeHomegame();
            var request = new CashgameContextRequest { Slug = slug, Year = year };

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);

            var result = _sut.Execute(request);

            Assert.AreEqual(slug, result.Slug);
        }

        [Test]
        public void Execute_WithRunningGame_GameIsRunningGameIsTrue()
        {
            const string slug = "a";
            var homegame = new FakeHomegame();
            var cashgame = new FakeCashgame();
            var request = new CashgameContextRequest { Slug = slug };

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<ICashgameRepository>().Setup(o => o.GetRunning(homegame)).Returns(cashgame);

            var result = _sut.Execute(request);

            Assert.IsTrue(result.GameIsRunning);
        }

        [Test]
        public void Execute_WithoutYears_YearsAreEmpty()
        {
            const string slug = "a";
            var homegame = new FakeHomegame();
            var years = new List<int>();
            var request = new CashgameContextRequest { Slug = slug };

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<ICashgameRepository>().Setup(o => o.GetYears(homegame)).Returns(years);

            var result = _sut.Execute(request);

            Assert.IsEmpty(result.Years);
        }

        [Test]
        public void Execute_WithYears_YearsContainsThem()
        {
            const string slug = "a";
            var homegame = new FakeHomegame();
            var years = new List<int>{1, 2, 3};
            var request = new CashgameContextRequest { Slug = slug };

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<ICashgameRepository>().Setup(o => o.GetYears(homegame)).Returns(years);

            var result = _sut.Execute(request);

            Assert.AreEqual(3, result.Years.Count);
        }
    }
}