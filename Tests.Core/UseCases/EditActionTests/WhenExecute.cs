using NUnit.Framework;

namespace Tests.Core.UseCases.EditActionTests
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
        public void EditAction_ValidInput_ActionIsSaved()
        {
            Assert.AreEqual(CashgameId, PostedCashgameId);
            Assert.AreEqual(ActionId, PostedActionId);
            Assert.AreEqual(Timestamp, PostedTimestamp);
            Assert.AreEqual(Stack, PostedStack);
            Assert.AreEqual(Amount, PostedAmount);
        }
    }
}