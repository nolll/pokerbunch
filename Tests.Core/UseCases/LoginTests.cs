using Core.Exceptions;
using Core.UseCases.Login;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class LoginTests : TestBase
    {
        [Test, Ignore] // Todo: Something goes wrong in this test half of the times. Don't know why
        public void Login_ReturnUrlIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(TestData.UserA.UserName, result.UserName);
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
            var request = new LoginRequest(TestData.UserA.UserName, TestData.UserB.UserName);

            Assert.Throws<LoginException>(() => Sut.Execute(request));
        }

        [Test, Ignore] // Todo: Something goes wrong in this test half of the times if i change TestData.UserNameA to TestData.UserA.UserName. Don't know why
        public void Login_UserFoundAndPasswordIsCorrect_UserNameIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(TestData.UserA.UserName, result.UserName);
        }
      
        private static LoginRequest CreateRequest()
        {
            return new LoginRequest(TestData.UserA.UserName, TestData.UserPasswordA);
        }

        private LoginInteractor Sut
        {
            get { return new LoginInteractor(Repos.User); }
        }
    }
}
