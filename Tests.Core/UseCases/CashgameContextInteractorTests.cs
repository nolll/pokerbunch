using System.Collections.Generic;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Core.UseCases
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
            var request = new CashgameContextRequest { Slug = slug };

            var result = _sut.Execute(request);

            Assert.IsFalse(result.GameIsRunning);
        }

        [Test]
        public void Execute_WithYear_SelectedYearIsSet()
        {
            const int year = 1;
            var request = new CashgameContextRequest { Year = year };

            var result = _sut.Execute(request);

            Assert.AreEqual(year, result.SelectedYear);
        }

        [Test]
        public void Execute_WithoutRunningGame_GameIsRunningGameIsFalse()
        {
            const string slug = "a";
            var request = new CashgameContextRequest { Slug = slug };

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

    public class CashgameContextRequest
    {
        public string Slug { get; set; }
        public int Year { get; set; }
    }

    public class CashgameContextInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameContextInteractor(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
        }

        public CashgameContextResult Execute(CashgameContextRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var runningGame = _cashgameRepository.GetRunning(homegame);

            var gameIsRunning = runningGame != null;
            var years = _cashgameRepository.GetYears(homegame);

            return new CashgameContextResult
                {
                    GameIsRunning = gameIsRunning,
                    Years = years,
                    Slug = request.Slug,
                    SelectedYear = request.Year
                };
        }
    }

    public class CashgameContextResult
    {
        public bool GameIsRunning { get; set; }
        public IList<int> Years { get; set; }
        public string Slug { get; set; }
        public int SelectedYear { get; set; }
    }
}
