using Application.Services;
using Application.UseCases.ApplicationContext;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.AuthModelFactories;
using Web.Models.AuthModels;
using Web.Models.UrlModels;

namespace Tests.Web.ModelFactoryTests.AuthModelFactories
{

    public class AuthLoginPageModelFactoryTests : MockContainer
    {
        [Test]
        public void ReturnUrl_NoReturnUrl_IsSetToRoot()
        {
            var homeUrl = new HomeUrlModel();
            GetMock<IApplicationContextInteractor>().Setup(o => o.Execute()).Returns(new ApplicationContextResultInTest());

            var sut = GetSut();

            var result = sut.Create(null);

            Assert.AreEqual(homeUrl.Relative, result.ReturnUrl);
        }

        [Test]
        public void ReturnUrl_WithReturnUrl_IsSet()
        {
            GetMock<IWebContext>().Setup(o => o.GetQueryParam("return")).Returns("return-url");
            GetMock<IApplicationContextInteractor>().Setup(o => o.Execute()).Returns(new ApplicationContextResultInTest());

            var sut = GetSut();
            var result = sut.Create(null);

            Assert.AreEqual("return-url", result.ReturnUrl);
        }

        [Test]
        public void AddUserUrl_IsSet()
        {
            GetMock<IApplicationContextInteractor>().Setup(o => o.Execute()).Returns(new ApplicationContextResultInTest());

            var sut = GetSut();
            var result = sut.Create(null);

            Assert.IsInstanceOf<AddUserUrlModel>(result.AddUserUrl);
        }

        [Test]
        public void ForgotPasswordUrl_IsSet()
        {
            GetMock<IApplicationContextInteractor>().Setup(o => o.Execute()).Returns(new ApplicationContextResultInTest());

            var sut = GetSut();
            var result = sut.Create(null);

            Assert.IsInstanceOf<ForgotPasswordUrlModel>(result.ForgotPasswordUrl);
        }

        [Test]
        public void LoginName_IsSet()
        {
            var postModel = new AuthLoginPostModel { LoginName = "login-name" };

            GetMock<IApplicationContextInteractor>().Setup(o => o.Execute()).Returns(new ApplicationContextResultInTest());

            var sut = GetSut();
            var result = sut.Create(postModel);

            Assert.AreEqual(result.LoginName, "login-name");
        }

        private AuthLoginPageModelFactory GetSut()
        {
            return new AuthLoginPageModelFactory(
                GetMock<IWebContext>().Object,
                GetMock<IApplicationContextInteractor>().Object);
        }

    }

}