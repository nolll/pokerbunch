using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameChartTests
{
    public class WithTwoGames : Arrange
    {
        [Test]
        public void GameDataIsCorrect()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(2, result.GameItems.Count);

            Assert.AreEqual("2001-01-01", result.GameItems[0].Date.IsoString);
            Assert.AreEqual(2, result.GameItems[0].Winnings.Count);
            Assert.AreEqual(-150, result.GameItems[0].Winnings[PlayerData.Id2]);
            Assert.AreEqual(150, result.GameItems[0].Winnings[PlayerData.Id1]);
            Assert.AreEqual(2, result.GameItems[1].Winnings.Count);
            Assert.AreEqual(300, result.GameItems[1].Winnings[PlayerData.Id1]);
            Assert.AreEqual(-300, result.GameItems[1].Winnings[PlayerData.Id2]);
        }

        [Test]
        public void PlayerDataIsCorrect()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(2, result.PlayerItems.Count);
            Assert.AreEqual(PlayerData.Id1, result.PlayerItems[0].Id);
            Assert.AreEqual(PlayerData.Name1, result.PlayerItems[0].Name);
            Assert.AreEqual(PlayerData.Id2, result.PlayerItems[1].Id);
            Assert.AreEqual(PlayerData.Name2, result.PlayerItems[1].Name);
        }
    }
}
