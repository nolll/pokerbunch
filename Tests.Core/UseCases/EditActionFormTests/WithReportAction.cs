using NUnit.Framework;

namespace Tests.Core.UseCases.EditActionFormTests
{
    public class WithReportAction : Arrange
    {
        protected override string ActionId => ReportActionId;

        [Test]
        public void CanEditAmountIsFalse()
        {
            Assert.IsFalse(Result.CanEditAmount);
        }
    }
}