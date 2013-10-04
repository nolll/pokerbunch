using System.Web.Mvc;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.Controllers;
using Web.Models.AuthModels;

namespace Web.Tests.ControllerTests{

	public class AuthControllerTests : MockContainer {

		[Test]
        public void ActionLoginPost_UserExistsButNoReturnUrl_RedirectsToRoot(){
			var user = new User();
            UserRepositoryMock.Setup(o => o.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

            var sut = GetSut();

            var loginPageModel = new AuthLoginPageModel();
            var result = sut.Login(loginPageModel) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("/", result.Url);
		}

        [Test]
        public void ActionLoginPost_UserExistsAndWithReturnUrl_RedirectsToReturnUrl(){
			var user = new User();
            UserRepositoryMock.Setup(o => o.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

            var sut = GetSut();

            var loginPageModel = new AuthLoginPageModel();
            loginPageModel.ReturnUrl = "return-url";
            var result = sut.Login(loginPageModel) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("return-url", result.Url);
		}

        [Test]
        public void ActionLoginPost_UserNotFound_ShowsForm(){
            AuthLoginPageModelFactoryMock.Setup(o => o.Create()).Returns(new AuthLoginPageModel());

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
			var user = new User();
            UserRepositoryMock.Setup(o => o.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(user);
            UserRepositoryMock.Setup(o => o.GetToken(It.IsAny<User>())).Returns(tokenName);

            var sut = GetSut();
			sut.Login(new AuthLoginPageModel());

            WebContextMock.Verify(o => o.SetSessionCookie(cookieName, tokenName));
		}

        [Test]
        public void ActionLoginPost_UserFoundAndRememberChecked_SetsPersistentCookie(){
            const string cookieName = "token";
            const string tokenName = "a";
            var user = new User();
            UserRepositoryMock.Setup(o => o.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(user);
            UserRepositoryMock.Setup(o => o.GetToken(It.IsAny<User>())).Returns(tokenName);

            var sut = GetSut();
            sut.Login(new AuthLoginPageModel{RememberMe = true});

            WebContextMock.Verify(o => o.SetPersistentCookie(cookieName, tokenName));
		}

		[Test]
        public void ActionLogout_ClearsCookies(){
            const string cookieName = "token";
            
            var sut = GetSut();

            sut.Logout();

            WebContextMock.Verify(o => o.ClearCookie(cookieName));
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
                UserRepositoryMock.Object, 
                EncryptionServiceMock.Object, 
                WebContextMock.Object, 
                AuthLoginPageModelFactoryMock.Object);
        }

	}

}