using System.Web.Mvc;
using Application.Urls;
using Application.UseCases.AppContext;
using Application.UseCases.LoginForm;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeCommands;
using Web.Commands.AuthCommands;
using Web.Controllers;
using Web.Models.AuthModels;

namespace Tests.Web.ControllerTests
{
    public class AuthControllerTests : MockContainer
    {
        [Test]
        public void LoginPost_LoginSucceededButNoReturnUrl_RedirectsToRoot()
        {
            var command = new SuccessfulCommandInTest();
            GetMock<IAuthCommandProvider>().Setup(o => o.GetLoginCommand(It.IsAny<LoginPostModel>())).Returns(command);

            var homeUrl = new HomeUrl();

            var sut = GetSut();

            var loginPageModel = new LoginPostModel();
            var result = sut.Login(loginPageModel) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(homeUrl.Relative, result.Url);
        }

        [Test]
        public void LoginPost_LoginSucceededWithReturnUrl_RedirectsToReturnUrl()
        {
            var command = new SuccessfulCommandInTest();
            GetMock<IAuthCommandProvider>().Setup(o => o.GetLoginCommand(It.IsAny<LoginPostModel>())).Returns(command);

            var sut = GetSut();

            var loginPageModel = new LoginPostModel { ReturnUrl = "return-url" };
            var result = sut.Login(loginPageModel) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("return-url", result.Url);
        }

        [Test]
        public void Logout_RedirectsToHome()
        {
            var homeUrl = new HomeUrl();

            var command = new SuccessfulCommandInTest();
            GetMock<IAuthCommandProvider>().Setup(o => o.GetLogoutCommand()).Returns(command);

            var sut = GetSut();

            var result = sut.Logout() as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(homeUrl.Relative, result.Url);
        }

        private AuthController GetSut()
        {
            return new AuthController(
                GetMock<IAppContextInteractor>().Object,
                GetMock<ILoginFormInteractor>().Object,
                GetMock<IAuthCommandProvider>().Object);
        }
    }
}