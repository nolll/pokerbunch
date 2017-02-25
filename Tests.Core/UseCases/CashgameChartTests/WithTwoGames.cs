using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameChartTests
{
    public class WithTwoGames : Arrange
    {
        [Test]
        public void GameDataIsCorrect()
        {
            Assert.AreEqual(2, Result.GameItems.Count);
            Assert.AreEqual("2001-01-01", Result.GameItems[0].Date.IsoString);
            Assert.AreEqual(2, Result.GameItems[0].Winnings.Count);
            Assert.AreEqual(-150, Result.GameItems[0].Winnings[PlayerData.Id2]);
            Assert.AreEqual(150, Result.GameItems[0].Winnings[PlayerData.Id1]);
            Assert.AreEqual(2, Result.GameItems[1].Winnings.Count);
            Assert.AreEqual(300, Result.GameItems[1].Winnings[PlayerData.Id1]);
            Assert.AreEqual(-300, Result.GameItems[1].Winnings[PlayerData.Id2]);
        }

        [Test]
        public void PlayerDataIsCorrect()
        {
            Assert.AreEqual(2, Result.PlayerItems.Count);
            Assert.AreEqual(PlayerData.Id1, Result.PlayerItems[0].Id);
            Assert.AreEqual(PlayerData.Name1, Result.PlayerItems[0].Name);
            Assert.AreEqual(PlayerData.Id2, Result.PlayerItems[1].Id);
            Assert.AreEqual(PlayerData.Name2, Result.PlayerItems[1].Name);
        }
    }
}
