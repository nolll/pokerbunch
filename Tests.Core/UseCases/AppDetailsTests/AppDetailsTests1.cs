using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.AppDetailsTests
{
    public abstract class Arrange
    {
        protected AppDetails Sut;

        [SetUp]
        public void Setup()
        {
            var arm = new Mock<IAppRepository>();

            arm.Setup(s => s.GetById(AppData.Id1)).Returns(AppData.OneApp);

            Sut = new AppDetails(arm.Object);
        }

        protected AppDetails.Request Request => new AppDetails.Request(AppData.Id1);
    }

    public class AppDetailsTests1 : Arrange
    {
        [Test]
        public void AppDetails_AllDataIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(AppData.Id1, result.AppId);
            Assert.AreEqual(AppData.Key1, result.AppKey);
            Assert.AreEqual(AppData.Name1, result.AppName);
        }
    }
}