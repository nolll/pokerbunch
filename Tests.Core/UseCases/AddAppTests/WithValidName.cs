using NUnit.Framework;

namespace Tests.Core.UseCases.AddAppTests
{
    public class WithValidName : Arrange
    {
        protected override string AppName => ValidAppName;

        [Test]
        public void AppIsAdded() => Assert.AreEqual(AddedAppName, AddedAppName);
    }
}
