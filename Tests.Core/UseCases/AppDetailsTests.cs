using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class AppDetailsTests : TestBase
    {
        [Test]
        public void AppDetails_AllDataIsSet()
        {
            var request = new AppDetails.Request(TestData.AppA.Id);
            var result = Sut.Execute(request);

            Assert.AreEqual(TestData.AppA.Id, result.AppId);
            Assert.AreEqual(TestData.AppA.AppKey, result.AppKey);
            Assert.AreEqual(TestData.AppA.Name, result.AppName);
        }

        private AppDetails Sut => new AppDetails(Repos.App);
    }
}