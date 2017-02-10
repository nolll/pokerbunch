using NUnit.Framework;

namespace Tests.Core.UseCases.AddAppTests
{
    public class WithValidName : Arrange
    {
        [Test]
        public void AddApp_AppIsAdded()
        {
            Sut.Execute(ValidRequest);

            Assert.AreEqual(AddedAppName, AddedAppName);
        }
    }
}
