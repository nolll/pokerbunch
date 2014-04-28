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
        public void Execute_NoSlug_ThrowsException()
        {
            var request = new CashgameTopListRequest();

            Assert.Throws<ArgumentException>(() => _sut.Execute(request));
        }

        [Test]
        public void Execute_WithSlug_ReturnsTopListItems()
        {
            const string slug = "a";
            const string playerName = "b";
            const int rank = 1;
            const int playerId = 1;
            const int buyin = 2;
            const int cashout = 3;
            const int gamesPlayed = 4;
            const int minutesPlayed = 5;
            const int winnings = 6;
            const int winRate = 7;
            var homegame = new FakeHomegame();
            var totalResult = new FakeCashgameTotalResult(playerId: playerId, buyin: buyin, cashout: cashout, gameCount: gamesPlayed, timePlayed: minutesPlayed, winnings: winnings, winRate: winRate);
            var totalResultList = new List<CashgameTotalResult> {totalResult};
            var player = new FakePlayer(id: playerId, displayName: playerName);
            var playerList = new List<Player> {player};
            var suite = new FakeCashgameSuite(totalResults: totalResultList, players: playerList);
            var request = new CashgameTopListRequest{Slug = slug};

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<ICashgameService>().Setup(o => o.GetSuite(homegame, null)).Returns(suite);

            var result = _sut.Execute(request);

            Assert.AreEqual(ToplistSortOrder.Winnings, result.OrderBy);
            Assert.AreEqual(1, result.Items.Count);
            Assert.AreEqual(rank, result.Items[0].Rank);
            Assert.AreEqual(buyin, result.Items[0].Buyin.Amount);
            Assert.AreEqual(cashout, result.Items[0].Cashout.Amount);
            Assert.AreEqual(gamesPlayed, result.Items[0].GamesPlayed);
            Assert.AreEqual(minutesPlayed, result.Items[0].MinutesPlayed.TotalMinutes);
            Assert.AreEqual(playerName, result.Items[0].Name);
            Assert.AreEqual(winnings, result.Items[0].Winnings.Amount);
            Assert.AreEqual(winRate, result.Items[0].WinRate.Amount);
        }
    }
}
