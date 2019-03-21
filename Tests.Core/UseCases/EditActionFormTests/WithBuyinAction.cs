using NUnit.Framework;

namespace Tests.Core.UseCases.EditActionFormTests
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