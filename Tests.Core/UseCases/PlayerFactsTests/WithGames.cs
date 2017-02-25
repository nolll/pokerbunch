using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerFactsTests
{
    public class WithGames : Arrange
    {
        [Test]
        public void PlayerFacts_ReturnsResultObject()
        {
            Assert.AreEqual(2, Result.GamesPlayed);
            Assert.AreEqual(184, Result.TimePlayed.Minutes);
            Assert.AreEqual(300, Result.Winnings.Amount);
            Assert.AreEqual(200, Result.BestResult.Amount);
            Assert.AreEqual(100, Result.WorstResult.Amount);
            Assert.AreEqual(2, Result.BestResultCount);
            Assert.AreEqual(2, Result.CurrentStreak);
            Assert.AreEqual(2, Result.WinningStreak);
            Assert.AreEqual(0, Result.LosingStreak);
        }
    }
}