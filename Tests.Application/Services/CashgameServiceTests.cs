﻿using System.Collections.Generic;
using Application.Factories.Interfaces;
using Application.Services;
using Core.Classes;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.Services
{
    public class CashgameServiceTests : MockContainer
    {
        [TestCase(null)]
        [TestCase(1)]
        public void GetSuite_WithOrWithoutYear_ReturnsSuite(int? year)
        {
            var homegame = new FakeHomegame();
            var players = new List<Player>();
            var cashgames = new List<Cashgame>();
            var suite = new FakeCashgameSuite();

            GetMock<IPlayerRepository>().Setup(o => o.GetList(homegame)).Returns(players);
            GetMock<ICashgameRepository>().Setup(o => o.GetPublished(homegame, year)).Returns(cashgames);
            GetMock<ICashgameSuiteFactory>().Setup(o => o.Create(cashgames, players)).Returns(suite);

            var sut = GetSut();
            var result = sut.GetSuite(homegame, year);

            Assert.AreEqual(suite, result);
        }

        [TestCase(null)]
        [TestCase(1)]
        public void GetFacts_WithOrWithoutYear_ReturnsSuite(int? year)
        {
            var homegame = new FakeHomegame();
            var players = new List<Player>();
            var cashgames = new List<Cashgame>();
            var facts = new FakeCashgameFacts();

            GetMock<IPlayerRepository>().Setup(o => o.GetList(homegame)).Returns(players);
            GetMock<ICashgameRepository>().Setup(o => o.GetPublished(homegame, year)).Returns(cashgames);
            GetMock<ICashgameFactsFactory>().Setup(o => o.Create(cashgames, players)).Returns(facts);

            var sut = GetSut();
            var result = sut.GetFacts(homegame, year);

            Assert.AreEqual(facts, result);
        }

        [Test]
        public void GetPlayers_WithCashgameWithTwoResults_CallsGetListWithTwoIds()
        {
            const int playerId1 = 1;
            const int playerId2 = 2;
            var playerIds = new List<int> {playerId1, playerId2};
            var result1 = new FakeCashgameResult(playerId1);
            var result2 = new FakeCashgameResult(playerId2);
            var results = new List<CashgameResult>{result1, result2};
            var cashgame = new FakeCashgame(results: results);
            var players = new List<Player>();

            GetMock<IPlayerRepository>().Setup(o => o.GetList(playerIds)).Returns(players);

            var sut = GetSut();
            var result = sut.GetPlayers(cashgame);

            Assert.AreEqual(players, result);
        }

        [Test]
        public void CashgameIsRunning_WithoutRunningGame_ReturnsFalse()
        {
            const string slug = "a";

            var sut = GetSut();
            var result = sut.CashgameIsRunning(slug);

            Assert.IsFalse(result);
        }

        [Test]
        public void CashgameIsRunning_WithRunningGame_ReturnsTrue()
        {
            const string slug = "a";
            var homegame = new FakeHomegame();
            var cashgame = new FakeCashgame();

            GetMock<IHomegameRepository>().Setup(o => o.GetByName(slug)).Returns(homegame);
            GetMock<ICashgameRepository>().Setup(o => o.GetRunning(homegame)).Returns(cashgame);

            var sut = GetSut();
            var result = sut.CashgameIsRunning(slug);

            Assert.IsTrue(result);
        }

        [Test]
        public void GetLatestYear_WithoutGames_ReturnsNull()
        {
            const string slug = "a";
            var homegame = new FakeHomegame();
            var years = new List<int>();
            
            GetMock<IHomegameRepository>().Setup(o => o.GetByName(slug)).Returns(homegame);
            GetMock<ICashgameRepository>().Setup(o => o.GetYears(homegame)).Returns(years);

            var sut = GetSut();
            var result = sut.GetLatestYear(slug);

            Assert.IsNull(result);
        }

        [Test]
        public void GetLatestYear_WithGames_ReturnsFirstYearInList()
        {
            const string slug = "a";
            var homegame = new FakeHomegame();
            const int latestYear = 2;
            const int previousYear = 1;
            var years = new List<int>{latestYear, previousYear};

            GetMock<IHomegameRepository>().Setup(o => o.GetByName(slug)).Returns(homegame);
            GetMock<ICashgameRepository>().Setup(o => o.GetYears(homegame)).Returns(years);

            var sut = GetSut();
            var result = sut.GetLatestYear(slug);

            Assert.AreEqual(latestYear, result);
        }

        private CashgameService GetSut()
        {
            return new CashgameService(
                GetMock<IPlayerRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                GetMock<ICashgameSuiteFactory>().Object,
                GetMock<ICashgameFactsFactory>().Object,
                GetMock<IHomegameRepository>().Object);
        }
    }
}