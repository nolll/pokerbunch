using NUnit.Framework;

namespace Tests.Core.UseCases.ReportTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void CallsReport()
        {
            Assert.AreEqual(CashgameId, PostedCashgameId);
            Assert.AreEqual(PlayerId, PostedPlayerId);
            Assert.AreEqual(Stack, PostedStack);
        }
    }
}