using NUnit.Framework;

namespace Tests.Core.UseCases.EditCheckpointFormTests
{
    public class WithBuyinAction : Arrange
    {
        protected override string ActionId => BuyinActionId;

        [Test]
        public void CanEditAmountIsTrue()
        {
            Assert.IsTrue(Result.CanEditAmount);
        }
    }
}