using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases.CashgameList;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class CashgameListTests : TestBase
    {
        private const string Slug = "a";
        private const int Year = 1;

        [Test]
        public void CashgameList_SlugIsSet()
        {
            SetupEmptyGameList();

            var result = Execute(CreateRequest());

            Assert.AreEqual(Slug, result.Slug);
        }

        [Test]
        public void CashgameList_WithoutYear_YearIsNull()
        {
            SetupEmptyGameList();

            var result = Execute(CreateRequest());

            Assert.IsFalse(result.ShowYear);
            Assert.IsNull(result.Year);
        }

        [Test]
        public void CashgameList_HasEmptyListOfGames()
        {
            SetupEmptyGameList();

            var result = Execute(CreateRequest());

            Assert.AreEqual(0, result.List.Count);
        }

        [Test]
        public void CashgameList_WithYear_YearIsSet()
        {
            var cashgame = A.Cashgame.Build();
            var cashgameList = new List<Cashgame> {cashgame};
            SetupCashgames(cashgameList, Year);

            var result = Execute(CreateRequest(year: Year));

            Assert.IsTrue(result.ShowYear);
            Assert.AreEqual(Year, result.Year);
        }

        [Test]
        public void CashgameList_WithOneGame_LocationIsSet()
        {
            SetupSingleGame();

            var result = Execute(CreateRequest());

            Assert.AreEqual("Location", result.List[0].Location);
        }

        [Test]
        public void CashgameList_WithOneGame_UrlIsSet()
        {
            SetupSingleGame();

            var result = Execute(CreateRequest());

            Assert.AreEqual("/a/cashgame/details/2001-01-01", result.List[0].Url.Relative);
        }

        [Test]
        public void CashgameList_WithOneGame_DurationIsSet()
        {
            SetupSingleGame();

            var result = Execute(CreateRequest());

            Assert.AreEqual(1, result.List[0].Duration.Minutes);
        }

        [Test]
        public void CashgameList_WithOneGame_DateIsSet()
        {
            SetupSingleGame();

            var result = Execute(CreateRequest());

            var expected = new Date(2001, 1, 1);
            Assert.AreEqual(expected, result.List[0].Date);
        }

        [Test]
        public void CashgameList_WithOneGame_PlayerCountIsSet()
        {
            SetupSingleGame();

            var result = Execute(CreateRequest());

            Assert.AreEqual(4, result.List[0].PlayerCount);
        }

        [Test]
        public void CashgameList_WithOneGame_TurnoverIsSet()
        {
            SetupSingleGame();

            var result = Execute(CreateRequest());

            Assert.AreEqual(2, result.List[0].Turnover.Amount);
        }

        [Test]
        public void CashgameList_WithOneGame_AverageBuyinIsSet()
        {
            SetupSingleGame();

            var result = Execute(CreateRequest());

            Assert.AreEqual(3, result.List[0].AverageBuyin.Amount);
        }

        [TestCase("date")]
        [TestCase(null)]
        public void TopList_SortByWinnings_HighestWinningsIsFirst(string orderBy)
        {
            var low = new DateTime(2001, 1, 1, 1, 1, 1);
            var high = new DateTime(2001, 1, 2, 1, 1, 1);

            var cashgame1 = A.Cashgame.WithStartTime(low).Build();
            var cashgame2 = A.Cashgame.WithStartTime(high).Build();
            SetupTwoGames(cashgame1, cashgame2);

            var result = Execute(CreateRequest(orderBy));

            Assert.AreEqual(ListSortOrder.Date, result.SortOrder);
            Assert.AreEqual(new Date(2001, 1, 2), result.List[0].Date);
            Assert.AreEqual(new Date(2001, 1, 1), result.List[1].Date);
        }

        [Test]
        public void TopList_SortByPlayerCount_HighestPlayerCountIsFirst()
        {
            const int low = 1;
            const int high = 2;

            var cashgame1 = A.Cashgame.WithPlayerCount(low).Build();
            var cashgame2 = A.Cashgame.WithPlayerCount(high).Build();
            SetupTwoGames(cashgame1, cashgame2);

            var result = Execute(CreateRequest("playercount"));

            Assert.AreEqual(ListSortOrder.PlayerCount, result.SortOrder);
            Assert.AreEqual(high, result.List[0].PlayerCount);
            Assert.AreEqual(low, result.List[1].PlayerCount);
        }

        [Test]
        public void TopList_SortByLocation_HighestLocationIsFirst()
        {
            const string low = "a";
            const string high = "b";

            var cashgame1 = A.Cashgame.WithLocation(low).Build();
            var cashgame2 = A.Cashgame.WithLocation(high).Build();
            SetupTwoGames(cashgame1, cashgame2);

            var result = Execute(CreateRequest("location"));

            Assert.AreEqual(ListSortOrder.Location, result.SortOrder);
            Assert.AreEqual(high, result.List[0].Location);
            Assert.AreEqual(low, result.List[1].Location);
        }

        [Test]
        public void TopList_SortByDuration_HighestDurationIsFirst()
        {
            var lowStartTime = new DateTime(2001, 1, 1, 1, 1, 1);
            var lowEndTime = new DateTime(2001, 1, 1, 1, 2, 1);
            var highStartTime = new DateTime(2001, 1, 2, 1, 1, 1);
            var highEndTime = new DateTime(2001, 1, 2, 1, 3, 1);
            
            var cashgame1 = A.Cashgame.WithStartTime(lowStartTime).WithEndTime(lowEndTime).Build();
            var cashgame2 = A.Cashgame.WithStartTime(highStartTime).WithEndTime(highEndTime).Build();
            SetupTwoGames(cashgame1, cashgame2);

            var result = Execute(CreateRequest("duration"));

            Assert.AreEqual(ListSortOrder.Duration, result.SortOrder);
            Assert.AreEqual(2, result.List[0].Duration.Minutes);
            Assert.AreEqual(1, result.List[1].Duration.Minutes);
        }

        [Test]
        public void TopList_SortByTurnover_HighestTurnoverIsFirst()
        {
            const int low = 1;
            const int high = 2;

            var cashgame1 = A.Cashgame.WithTurnover(low).Build();
            var cashgame2 = A.Cashgame.WithTurnover(high).Build();
            SetupTwoGames(cashgame1, cashgame2);

            var result = Execute(CreateRequest("turnover"));

            Assert.AreEqual(ListSortOrder.Turnover, result.SortOrder);
            Assert.AreEqual(high, result.List[0].Turnover.Amount);
            Assert.AreEqual(low, result.List[1].Turnover.Amount);
        }

        [Test]
        public void TopList_SortByAverageBuyin_HighestAverageBuyinIsFirst()
        {
            const int low = 1;
            const int high = 2;

            var cashgame1 = A.Cashgame.WithAverageBuyin(low).Build();
            var cashgame2 = A.Cashgame.WithAverageBuyin(high).Build();
            SetupTwoGames(cashgame1, cashgame2);

            var result = Execute(CreateRequest("averagebuyin"));

            Assert.AreEqual(ListSortOrder.AverageBuyin, result.SortOrder);
            Assert.AreEqual(high, result.List[0].AverageBuyin.Amount);
            Assert.AreEqual(low, result.List[1].AverageBuyin.Amount);
        }

        private void SetupTwoGames(Cashgame cashgame1, Cashgame cashgame2)
        {
            var cashgames = new List<Cashgame>{cashgame1, cashgame2};
            SetupCashgames(cashgames);
        }

        private void SetupEmptyGameList()
        {
            var cashgames = new List<Cashgame>();
            SetupCashgames(cashgames);
        }

        private void SetupSingleGame()
        {
            var cashgame = A.Cashgame.Build();
            var cashgames = new List<Cashgame> { cashgame };
            SetupCashgames(cashgames);
        }

        private CashgameListRequest CreateRequest(string orderBy = null, int? year = null)
        {
            return new CashgameListRequest(Slug, orderBy, year);
        }

        private void SetupBunch(Bunch bunch)
        {
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(bunch);
        }

        private void SetupCashgames(IList<Cashgame> cashgames, int? year = null)
        {
            var bunch = A.Bunch.Build();
            SetupBunch(bunch);
            GetMock<ICashgameRepository>().Setup(o => o.GetPublished(It.IsAny<Bunch>(), year)).Returns(cashgames);
        }

        private CashgameListResult Execute(CashgameListRequest request)
        {
            return CashgameListInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                request);
        }
    }
}
