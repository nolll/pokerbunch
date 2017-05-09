using Core.Exceptions;
using NUnit.Framework;

namespace Tests.Core.UseCases.DeleteCashgameTests
{
    public class WhenCashgameHasResults : Arrange
    {
        protected override ExecuteMode ExecuteMode => ExecuteMode.Manual;
        protected override string Id => IdWithResults;

        [Test]
        public void ThrowsCashgameHasResultsException() => Assert.Throws<CashgameHasResultsException>(Execute);
    }
}