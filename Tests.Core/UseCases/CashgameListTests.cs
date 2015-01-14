using Core.Entities;
using Core.UseCases.CashgameList;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class CashgameListTests : TestBase
    {
        private const int Year = 2001;

        [Test]
        public void CashgameList_SlugIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(Constants.SlugA, result.Slug);
        }

        [Test]
        public void CashgameList_WithoutYear_YearIsNull()
        {
            var result = Execute(CreateRequest());

            Assert.IsFalse(result.ShowYear);
            Assert.IsNull(result.Year);
        }

        [Test]
        public void CashgameList_WithoutGames_HasEmptyListOfGames()
        {
            Repos.Cashgame.ClearList();

            var result = Execute(CreateRequest());

            Assert.AreEqual(0, result.List.Count);
        }

        [Test]
        public void CashgameList_WithYear_YearIsSet()
        {
            var result = Execute(CreateRequest(year: Year));

            Assert.IsTrue(result.ShowYear);
            Assert.AreEqual(Year, result.Year);
        }

        [Test]
        public void CashgameList_DefaultSort_FirstItemLocationIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(Constants.LocationB, result.List[0].Location);
        }

        [Test]
        public void CashgameList_DefaultSort_FirstItemUrlIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual("/bunch-a/cashgame/details/2002-03-04", result.List[0].Url.Relative);
        }

        [Test]
        public void CashgameList_DefaultSort_FirstItemDurationIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(92, result.List[0].Duration.Minutes);
        }

        [Test]
        public void CashgameList_DefaultSort_FirstItemDateIsSet()
        {
            var result = Execute(CreateRequest());

            var expected = new Date(2002, 3, 4);
            Assert.AreEqual(expected, result.List[0].Date);
        }

        [Test]
        public void CashgameList_DefaultSort_FirstItemPlayerCountIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(2, result.List[0].PlayerCount);
        }

        [Test]
        public void CashgameList_DefaultSort_FirstItemTurnoverIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(600, result.List[0].Turnover.Amount);
        }

        [Test]
        public void CashgameList_DefaultSort_FirstItemAverageBuyinIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(300, result.List[0].AverageBuyin.Amount);
        }

        [TestCase("date")]
        [TestCase(null)]
        public void TopList_SortByWinnings_HighestWinningsIsFirst(string orderBy)
        {
            var result = Execute(CreateRequest(orderBy));

            Assert.AreEqual(ListSortOrder.Date, result.SortOrder);
            Assert.AreEqual(new Date(2002, 3, 4), result.List[0].Date);
            Assert.AreEqual(new Date(2001, 2, 3), result.List[1].Date);
        }

        [Test]
        public void TopList_SortByPlayerCount_HighestPlayerCountIsFirst()
        {
            var result = Execute(CreateRequest("playercount"));

            Assert.AreEqual(ListSortOrder.PlayerCount, result.SortOrder);
            Assert.AreEqual(2, result.List[0].PlayerCount);
            Assert.AreEqual(2, result.List[1].PlayerCount);
        }

        [Test]
        public void TopList_SortByLocation_HighestLocationIsFirst()
        {
            var result = Execute(CreateRequest("location"));

            Assert.AreEqual(ListSortOrder.Location, result.SortOrder);
            Assert.AreEqual(Constants.LocationB, result.List[0].Location);
            Assert.AreEqual(Constants.LocationA, result.List[1].Location);
        }

        [Test]
        public void TopList_SortByDuration_HighestDurationIsFirst()
        {
            var result = Execute(CreateRequest("duration"));

            Assert.AreEqual(ListSortOrder.Duration, result.SortOrder);
            Assert.AreEqual(92, result.List[0].Duration.Minutes);
            Assert.AreEqual(62, result.List[1].Duration.Minutes);
        }

        [Test]
        public void TopList_SortByTurnover_HighestTurnoverIsFirst()
        {
            var result = Execute(CreateRequest("turnover"));

            Assert.AreEqual(ListSortOrder.Turnover, result.SortOrder);
            Assert.AreEqual(600, result.List[0].Turnover.Amount);
            Assert.AreEqual(400, result.List[1].Turnover.Amount);
        }

        [Test]
        public void TopList_SortByAverageBuyin_HighestAverageBuyinIsFirst()
        {
            var result = Execute(CreateRequest("averagebuyin"));

            Assert.AreEqual(ListSortOrder.AverageBuyin, result.SortOrder);
            Assert.AreEqual(300, result.List[0].AverageBuyin.Amount);
            Assert.AreEqual(200, result.List[1].AverageBuyin.Amount);
        }

        private CashgameListRequest CreateRequest(string orderBy = null, int? year = null)
        {
            return new CashgameListRequest(Constants.SlugA, orderBy, year);
        }

        private CashgameListResult Execute(CashgameListRequest request)
        {
            return CashgameListInteractor.Execute(
                Repos.Bunch,
                Repos.Cashgame,
                request);
        }
    }
}
