using System.Collections.Generic;
using Application.UseCases.CashgameTopList;
using Core.Entities;
using Core.Repositories;
using Core.Services.Interfaces;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.Builders;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class TopListTests : TestBase
    {
        [Test]
        public void TopList_ReturnsTopListItems()
        {
            var request = new TopListRequest("a", "winnings", 1);

            var totalResult1 = new CashgameTotalResultBuilder().Build();
            SetupSuite(totalResult1);
            
            var result = Execute(request);

            Assert.AreEqual(1, result.Items.Count);
            Assert.AreEqual(ToplistSortOrder.Winnings, result.OrderBy);
            Assert.AreEqual(1, result.Year);
            Assert.AreEqual("a", result.Slug);
        }

        [Test]
        public void TopList_ItemHasCorrectValues()
        {
            var player = new PlayerInTest(1, displayName: "a");
            var totalResult1 = new CashgameTotalResultBuilder()
                .WithPlayer(player)
                .WithBuyin(2)
                .WithCashout(3)
                .WithGamesPlayed(4)
                .WithMinutesPlayed(5)
                .WithWinnings(6)
                .WithWinRate(7)
                .Build();

            var request = new TopListRequest("a", "winnings", 1);

            SetupSuite(totalResult1);

            var result = Execute(request);

            Assert.AreEqual(1, result.Items[0].Rank);
            Assert.AreEqual(2, result.Items[0].Buyin.Amount);
            Assert.AreEqual(3, result.Items[0].Cashout.Amount);
            Assert.AreEqual(4, result.Items[0].GamesPlayed);
            Assert.AreEqual(5, result.Items[0].TimePlayed.Minutes);
            Assert.AreEqual("a", result.Items[0].Name);
            Assert.AreEqual(1, result.Items[0].PlayerId);
            Assert.AreEqual(6, result.Items[0].Winnings.Amount);
            Assert.AreEqual(7, result.Items[0].WinRate.Amount);
        }

        [TestCase("winnings")]
        [TestCase(null)]
        public void TopList_SortByWinnings_HighestWinningsIsFirst(string orderBy)
        {
            const int low = 1;
            const int high = 2;

            var request = new TopListRequest("a", orderBy, 1);

            var totalResult1 = new CashgameTotalResultBuilder().WithWinnings(low).Build();
            var totalResult2 = new CashgameTotalResultBuilder().WithWinnings(high).Build();
            SetupSuite(totalResult1, totalResult2);

            var result = Execute(request);

            Assert.AreEqual(high, result.Items[0].Winnings.Amount);
            Assert.AreEqual(low, result.Items[1].Winnings.Amount);
        }

        [Test]
        public void TopList_SortByBuyin_HighestBuyinIsFirst()
        {
            const int low = 1;
            const int high = 2;

            var request = new TopListRequest("a", "buyin", 1);

            var totalResult1 = new CashgameTotalResultBuilder().WithBuyin(low).Build();
            var totalResult2 = new CashgameTotalResultBuilder().WithBuyin(high).Build();
            SetupSuite(totalResult1, totalResult2);

            var result = Execute(request);

            Assert.AreEqual(high, result.Items[0].Buyin.Amount);
            Assert.AreEqual(low, result.Items[1].Buyin.Amount);
        }

        [Test]
        public void TopList_SortByCashout_HighestCashoutIsFirst()
        {
            const int low = 1;
            const int high = 2;

            var request = new TopListRequest("a", "cashout", 1);

            var totalResult1 = new CashgameTotalResultBuilder().WithCashout(low).Build();
            var totalResult2 = new CashgameTotalResultBuilder().WithCashout(high).Build();
            SetupSuite(totalResult1, totalResult2);

            var result = Execute(request);

            Assert.AreEqual(high, result.Items[0].Cashout.Amount);
            Assert.AreEqual(low, result.Items[1].Cashout.Amount);
        }

        [Test]
        public void TopList_SortByTimePlayed_HighestTotalMinutesIsFirst()
        {
            const int low = 1;
            const int high = 2;

            var request = new TopListRequest("a", "timeplayed", 1);

            var totalResult1 = new CashgameTotalResultBuilder().WithMinutesPlayed(low).Build();
            var totalResult2 = new CashgameTotalResultBuilder().WithMinutesPlayed(high).Build();
            SetupSuite(totalResult1, totalResult2);

            var result = Execute(request);

            Assert.AreEqual(high, result.Items[0].TimePlayed.Minutes);
            Assert.AreEqual(low, result.Items[1].TimePlayed.Minutes);
        }

        [Test]
        public void TopList_SortByGamesPlayed_HighestGameCountIsFirst()
        {
            const int low = 1;
            const int high = 2;

            var request = new TopListRequest("a", "gamesplayed", 1);

            var totalResult1 = new CashgameTotalResultBuilder().WithGamesPlayed(low).Build();
            var totalResult2 = new CashgameTotalResultBuilder().WithGamesPlayed(high).Build();
            SetupSuite(totalResult1, totalResult2);

            var result = Execute(request);

            Assert.AreEqual(high, result.Items[0].GamesPlayed);
            Assert.AreEqual(low, result.Items[1].GamesPlayed);
        }

        [Test]
        public void TopList_SortByWinRate_HighestWinRateIsFirst()
        {
            const int low = 1;
            const int high = 2;

            var request = new TopListRequest("a", "winrate", 1);

            var totalResult1 = new CashgameTotalResultBuilder().WithWinRate(low).Build();
            var totalResult2 = new CashgameTotalResultBuilder().WithWinRate(high).Build();
            SetupSuite(totalResult1, totalResult2);

            var result = Execute(request);

            Assert.AreEqual(high, result.Items[0].WinRate.Amount);
            Assert.AreEqual(low, result.Items[1].WinRate.Amount);
        }

        private void SetupSuite(CashgameTotalResult totalResult1, CashgameTotalResult totalResult2 = null)
        {
            var homegame = A.Bunch.Build();
            var suite = BuildSuite(totalResult1, totalResult2);

            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(It.IsAny<string>())).Returns(homegame);
            GetMock<ICashgameService>().Setup(o => o.GetSuite(homegame, It.IsAny<int?>())).Returns(suite);
        }

        private CashgameSuite BuildSuite(CashgameTotalResult totalResult1, CashgameTotalResult totalResult2 = null)
        {
            var totalResultList = new List<CashgameTotalResult> { totalResult1 };
            if(totalResult2 != null)
                totalResultList.Add(totalResult2);
            return new CashgameSuiteInTest(totalResults: totalResultList);
        }

        private TopListResult Execute(TopListRequest request)
        {
            return TopListInteractor.Execute(GetMock<IBunchRepository>().Object, GetMock<ICashgameService>().Object, request);
        }
    }
}