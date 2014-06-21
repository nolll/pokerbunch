using System.Collections.Generic;
using Application.UseCases.CashgameTopList;
using Core.Entities;
using NUnit.Framework;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class TopListResultTests
    {
        [Test]
        public void Execute_WithSlug_ReturnsTopListItems()
        {
            const string slug = "a";
            const int year = 1;
            var homegame = new HomegameInTest(slug: slug);
            var totalResult = new CashgameTotalResultInTest(player: new PlayerInTest());
            var totalResultList = new List<CashgameTotalResult> { totalResult };

            var result = new TopListResult(homegame, totalResultList, ToplistSortOrder.Winnings, year);

            Assert.IsInstanceOf<TopListResult>(result);
            Assert.AreEqual(1, result.Items.Count);
            Assert.AreEqual(ToplistSortOrder.Winnings, result.OrderBy);
            Assert.AreEqual(year, result.Year);
            Assert.AreEqual(slug, result.Slug);
        }

        [Test]
        public void SortItems_SortByWinnings_HighestWinningsIsFirst()
        {
            const int low = 1;
            const int high = 2;
            var item1 = new TopListItemInTest(winnings: new Money(low));
            var item2 = new TopListItemInTest(winnings: new Money(high));
            var items = new List<TopListItem> { item1, item2 };

            var result = TopListResult.SortItems(items, ToplistSortOrder.Winnings);

            Assert.AreEqual(high, result[0].Winnings.Amount);
        }

        [Test]
        public void SortItems_SortByBuyin_HighestBuyinIsFirst()
        {
            const int low = 1;
            const int high = 2;
            var item1 = new TopListItemInTest(buyin: new Money(low));
            var item2 = new TopListItemInTest(buyin: new Money(high));
            var items = new List<TopListItem> { item1, item2 };

            var result = TopListResult.SortItems(items, ToplistSortOrder.Buyin);

            Assert.AreEqual(high, result[0].Buyin.Amount);
        }

        [Test]
        public void SortItems_SortByCashout_HighestCashoutIsFirst()
        {
            const int low = 1;
            const int high = 2;
            var item1 = new TopListItemInTest(cashout: new Money(low));
            var item2 = new TopListItemInTest(cashout: new Money(high));
            var items = new List<TopListItem> { item1, item2 };

            var result = TopListResult.SortItems(items, ToplistSortOrder.Cashout);

            Assert.AreEqual(high, result[0].Cashout.Amount);
        }

        [Test]
        public void SortItems_SortByTimePlayed_HighestTotalMinutesIsFirst()
        {
            const int low = 1;
            const int high = 2;
            var item1 = new TopListItemInTest(timePlayed: Time.FromMinutes(low));
            var item2 = new TopListItemInTest(timePlayed: Time.FromMinutes(high));
            var items = new List<TopListItem> { item1, item2 };

            var result = TopListResult.SortItems(items, ToplistSortOrder.TimePlayed);

            Assert.AreEqual(high, result[0].TimePlayed.Minutes);
        }

        [Test]
        public void SortItems_SortByGamesPlayed_HighestGameCountIsFirst()
        {
            const int low = 1;
            const int high = 2;
            var item1 = new TopListItemInTest(gamesPlayed: low);
            var item2 = new TopListItemInTest(gamesPlayed: high);
            var items = new List<TopListItem> { item1, item2 };

            var result = TopListResult.SortItems(items, ToplistSortOrder.GamesPlayed);

            Assert.AreEqual(high, result[0].GamesPlayed);
        }

        [Test]
        public void SortItems_SortByWinRate_HighestWinRateIsFirst()
        {
            const int low = 1;
            const int high = 2;
            var item1 = new TopListItemInTest(winRate: new Money(low));
            var item2 = new TopListItemInTest(winRate: new Money(high));
            var items = new List<TopListItem> { item1, item2 };

            var result = TopListResult.SortItems(items, ToplistSortOrder.WinRate);

            Assert.AreEqual(high, result[0].WinRate.Amount);
        }
    }
}