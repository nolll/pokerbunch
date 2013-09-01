using System.Web.Mvc;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.Controllers;
using Web.Models.AuthModels;
using Web.Tests.Fakes;
using Web.Validators;

namespace Web.Tests.ControllerTests{

	public class AuthControllerTests : MockContainer {

		[Test]
        public void ActionLoginPost_UserExistsButNoReturnUrl_RedirectsToRoot(){
			var user = new User();
		    UserStorageMock.Setup(o => o.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(user);
            SetupValidValidator();

            var sut = GetSut();

            var loginPageModel = new AuthLoginPageModel();
            var result = sut.Login(loginPageModel) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("/", result.Url);
		}

        [Test]
        public void ActionLoginPost_UserExistsAndWithReturnUrl_RedirectsToReturnUrl(){
			var user = new User();
            UserStorageMock.Setup(o => o.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(user);
			SetupValidValidator();

            var sut = GetSut();

            var loginPageModel = new AuthLoginPageModel();
            loginPageModel.ReturnUrl = "return-url";
            var result = sut.Login(loginPageModel) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("return-url", result.Url);
		}

        [Test]
        public void ActionLoginPost_UserNotFound_ShowsForm(){
			SetupInvalidValidator();
            AuthLoginPageModelFactoryMock.Setup(o => o.Create(It.IsAny<string>(), It.IsAny<string>())).Returns(new AuthLoginPageModel());

            var sut = GetSut();

            var loginPageModel = new AuthLoginPageModel();
			var result = sut.Login(loginPageModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<AuthLoginPageModel>(result.Model);
            Assert.AreEqual("Login", result.ViewName);
		}

		[Test]
        public void ActionLoginPost_WithUserNameAndPassword_SetsSessionCookie()
		{
		    const string cookieName = "token";
		    const string tokenName = "a";
			var user = new User();
            UserStorageMock.Setup(o => o.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(user);
		    UserStorageMock.Setup(o => o.GetToken(It.IsAny<User>())).Returns(tokenName);
			SetupValidValidator();

            var sut = GetSut();
			sut.Login(new AuthLoginPageModel());

            WebContextMock.Verify(o => o.SetSessionCookie(cookieName, tokenName));
		}

        [Test]
        public void ActionLoginPost_UserFoundAndRememberChecked_SetsPersistentCookie(){
            const string cookieName = "token";
            const string tokenName = "a";
            var user = new User();
            UserStorageMock.Setup(o => o.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(user);
            UserStorageMock.Setup(o => o.GetToken(It.IsAny<User>())).Returns(tokenName);
            SetupValidValidator();

            var sut = GetSut();
            sut.Login(new AuthLoginPageModel{RememberMe = true});

            WebContextMock.Verify(o => o.SetPersistentCookie(cookieName, tokenName));
		}

		[Test]
        public void ActionLogout_ClearsCookies(){
            const string cookieName = "token";
            
            var sut = GetSut();

            var result = sut.Logout() as RedirectToRouteResult;

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
        
        private void SetupValidValidator(){
			var validator = new ValidatorFake();
			SetupValidator(validator);
		}

        private void SetupInvalidValidator(){
			var validator = new ValidatorFake {IsValid = false};
            SetupValidator(validator);
		}

        private void SetupValidator(Validator validator){
			UserValidatorFactoryMock.Setup(o => o.GetLoginValidator(It.IsAny<User>())).Returns(validator);
		}

        private AuthController GetSut()
        {
            return new AuthController(UserStorageMock.Object, EncryptionServiceMock.Object, UserValidatorFactoryMock.Object, WebContextMock.Object, AuthLoginPageModelFactoryMock.Object);
        }

	}

}