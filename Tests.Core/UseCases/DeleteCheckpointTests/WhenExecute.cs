using NUnit.Framework;

namespace Tests.Core.UseCases.DeleteCheckpointTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void DeletesCheckpointAndReturnsCorrectValues()
        {
            Assert.AreEqual(CashgameId, PostedCashgameId);
            Assert.AreEqual(ActionId, PostedActionId);
            Assert.IsFalse(Result.GameIsRunning);
        }
    }
}