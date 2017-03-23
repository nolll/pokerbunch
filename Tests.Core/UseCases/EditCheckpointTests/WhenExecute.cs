using NUnit.Framework;

namespace Tests.Core.UseCases.EditCheckpointTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void CashgameIdIsSet()
        {
            Assert.AreEqual(CashgameId, Result.CashgameId);
        }

        [Test]
        public void PlayerIdIsSet()
        {
            Assert.AreEqual(PlayerId, Result.PlayerId);
        }

        [Test]
        public void EditCheckpoint_ValidInput_CheckpointIsSaved()
        {
            Assert.AreEqual(ActionId, PostedActionId);
            Assert.AreEqual(Timestamp, PostedTimestamp);
            Assert.AreEqual(Stack, PostedStack);
            Assert.AreEqual(Amount, PostedAmount);
        }
    }
}