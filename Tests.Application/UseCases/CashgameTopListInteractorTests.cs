using System;
using System.Collections.Generic;
using Application.UseCases.CashgameTopList;
using Core.Classes;
using Core.Repositories;
using Core.Services.Interfaces;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class CashgameTopListInteractorTests : MockContainer
    {
        private CashgameTopListInteractor _sut;

        [SetUp]
        public virtual void SetUp()
        {
            _sut = new CashgameTopListInteractor(
                GetMock<IHomegameRepository>().Object,
                GetMock<ICashgameService>().Object);
        }

        [Test]
        public void Execute_WithSlug_ReturnsTopListItems()
        {
            const string slug = "a";
            var homegame = new FakeHomegame();
            var totalResult = new FakeCashgameTotalResult();
            var totalResultList = new List<CashgameTotalResult> {totalResult};
            var playerList = new List<Player>();
            var suite = new FakeCashgameSuite(totalResults: totalResultList, players: playerList);
            var request = new CashgameTopListRequest{Slug = slug};

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<ICashgameService>().Setup(o => o.GetSuite(homegame, null)).Returns(suite);

            var result = _sut.Execute(request);

            Assert.AreEqual(ToplistSortOrder.Winnings, result.OrderBy);
            Assert.AreEqual(1, result.Items.Count);
        }

        [Test]
        public void CreateItem_AllPropertiesAreSet()
        {
            const string playerName = "a";
            const int playerId = 1;
            const int buyin = 2;
            const int cashout = 3;
            const int gamesPlayed = 4;
            const int minutesPlayed = 5;
            const int winnings = 6;
            const int winRate = 7;
            const int expectedRank = 1;
            var totalResult = new FakeCashgameTotalResult(playerId: playerId, buyin: buyin, cashout: cashout, gameCount: gamesPlayed, timePlayed: minutesPlayed, winnings: winnings, winRate: winRate);
            const int index = 0;
            var currency = Currency.Default;
            var player = new FakePlayer(playerId, displayName: playerName);
            var players = new List<Player>{player};

            var result = _sut.CreateItem(totalResult, index, currency, players);

            Assert.AreEqual(expectedRank, result.Rank);
            Assert.AreEqual(buyin, result.Buyin.Amount);
            Assert.AreEqual(cashout, result.Cashout.Amount);
            Assert.AreEqual(gamesPlayed, result.GamesPlayed);
            Assert.AreEqual(minutesPlayed, result.MinutesPlayed.TotalMinutes);
            Assert.AreEqual(playerName, result.Name);
            Assert.AreEqual(winnings, result.Winnings.Amount);
            Assert.AreEqual(winRate, result.WinRate.Amount);
        }
    }
}
