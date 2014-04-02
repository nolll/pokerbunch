using System.Linq;
using Application.Services;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.AuthCommands;
using Web.Models.AuthModels;
using Web.Services;

namespace Tests.Web.CommandTests.AuthCommands{

	public class LoginCommandTests : MockContainer
	{
        private string _loginName;
	    private string _password;
	    private bool _rememberMe;

        [SetUp]
        public void SetUp()
        {
            _loginName = "";
            _password = "";
            _rememberMe = false;
        }

        [Test]
        public void Execute_PasswordsMatch_ReturnsTrue()
        {
            _password = "a";
            const string userPassword = "a";
			var user = new FakeUser(encryptedPassword: userPassword);
            GetMock<IUserRepository>().Setup(o => o.GetByNameOrEmail(It.IsAny<string>())).Returns(user);
            GetMock<IEncryptionService>().Setup(o => o.Encrypt(It.IsAny<string>(), It.IsAny<string>())).Returns(_password);

            var sut = GetSut();
            var result = sut.Execute();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.Errors.Count());
		}
        
        [Test]
        public void Execute_PasswordsMismatch_ReturnsFalse()
        {
            _password = "a";
            const string userPassword = "b";
            var user = new FakeUser(encryptedPassword: userPassword);
            GetMock<IUserRepository>().Setup(o => o.GetByNameOrEmail(It.IsAny<string>())).Returns(user);
            GetMock<IEncryptionService>().Setup(o => o.Encrypt(It.IsAny<string>(), It.IsAny<string>())).Returns(_password);

            var sut = GetSut();
            var result = sut.Execute();

            Assert.IsFalse(result);
            Assert.AreEqual(1, sut.Errors.Count());
		}
        
        [Test]
        public void Execute_UserFound_SetsSessionCookie()
		{
		    const string cookieName = "token";
		    const string token = "a";
			var user = new FakeUser(token: token);
            GetMock<IUserRepository>().Setup(o => o.GetByNameOrEmail(It.IsAny<string>())).Returns(user);
            GetMock<IUrlProvider>().Setup(o => o.GetHomeUrl()).Returns("any");

            var sut = GetSut();
			sut.Execute();

            GetMock<IWebContext>().Verify(o => o.SetSessionCookie(cookieName, token));
		}

        [Test]
        public void Execute_UserFoundAndRememberChecked_SetsPersistentCookie(){
            const string cookieName = "token";
            const string token = "a";
            _rememberMe = true;
            var user = new FakeUser(token: token);
            GetMock<IUserRepository>().Setup(o => o.GetByNameOrEmail(It.IsAny<string>())).Returns(user);
            GetMock<IUrlProvider>().Setup(o => o.GetHomeUrl()).Returns("any");

            var sut = GetSut();
            sut.Execute();

            GetMock<IWebContext>().Verify(o => o.SetPersistentCookie(cookieName, token));
		}

        private LoginCommand GetSut()
        {
            var postModel = new AuthLoginPostModel
                {
                    LoginName = _loginName,
                    Password = _password,
                    RememberMe = _rememberMe
                };

            return new LoginCommand(
                GetMock<IUserRepository>().Object,
                GetMock<IEncryptionService>().Object,
                GetMock<IWebContext>().Object,
                GetMock<IFormsAuthenticationService>().Object,
                GetMock<IHomegameRepository>().Object,
                postModel)
            ;
        }

	}

}