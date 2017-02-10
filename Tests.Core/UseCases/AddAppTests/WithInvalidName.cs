using Core.Exceptions;
using NUnit.Framework;

namespace Tests.Core.UseCases.AddAppTests
{
    public class WithInvalidName : Arrange
    {
        [Test]
        public void AddApp_AppIsAdded()
        {
            Assert.Throws<ValidationException>(() => Sut.Execute(InvalidRequest));
        }
    }
}