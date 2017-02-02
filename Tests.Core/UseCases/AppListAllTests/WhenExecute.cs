using NUnit.Framework;

namespace Tests.Core.UseCases.AppListAllTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void ReturnsList()
        {
            var result = Sut.Execute();

            Assert.AreEqual(2, result.Items.Count);
            Assert.AreEqual("name-1", result.Items[0].AppName);
            Assert.AreEqual("name-2", result.Items[1].AppName);
        }
    }
}
