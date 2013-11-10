using System.Web.Mvc;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Controllers;
using Web.Models.AuthModels;

namespace Web.Tests.ControllerTests{

	public class AuthControllerTests : WebMockContainer {

		[Test]
        public void ActionLoginPost_UserExistsButNoReturnUrl_RedirectsToRoot(){
			var user = new FakeUser();
            Mocks.UserRepositoryMock.Setup(o => o.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

		    const string homeUrl = "a";
		    Mocks.UrlProviderMock.Setup(o => o.GetHomeUrl()).Returns(homeUrl);

            var sut = GetSut();

            var loginPageModel = new AuthLoginPageModel();
            var result = sut.Login(loginPageModel) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(homeUrl, result.Url);
		}

        [Test]
        public void ActionLoginPost_UserExistsAndWithReturnUrl_RedirectsToReturnUrl(){
			var user = new FakeUser();
            Mocks.UserRepositoryMock.Setup(o => o.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

            var sut = GetSut();

            var loginPageModel = new AuthLoginPageModel();
            loginPageModel.ReturnUrl = "return-url";
            var result = sut.Login(loginPageModel) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("return-url", result.Url);
		}

        [Test]
        public void ActionLoginPost_UserNotFound_ShowsForm(){
            Mocks.AuthLoginPageModelFactoryMock.Setup(o => o.Create()).Returns(new AuthLoginPageModel());

            var sut = GetSut();
            sut.ModelState.AddModelError("fake_error", "");

            var loginPageModel = new AuthLoginPostModel();
			var result = sut.Login(loginPageModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.ViewName);
		}

		[Test]
        public void ActionLoginPost_WithUserNameAndPassword_SetsSessionCookie()
		{
		    const string cookieName = "token";
		    const string tokenName = "a";
			var user = new FakeUser();
            Mocks.UserRepositoryMock.Setup(o => o.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(user);
            Mocks.UserRepositoryMock.Setup(o => o.GetToken(It.IsAny<User>())).Returns(tokenName);
            Mocks.UrlProviderMock.Setup(o => o.GetHomeUrl()).Returns("any");

            var sut = GetSut();
			sut.Login(new AuthLoginPostModel());

            Mocks.WebContextMock.Verify(o => o.SetSessionCookie(cookieName, tokenName));
		}

        [Test]
        public void ActionLoginPost_UserFoundAndRememberChecked_SetsPersistentCookie(){
            const string cookieName = "token";
            const string tokenName = "a";
            var user = new FakeUser();
            Mocks.UserRepositoryMock.Setup(o => o.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(user);
            Mocks.UserRepositoryMock.Setup(o => o.GetToken(It.IsAny<User>())).Returns(tokenName);
            Mocks.UrlProviderMock.Setup(o => o.GetHomeUrl()).Returns("any");

            var sut = GetSut();
            sut.Login(new AuthLoginPostModel{RememberMe = true});

            Mocks.WebContextMock.Verify(o => o.SetPersistentCookie(cookieName, tokenName));
		}

		[Test]
        public void ActionLogout_ClearsCookies(){
            const string cookieName = "token";
            
            var sut = GetSut();

            sut.Logout();

            Mocks.WebContextMock.Verify(o => o.ClearCookie(cookieName));
		}

		[Test]
        public void ActionLogout_RedirectsToHome(){
		    var sut = GetSut();

            var result = sut.Logout() as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
		}
        
        private AuthController GetSut()
        {
            return new AuthController(
                Mocks.UserRepositoryMock.Object,
                Mocks.EncryptionServiceMock.Object,
                Mocks.WebContextMock.Object,
                Mocks.AuthLoginPageModelFactoryMock.Object,
                Mocks.UrlProviderMock.Object);
        }

	}

}