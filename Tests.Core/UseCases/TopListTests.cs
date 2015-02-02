using Core.UseCases.CashgameTopList;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class TopListTests : TestBase
    {
        [Test]
        public void TopList_ReturnsTopListItems()
        {
            var request = new TopListRequest(Constants.SlugA, "winnings", null);
            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.Items.Count);
            Assert.AreEqual(ToplistSortOrder.Winnings, result.OrderBy);
            Assert.AreEqual(null, result.Year);
            Assert.AreEqual(Constants.SlugA, result.Slug);
        }

        [Test]
        public void TopList_ItemHasCorrectValues()
        {
            var request = new TopListRequest(Constants.SlugA, "winnings", null);
            var result = Sut.Execute(request);

            Assert.AreEqual(1, result.Items[0].Rank);
            Assert.AreEqual(400, result.Items[0].Buyin.Amount);
            Assert.AreEqual(600, result.Items[0].Cashout.Amount);
            Assert.AreEqual(2, result.Items[0].GamesPlayed);
            Assert.AreEqual(152, result.Items[0].TimePlayed.Minutes);
            Assert.AreEqual(Constants.PlayerNameA, result.Items[0].Name);
            Assert.AreEqual("/bunch-a/player/details/1", result.Items[0].PlayerUrl.Relative);
            Assert.AreEqual(200, result.Items[0].Winnings.Amount);
            Assert.AreEqual(79, result.Items[0].WinRate.Amount);
        }

        [TestCase("winnings")]
        [TestCase(null)]
        public void TopList_SortByWinnings_HighestWinningsIsFirst(string orderBy)
        {
            var request = new TopListRequest(Constants.SlugA, orderBy, null);
            var result = Sut.Execute(request);

            Assert.AreEqual(200, result.Items[0].Winnings.Amount);
            Assert.AreEqual(-200, result.Items[1].Winnings.Amount);
        }

        [Test]
        public void TopList_SortByBuyin_HighestBuyinIsFirst()
        {
            var request = new TopListRequest(Constants.SlugA, "buyin", null);
            var result = Sut.Execute(request);

            Assert.AreEqual(600, result.Items[0].Buyin.Amount);
            Assert.AreEqual(400, result.Items[1].Buyin.Amount);
        }

        [Test]
        public void TopList_SortByCashout_HighestCashoutIsFirst()
        {
            var request = new TopListRequest(Constants.SlugA, "cashout", null);
            var result = Sut.Execute(request);

            Assert.AreEqual(600, result.Items[0].Cashout.Amount);
            Assert.AreEqual(400, result.Items[1].Cashout.Amount);
        }

        [Test]
        public void TopList_SortByTimePlayed_HighestTotalMinutesIsFirst()
        {
            var request = new TopListRequest(Constants.SlugA, "timeplayed", null);
            var result = Sut.Execute(request);

            Assert.AreEqual(152, result.Items[0].TimePlayed.Minutes);
            Assert.AreEqual(152, result.Items[1].TimePlayed.Minutes);
        }

        [Test]
        public void TopList_SortByGamesPlayed_HighestGameCountIsFirst()
        {
            var request = new TopListRequest(Constants.SlugA, "gamesplayed", null);
            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.Items[0].GamesPlayed);
            Assert.AreEqual(2, result.Items[1].GamesPlayed);
        }

        [Test]
        public void TopList_SortByWinRate_HighestWinRateIsFirst()
        {
            var request = new TopListRequest(Constants.SlugA, "winrate", null);
            var result = Sut.Execute(request);

            Assert.AreEqual(79, result.Items[0].WinRate.Amount);
            Assert.AreEqual(-79, result.Items[1].WinRate.Amount);
        }

        private TopListInteractor Sut
        {
            get
            {
                return new TopListInteractor(
                    Repos.Bunch,
                    Repos.Cashgame,
                    Repos.Player);
            }
        }
    }
}