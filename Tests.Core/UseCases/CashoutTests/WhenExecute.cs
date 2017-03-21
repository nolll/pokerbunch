using NUnit.Framework;

namespace Tests.Core.UseCases.CashoutTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void CallsCashout()
        {
            Assert.AreEqual(CashgameId, PostedCashgameId);
            Assert.AreEqual(PlayerId, PostedPlayerId);
            Assert.AreEqual(CashoutStack, PostedStack);
        }
    }
}
