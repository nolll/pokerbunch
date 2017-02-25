using Core.Exceptions;
using NUnit.Framework;

namespace Tests.Core.UseCases.AddAppTests
{
    public class WithInvalidName : Arrange
    {
        protected override bool ExecuteAutomatically => false;
        protected override string AppName => InvalidAppName;

        [Test]
        public void AppIsAdded() => Assert.Throws<ValidationException>(Execute);
    }
}