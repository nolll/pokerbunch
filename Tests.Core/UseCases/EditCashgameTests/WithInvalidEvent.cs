using Core.Exceptions;
using NUnit.Framework;

namespace Tests.Core.UseCases.EditCashgameTests
{
    public class WithInvalidEvent : Arrange
    {
        protected override ExecuteMode ExecuteMode => ExecuteMode.Manual;
        protected override string EventId => InvalidEventId;

        [Test]
        public void ThrowsException()
        {
            Assert.Throws<ValidationException>(Execute);
        }
    }
}