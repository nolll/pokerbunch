using NUnit.Framework;

namespace Tests.Core.UseCases.EndCashgameTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void CallsEnd()
        {
            Assert.AreEqual(CashgameId, PostedCashgameId);
        }
    }
}