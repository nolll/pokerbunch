using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameFactsTests
{
    public class WithTwoGames : Arrange
    {
        [Test]
        public void GetFactsResult_AllPropertiesAreSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(2, result.GameCount);
            Assert.AreEqual(124, result.TotalTimePlayed.Minutes);
            Assert.AreEqual(800, result.Turnover.Amount);
            Assert.AreEqual(-150, result.WorstResult.Amount.Amount);
            Assert.AreEqual(+150, result.BestResult.Amount.Amount);
            Assert.AreEqual(-300, result.WorstTotalResult.Amount.Amount);
            Assert.AreEqual(300, result.BestTotalResult.Amount.Amount);
            Assert.AreEqual(400, result.BiggestBuyin.Amount.Amount);
            Assert.AreEqual(700, result.BiggestCashout.Amount.Amount);
            Assert.AreEqual(124, result.MostTimePlayed.Time.Minutes);
        }
    }
}
