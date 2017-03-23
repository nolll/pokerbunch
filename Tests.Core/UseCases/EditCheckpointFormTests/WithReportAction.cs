using NUnit.Framework;

namespace Tests.Core.UseCases.EditCheckpointFormTests
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