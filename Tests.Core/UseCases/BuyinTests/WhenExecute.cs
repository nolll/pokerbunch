using NUnit.Framework;

namespace Tests.Core.UseCases.BuyinTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void Buyin_StartedCashgame_AddsCheckpointWithCorrectValues()
        {
            Assert.AreEqual(CashgameId, PostedCashgameId);
            Assert.AreEqual(PlayerId, PostedPlayerId);
            Assert.AreEqual(AddedAmount, PostedAmount);
            Assert.AreEqual(Stack, PostedStack);
        }
    }
}
