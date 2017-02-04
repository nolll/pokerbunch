using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameDetailsChartTests
{
    public class WithTwoPlayersWithTwoAndThreeActions : Arrange
    {
        [Test]
        public void ResultsAreCorrect()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(2, result.PlayerItems.Count);
            Assert.AreEqual(PlayerData.Name1, result.PlayerItems[0].Name);
            Assert.AreEqual(2, result.PlayerItems[0].Results.Count);
            Assert.AreEqual(0, result.PlayerItems[0].Results[0].Winnings);
            Assert.AreEqual(-150, result.PlayerItems[0].Results[1].Winnings);
            Assert.AreEqual(3, result.PlayerItems[1].Results.Count);
            Assert.AreEqual(0, result.PlayerItems[1].Results[0].Winnings);
            Assert.AreEqual(50, result.PlayerItems[1].Results[1].Winnings);
            Assert.AreEqual(150, result.PlayerItems[1].Results[2].Winnings);
        }
    }
}