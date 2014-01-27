using System.Web.Mvc;
using Application.Services.Interfaces;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeCommands;
using Web.Commands.AuthCommands;
using Web.Controllers;
using Web.ModelServices;
using Web.Models.AuthModels;

namespace Tests.Web.ControllerTests{

	public class AuthControllerTests : MockContainer
    {
	    [Test]
	    public void Login_ReturnsCorrectViewAndModel()
	    {
	        var model = new AuthLoginPageModel();
	        GetMock<IAuthModelService>().Setup(o => o.GetLoginModel(null)).Returns(model);

	        var sut = GetSut();
	        var result = sut.Login() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(model, result.Model);
            Assert.AreEqual("Login", result.ViewName);
	    }

		[Test]
        public void LoginPost_LoginSucceededButNoReturnUrl_RedirectsToRoot()
        {
            var command = new FakeSuccessfulCommand();
            GetMock<IAuthCommandProvider>().Setup(o => o.GetLoginCommand(It.IsAny<AuthLoginPostModel>())).Returns(command);

		    const string homeUrl = "a";
		    GetMock<IUrlProvider>().Setup(o => o.GetHomeUrl()).Returns(homeUrl);

            var sut = GetSut();

            var loginPageModel = new AuthLoginPageModel();
            var result = sut.Login(loginPageModel) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(homeUrl, result.Url);
		}

        [Test]
        public void LoginPost_LoginSucceededWithReturnUrl_RedirectsToReturnUrl()
        {
            var command = new FakeSuccessfulCommand();
            GetMock<IAuthCommandProvider>().Setup(o => o.GetLoginCommand(It.IsAny<AuthLoginPostModel>())).Returns(command);

            var sut = GetSut();

            var loginPageModel = new AuthLoginPageModel {ReturnUrl = "return-url"};
            var result = sut.Login(loginPageModel) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("return-url", result.Url);
        }

        [Test]
        public void LoginPost_UserNotFound_ShowsForm(){
            var command = new FakeFailedCommand();
            GetMock<IAuthCommandProvider>().Setup(o => o.GetLoginCommand(It.IsAny<AuthLoginPostModel>())).Returns(command);

            var loginPageModel = new AuthLoginPostModel();
			GetMock<IAuthModelService>().Setup(o => o.GetLoginModel(loginPageModel)).Returns(new AuthLoginPageModel());

            var sut = GetSut();
            var result = sut.Login(loginPageModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.ViewName);
		}

		[Test]
        public void Logout_RedirectsToHome()
		{
		    const string homeUrl = "a";

            var command = new FakeSuccessfulCommand();
            GetMock<IAuthCommandProvider>().Setup(o => o.GetLogoutCommand()).Returns(command);
		    GetMock<IUrlProvider>().Setup(o => o.GetHomeUrl()).Returns(homeUrl);
            
            var sut = GetSut();

            var result = sut.Logout() as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(homeUrl, result.Url);
		}
        
        private AuthController GetSut()
        {
            return new AuthController(
                GetMock<IUrlProvider>().Object,
                GetMock<IAuthCommandProvider>().Object,
                GetMock<IAuthModelService>().Object);
        }

	}

}