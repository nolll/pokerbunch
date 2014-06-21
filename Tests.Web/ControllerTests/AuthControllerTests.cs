using System.Web.Mvc;
using Application.Urls;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeCommands;
using Tests.Common.FakeModels;
using Web.Commands.AuthCommands;
using Web.Controllers;
using Web.ModelFactories.AuthModelFactories;
using Web.Models.AuthModels;

namespace Tests.Web.ControllerTests
{
    public class AuthControllerTests : MockContainer
    {
        [Test]
        public void Login_ReturnsCorrectViewAndModel()
        {
            var model = new LoginPageModelInTest();
            GetMock<ILoginPageBuilder>().Setup(o => o.Build(null)).Returns(model);

            var sut = GetSut();
            var result = sut.Login() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(model, result.Model);
            Assert.AreEqual("Login", result.ViewName);
        }

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
        public void LoginPost_UserNotFound_ShowsForm()
        {
            var command = new FailedCommandInTest();
            GetMock<IAuthCommandProvider>().Setup(o => o.GetLoginCommand(It.IsAny<LoginPostModel>())).Returns(command);

            var loginPostModel = new LoginPostModel();
            GetMock<ILoginPageBuilder>().Setup(o => o.Build(loginPostModel)).Returns(new LoginPageModelInTest());

            var sut = GetSut();
            var result = sut.Login(loginPostModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.ViewName);
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
                GetMock<ILoginPageBuilder>().Object,
                GetMock<IAuthCommandProvider>().Object);
        }
    }
}