using System.Web.Mvc;
using Core.Services;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeCommands;
using Web.Commands.AuthCommands;
using Web.Controllers;
using Web.ModelFactories.AuthModelFactories;
using Web.Models.AuthModels;

namespace Web.Tests.ControllerTests{

	public class AuthControllerTests : MockContainer {

		[Test]
        public void ActionLoginPost_LoginSucceededButNoReturnUrl_RedirectsToRoot()
        {
            var command = new FakeSuccessfulCommand();
            GetMock<IAuthCommandProvider>().Setup(o => o.GetLoginCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(command);

		    const string homeUrl = "a";
		    GetMock<IUrlProvider>().Setup(o => o.GetHomeUrl()).Returns(homeUrl);

            var sut = GetSut();

            var loginPageModel = new AuthLoginPageModel();
            var result = sut.Login(loginPageModel) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(homeUrl, result.Url);
		}

        [Test]
        public void ActionLoginPost_LoginSucceededWithReturnUrl_RedirectsToReturnUrl()
        {
            var command = new FakeSuccessfulCommand();
            GetMock<IAuthCommandProvider>().Setup(o => o.GetLoginCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(command);

            var sut = GetSut();

            var loginPageModel = new AuthLoginPageModel();
            loginPageModel.ReturnUrl = "return-url";
            var result = sut.Login(loginPageModel) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("return-url", result.Url);
        }

        [Test]
        public void ActionLoginPost_UserNotFound_ShowsForm(){
            var command = new FakeFailedCommand();
            GetMock<IAuthCommandProvider>().Setup(o => o.GetLoginCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(command);
            
            GetMock<IAuthLoginPageModelFactory>().Setup(o => o.Create()).Returns(new AuthLoginPageModel());

            var sut = GetSut();
            sut.ModelState.AddModelError("fake_error", "");

            var loginPageModel = new AuthLoginPostModel();
			var result = sut.Login(loginPageModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.ViewName);
		}

		[Test]
        public void ActionLogout_RedirectsToHome(){
            var command = new FakeSuccessfulCommand();
            GetMock<IAuthCommandProvider>().Setup(o => o.GetLogoutCommand()).Returns(command);
            
            var sut = GetSut();

            var result = sut.Logout() as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
		}
        
        private AuthController GetSut()
        {
            return new AuthController(
                GetMock<IAuthLoginPageModelFactory>().Object,
                GetMock<IUrlProvider>().Object,
                GetMock<IAuthCommandProvider>().Object);
        }

	}

}