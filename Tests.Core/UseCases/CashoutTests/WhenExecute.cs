using NUnit.Framework;

namespace Tests.Core.UseCases.CashoutTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void AddsCheckpoint()
        {
            Assert.AreEqual(CashgameId, PostedCashgameId);
            Assert.AreEqual(PlayerId, PostedPlayerId);
            Assert.AreEqual(CashoutStack, PostedStack);
        }
    }
}
