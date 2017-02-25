using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameFactsTests
{
    public class WithTwoGames : Arrange
    {
        [Test]
        public void AllPropertiesAreSet()
        {
            Assert.AreEqual(2, Result.GameCount);
            Assert.AreEqual(124, Result.TotalTimePlayed.Minutes);
            Assert.AreEqual(800, Result.Turnover.Amount);
            Assert.AreEqual(-150, Result.WorstResult.Amount.Amount);
            Assert.AreEqual(+150, Result.BestResult.Amount.Amount);
            Assert.AreEqual(-300, Result.WorstTotalResult.Amount.Amount);
            Assert.AreEqual(300, Result.BestTotalResult.Amount.Amount);
            Assert.AreEqual(400, Result.BiggestBuyin.Amount.Amount);
            Assert.AreEqual(700, Result.BiggestCashout.Amount.Amount);
            Assert.AreEqual(124, Result.MostTimePlayed.Time.Minutes);
        }
    }
}
