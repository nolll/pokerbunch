using System.Linq;
using Application.Services;
using Core.Entities;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.AuthCommands;
using Web.Models.AuthModels;
using Web.Security;

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
			var user = new UserInTest(encryptedPassword: userPassword);
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
            var user = new UserInTest(encryptedPassword: userPassword);
            GetMock<IUserRepository>().Setup(o => o.GetByNameOrEmail(It.IsAny<string>())).Returns(user);
            GetMock<IEncryptionService>().Setup(o => o.Encrypt(It.IsAny<string>(), It.IsAny<string>())).Returns(_password);

            var sut = GetSut();
            var result = sut.Execute();

            Assert.IsFalse(result);
            Assert.AreEqual(1, sut.Errors.Count());
		}
        
        [Test]
        public void Execute_UserFound_SignsIn()
		{
			var user = new UserInTest();
            GetMock<IUserRepository>().Setup(o => o.GetByNameOrEmail(It.IsAny<string>())).Returns(user);

            var sut = GetSut();
			sut.Execute();

            GetMock<IAuth>().Verify(o => o.SignIn(It.IsAny<UserIdentity>(), false));
		}

        [Test]
        public void Execute_UserFoundAndRememberChecked_SetsPersistentCookie()
        {
            _rememberMe = true;
            var user = new UserInTest();

            GetMock<IUserRepository>().Setup(o => o.GetByNameOrEmail(It.IsAny<string>())).Returns(user);

            var sut = GetSut();
            sut.Execute();

            GetMock<IAuth>().Verify(o => o.SignIn(It.IsAny<UserIdentity>(), true));
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
                GetMock<IAuth>().Object,
                GetMock<IHomegameRepository>().Object,
                GetMock<IPlayerRepository>().Object,
                postModel)
            ;
        }

	}

}