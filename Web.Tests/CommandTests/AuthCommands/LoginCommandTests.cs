using System.Linq;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.AuthCommands;

namespace Web.Tests.CommandTests.AuthCommands{

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
        public void Execute_UserFound_ReturnsTrue(){
			var user = new FakeUser();
            Mocks.UserRepositoryMock.Setup(o => o.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

            var sut = GetSut();
            var result = sut.Execute();

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.Errors.Count());
		}
        
        [Test]
        public void Execute_UserNotFound_ReturnsFalse(){
            var sut = GetSut();
            var result = sut.Execute();

            Assert.IsFalse(result);
            Assert.AreEqual(1, sut.Errors.Count());
		}
        
        [Test]
        public void Execute_UserFound_SetsSessionCookie()
		{
		    const string cookieName = "token";
		    const string tokenName = "a";
			var user = new FakeUser();
            Mocks.UserRepositoryMock.Setup(o => o.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(user);
            Mocks.UserRepositoryMock.Setup(o => o.GetToken(It.IsAny<User>())).Returns(tokenName);
            Mocks.UrlProviderMock.Setup(o => o.GetHomeUrl()).Returns("any");

            var sut = GetSut();
			sut.Execute();

            Mocks.WebContextMock.Verify(o => o.SetSessionCookie(cookieName, tokenName));
		}

        [Test]
        public void Execute_UserFoundAndRememberChecked_SetsPersistentCookie(){
            const string cookieName = "token";
            const string tokenName = "a";
            _rememberMe = true;
            var user = new FakeUser();
            Mocks.UserRepositoryMock.Setup(o => o.GetUserByCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(user);
            Mocks.UserRepositoryMock.Setup(o => o.GetToken(It.IsAny<User>())).Returns(tokenName);
            Mocks.UrlProviderMock.Setup(o => o.GetHomeUrl()).Returns("any");

            var sut = GetSut();
            sut.Execute();

            Mocks.WebContextMock.Verify(o => o.SetPersistentCookie(cookieName, tokenName));
		}

        private LoginCommand GetSut()
        {
            return new LoginCommand(
                Mocks.UserRepositoryMock.Object,
                Mocks.EncryptionServiceMock.Object,
                Mocks.WebContextMock.Object,
                _loginName,
                _password,
                _rememberMe);
        }

	}

}