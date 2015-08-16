using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class TopListTests : TestBase
    {
        [Test]
        public void TopList_ReturnsTopListItems()
        {
            var request = new TopList.Request(TestData.UserNameA, TestData.SlugA, TopList.SortOrder.Winnings, null);
            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.Items.Count);
            Assert.AreEqual(TopList.SortOrder.Winnings, result.OrderBy);
            Assert.AreEqual(null, result.Year);
            Assert.AreEqual(TestData.SlugA, result.Slug);
        }

        [Test]
        public void TopList_ItemHasCorrectValues()
        {
            var request = new TopList.Request(TestData.UserNameA, TestData.SlugA, TopList.SortOrder.Winnings, null);
            var result = Sut.Execute(request);

            Assert.AreEqual(1, result.Items[0].Rank);
            Assert.AreEqual(400, result.Items[0].Buyin.Amount);
            Assert.AreEqual(600, result.Items[0].Cashout.Amount);
            Assert.AreEqual(2, result.Items[0].GamesPlayed);
            Assert.AreEqual(152, result.Items[0].TimePlayed.Minutes);
            Assert.AreEqual(TestData.PlayerNameA, result.Items[0].Name);
            Assert.AreEqual(1, result.Items[0].PlayerId);
            Assert.AreEqual(200, result.Items[0].Winnings.Amount);
            Assert.AreEqual(79, result.Items[0].WinRate.Amount);
        }

        [Test]
        public void TopList_SortByWinnings_HighestWinningsIsFirst()
        {
            var request = new TopList.Request(TestData.UserNameA, TestData.SlugA, TopList.SortOrder.Winnings, null);
            var result = Sut.Execute(request);

            Assert.AreEqual(200, result.Items[0].Winnings.Amount);
            Assert.AreEqual(-200, result.Items[1].Winnings.Amount);
        }

        [Test]
        public void TopList_SortByBuyin_HighestBuyinIsFirst()
        {
            var request = new TopList.Request(TestData.UserNameA, TestData.SlugA, TopList.SortOrder.Buyin, null);
            var result = Sut.Execute(request);

            Assert.AreEqual(600, result.Items[0].Buyin.Amount);
            Assert.AreEqual(400, result.Items[1].Buyin.Amount);
        }

        [Test]
        public void TopList_SortByCashout_HighestCashoutIsFirst()
        {
            var request = new TopList.Request(TestData.UserNameA, TestData.SlugA, TopList.SortOrder.Cashout, null);
            var result = Sut.Execute(request);

            Assert.AreEqual(600, result.Items[0].Cashout.Amount);
            Assert.AreEqual(400, result.Items[1].Cashout.Amount);
        }

        [Test]
        public void TopList_SortByTimePlayed_HighestTotalMinutesIsFirst()
        {
            var request = new TopList.Request(TestData.UserNameA, TestData.SlugA, TopList.SortOrder.TimePlayed, null);
            var result = Sut.Execute(request);

            Assert.AreEqual(152, result.Items[0].TimePlayed.Minutes);
            Assert.AreEqual(152, result.Items[1].TimePlayed.Minutes);
        }

        [Test]
        public void TopList_SortByGamesPlayed_HighestGameCountIsFirst()
        {
            var request = new TopList.Request(TestData.UserNameA, TestData.SlugA, TopList.SortOrder.GamesPlayed, null);
            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.Items[0].GamesPlayed);
            Assert.AreEqual(2, result.Items[1].GamesPlayed);
        }

        [Test]
        public void TopList_SortByWinRate_HighestWinRateIsFirst()
        {
            var request = new TopList.Request(TestData.UserNameA, TestData.SlugA, TopList.SortOrder.WinRate, null);
            var result = Sut.Execute(request);

            Assert.AreEqual(79, result.Items[0].WinRate.Amount);
            Assert.AreEqual(-79, result.Items[1].WinRate.Amount);
        }

        private TopList Sut
        {
            get
            {
                return new TopList(
                    Repos.Bunch,
                    Repos.Cashgame,
                    Repos.Player,
                    Repos.User);
            }
        }
    }
}