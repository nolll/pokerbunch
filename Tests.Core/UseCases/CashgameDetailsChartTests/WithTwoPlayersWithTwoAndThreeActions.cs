using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameDetailsChartTests
{
    public class WithTwoPlayersWithTwoAndThreeActions : Arrange
    {
        [Test]
        public void ResultsAreCorrect()
        {
            Assert.AreEqual(2, Result.PlayerItems.Count);
            Assert.AreEqual(PlayerData.Name1, Result.PlayerItems[0].Name);
            Assert.AreEqual(2, Result.PlayerItems[0].Results.Count);
            Assert.AreEqual(0, Result.PlayerItems[0].Results[0].Winnings);
            Assert.AreEqual(-150, Result.PlayerItems[0].Results[1].Winnings);
            Assert.AreEqual(3, Result.PlayerItems[1].Results.Count);
            Assert.AreEqual(0, Result.PlayerItems[1].Results[0].Winnings);
            Assert.AreEqual(50, Result.PlayerItems[1].Results[1].Winnings);
            Assert.AreEqual(150, Result.PlayerItems[1].Results[2].Winnings);
        }
    }
}