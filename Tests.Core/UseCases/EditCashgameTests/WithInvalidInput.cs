using Core.Exceptions;
using NUnit.Framework;

namespace Tests.Core.UseCases.EditCashgameTests
{
    public class WithInvalidInput : Arrange
    {
        [Test]
        public void ThrowsException()
        {
            Assert.Throws<ValidationException>(() => Sut.Execute(InvalidRequest));
        }
    }
}