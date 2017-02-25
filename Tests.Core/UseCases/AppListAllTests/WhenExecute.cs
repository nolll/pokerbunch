using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AppListAllTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void ReturnsList()
        {
            Assert.AreEqual(2, Result.Items.Count);
            Assert.AreEqual(AppData.Name1, Result.Items[0].AppName);
            Assert.AreEqual(AppData.Name2, Result.Items[1].AppName);
        }
    }
}
