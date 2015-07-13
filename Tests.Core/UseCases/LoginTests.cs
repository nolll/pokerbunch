using Core.Exceptions;
using Core.UseCases.Login;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class LoginTests : TestBase
    {
        [Test]
        public void Login_ReturnUrlIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(Constants.UserNameA, result.UserName);
        }

        [Test]
        public void Login_UserNotFound_ThrowsException()
        {
            var request = new LoginRequest("username-that-does-not-exist", "");

            Assert.Throws<LoginException>(() => Sut.Execute(request));
        }

        [Test]
        public void Login_UserFoundButPasswordIsWrong_ThrowsException()
        {
            var request = new LoginRequest(Constants.UserNameA, Constants.UserNameB);

            Assert.Throws<LoginException>(() => Sut.Execute(request));
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrect_UserNameIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(Constants.UserNameA, result.UserName);
        }

        [Test]
        public void Login_RememberMeIsFalse_SignInDoesntSavePersistentCookie()
        {
            Sut.Execute(CreateRequest());

            Assert.IsFalse(Services.Auth.StayLoggedIn);
        }
        
        private static LoginRequest CreateRequest()
        {
            return new LoginRequest(Constants.UserNameA, Constants.UserPasswordA);
        }

        private LoginInteractor Sut
        {
            get { return new LoginInteractor(Repos.User); }
        }
    }
}
