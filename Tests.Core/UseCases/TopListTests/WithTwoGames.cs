using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.TopListTests
{
    public class WithTwoGames : Arrange
    {
        [Test]
        public void TopList_ReturnsTopListItems()
        {
            Assert.AreEqual(2, Result.Items.Count);
            Assert.AreEqual(null, Result.Year);
            Assert.AreEqual(BunchData.Id1, Result.Slug);
        }

        [Test]
        public void TopList_ItemHasCorrectValues()
        {
            Assert.AreEqual(1, Result.Items[0].Rank);
            Assert.AreEqual(400, Result.Items[0].Buyin.Amount);
            Assert.AreEqual(700, Result.Items[0].Cashout.Amount);
            Assert.AreEqual(2, Result.Items[0].GamesPlayed);
            Assert.AreEqual(124, Result.Items[0].TimePlayed.Minutes);
            Assert.AreEqual(PlayerData.Name1, Result.Items[0].Name);
            Assert.AreEqual(PlayerData.Id1, Result.Items[0].PlayerId);
            Assert.AreEqual(300, Result.Items[0].Winnings.Amount);
            Assert.AreEqual(145, Result.Items[0].WinRate.Amount);
        }

        [Test]
        public void TopList_HighestWinningsIsFirst()
        {
            Assert.AreEqual(300, Result.Items[0].Winnings.Amount);
            Assert.AreEqual(-300, Result.Items[1].Winnings.Amount);
        }
    }
}