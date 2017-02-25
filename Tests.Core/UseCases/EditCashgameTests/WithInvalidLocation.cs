using Core.Exceptions;
using NUnit.Framework;

namespace Tests.Core.UseCases.EditCashgameTests
{
    public class WithInvalidLocation : Arrange
    {
        protected override string LocationId => InvalidLocationId;

        [Test]
        public void ThrowsException()
        {
            Assert.Throws<ValidationException>(() => Execute());
        }
    }
}