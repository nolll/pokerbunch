using NUnit.Framework;

namespace Tests.Core.UseCases.EditActionFormTests
{
    public class WhenExecute : Arrange
    {
        protected override string ActionId => BuyinActionId;

        [Test]
        public void StackAndAmountAndTimestampIsSet()
        {
            Assert.AreEqual(Stack, Result.Stack);
            Assert.AreEqual(Added, Result.Amount);
            Assert.AreEqual(BuyinTime, Result.TimeStamp);
        }

        [Test]
        public void CheckpointIdIsSet()
        {
            Assert.AreEqual(BuyinActionId, Result.ActionId);
        }

        [Test]
        public void CashgameIdAndPlayerIdIsSet()
        {
            Assert.AreEqual(CashgameId, Result.CashgameId);
            Assert.AreEqual(PlayerId, Result.PlayerId);
        }
    }
}
