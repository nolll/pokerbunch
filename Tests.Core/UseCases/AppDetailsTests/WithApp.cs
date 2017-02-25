using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AppDetailsTests
{
    public class WithApp : Arrange
    {
        [Test]
        public void AppDetails_AllDataIsSet()
        {
            Assert.AreEqual(AppData.Id1, Result.AppId);
            Assert.AreEqual(AppData.Key1, Result.AppKey);
            Assert.AreEqual(AppData.Name1, Result.AppName);
        }
    }
}