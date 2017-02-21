using NUnit.Framework;

namespace Tests.Core.UseCases.PlayerFactsTests
{
    public class WithGames : Arrange
    {
        [Test]
        public void PlayerFacts_ReturnsResultObject()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(2, result.GamesPlayed);
            Assert.AreEqual(184, result.TimePlayed.Minutes);
            Assert.AreEqual(300, result.Winnings.Amount);
            Assert.AreEqual(200, result.BestResult.Amount);
            Assert.AreEqual(100, result.WorstResult.Amount);
            Assert.AreEqual(2, result.BestResultCount);
            Assert.AreEqual(2, result.CurrentStreak);
            Assert.AreEqual(2, result.WinningStreak);
            Assert.AreEqual(0, result.LosingStreak);
        }
    }
}