using Application.Urls;
using Application.UseCases.BunchContext;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.HomeModelFactories;
using Web.Models.UrlModels;

namespace Tests.Web.ModelFactoryTests.HomeModelFactories
{
    public class HomePageModelFactoryTests : MockContainer
    {
        [Test]
        public void AllProperties_DefaultState_IsFalse()
        {
            GetMock<IBunchContextInteractor>().Setup(o => o.Execute(It.IsAny<BunchContextRequest>())).Returns(new BunchContextResultInTest());

            var sut = GetSut();
            var result = sut.Build();

            Assert.IsFalse(result.IsLoggedIn);
            Assert.IsInstanceOf<AddHomegameUrl>(result.AddHomegameUrl);
            Assert.IsInstanceOf<LoginUrl>(result.LoginUrl);
            Assert.IsInstanceOf<AddUserUrl>(result.RegisterUrl);
        }

        [Test]
        public void IsLoggedIn_WithUser_IsTrue()
        {
            var bunchContextResult = new BunchContextResultInTest(isLoggedIn: true);
            GetMock<IBunchContextInteractor>().Setup(o => o.Execute(It.IsAny<BunchContextRequest>())).Returns(bunchContextResult);

            var sut = GetSut();
            var result = sut.Build();

            Assert.IsTrue(result.IsLoggedIn);
        }

        private HomePageBuilder GetSut()
        {
            return new HomePageBuilder(
                GetMock<IBunchContextInteractor>().Object);
        }
    }
}
