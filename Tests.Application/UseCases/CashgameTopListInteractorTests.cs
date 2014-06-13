using System.Collections.Generic;
using Application.UseCases.CashgameTopList;
using Core.Entities;
using Core.Repositories;
using Core.Services.Interfaces;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class CashgameTopListInteractorTests : MockContainer
    {
        private TopListInteractor _sut;

        [SetUp]
        public virtual void SetUp()
        {
            _sut = new TopListInteractor(
                GetMock<IHomegameRepository>().Object,
                GetMock<ICashgameService>().Object);
        }

        [Test]
        public void Execute_WithSlug_ReturnsTopListItems()
        {
            const string slug = "a";
            var homegame = new HomegameInTest();
            var totalResult = new CashgameTotalResultInTest(player: new PlayerInTest());
            var totalResultList = new List<CashgameTotalResult> {totalResult};
            var playerList = new List<Player>();
            var suite = new CashgameSuiteInTest(totalResults: totalResultList, players: playerList);
            var request = new TopListRequest{Slug = slug};

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
            var player = new PlayerInTest(playerId, displayName: playerName);
            var totalResult = new CashgameTotalResultInTest(player: player, buyin: buyin, cashout: cashout, gameCount: gamesPlayed, timePlayed: minutesPlayed, winnings: winnings, winRate: winRate);
            const int index = 0;
            var currency = Currency.Default;
            
            var result = _sut.CreateItem(totalResult, index, currency);

            Assert.AreEqual(expectedRank, result.Rank);
            Assert.AreEqual(buyin, result.Buyin.Amount);
            Assert.AreEqual(cashout, result.Cashout.Amount);
            Assert.AreEqual(gamesPlayed, result.GamesPlayed);
            Assert.AreEqual(minutesPlayed, result.TimePlayed.Minutes);
            Assert.AreEqual(playerName, result.Name);
            Assert.AreEqual(playerId, result.PlayerId);
            Assert.AreEqual(winnings, result.Winnings.Amount);
            Assert.AreEqual(winRate, result.WinRate.Amount);
        }

        [Test]
        public void SortItems_SortByWinnings_HighestWinningsIsFirst()
        {
            const int low = 1;
            const int high = 2;
            var item1 = new TopListItem { Winnings = new Money(low) };
            var item2 = new TopListItem { Winnings = new Money(high) };
            var items = new List<TopListItem> { item1, item2 };

            var result = _sut.SortItems(items, ToplistSortOrder.Winnings);

            Assert.AreEqual(high, result[0].Winnings.Amount);
        }

        [Test]
        public void SortItems_SortByBuyin_HighestBuyinIsFirst()
        {
            const int low = 1;
            const int high = 2;
            var item1 = new TopListItem { Buyin = new Money(low) };
            var item2 = new TopListItem { Buyin = new Money(high) };
            var items = new List<TopListItem> { item1, item2 };

            var result = _sut.SortItems(items, ToplistSortOrder.Buyin);

            Assert.AreEqual(high, result[0].Buyin.Amount);
        }

        [Test]
        public void SortItems_SortByCashout_HighestCashoutIsFirst()
        {
            const int low = 1;
            const int high = 2;
            var item1 = new TopListItem { Cashout = new Money(low) };
            var item2 = new TopListItem { Cashout = new Money(high) };
            var items = new List<TopListItem> { item1, item2 };

            var result = _sut.SortItems(items, ToplistSortOrder.Cashout);

            Assert.AreEqual(high, result[0].Cashout.Amount);
        }

        [Test]
        public void SortItems_SortByTimePlayed_HighestTotalMinutesIsFirst()
        {
            const int low = 1;
            const int high = 2;
            var item1 = new TopListItem { TimePlayed = Time.FromMinutes(low) };
            var item2 = new TopListItem { TimePlayed = Time.FromMinutes(high) };
            var items = new List<TopListItem> { item1, item2 };

            var result = _sut.SortItems(items, ToplistSortOrder.TimePlayed);

            Assert.AreEqual(high, result[0].TimePlayed.Minutes);
        }

        [Test]
        public void SortItems_SortByGamesPlayed_HighestGameCountIsFirst()
        {
            const int low = 1;
            const int high = 2;
            var item1 = new TopListItem { GamesPlayed = low };
            var item2 = new TopListItem { GamesPlayed = high };
            var items = new List<TopListItem> { item1, item2 };

            var result = _sut.SortItems(items, ToplistSortOrder.GamesPlayed);

            Assert.AreEqual(high, result[0].GamesPlayed);
        }

        [Test]
        public void SortItems_SortByWinRate_HighestWinRateIsFirst()
        {
            const int low = 1;
            const int high = 2;
            var item1 = new TopListItem { WinRate = new Money(low) };
            var item2 = new TopListItem { WinRate = new Money(high) };
            var items = new List<TopListItem> { item1, item2 };

            var result = _sut.SortItems(items, ToplistSortOrder.WinRate);

            Assert.AreEqual(high, result[0].WinRate.Amount);
        }
    }
}
