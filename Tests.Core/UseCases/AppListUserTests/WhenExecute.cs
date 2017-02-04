using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AppListUserTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void ReturnsList()
        {
            var result = Sut.Execute();

            Assert.AreEqual(2, result.Items.Count);
            Assert.AreEqual(AppData.Name1, result.Items[0].AppName);
            Assert.AreEqual(AppData.Name2, result.Items[1].AppName);
        }
    }
}
