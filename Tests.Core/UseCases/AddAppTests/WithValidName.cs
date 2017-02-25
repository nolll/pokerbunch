using NUnit.Framework;

namespace Tests.Core.UseCases.AddAppTests
{
    public class WithValidName : Arrange
    {
        protected override string AppName => ValidAppName;

        [Test]
        public void AppIsAdded()
        {
            Execute();
            Assert.AreEqual(AddedAppName, AddedAppName);
        }
    }
}
