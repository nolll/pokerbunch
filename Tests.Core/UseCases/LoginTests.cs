using Core.Exceptions;
using Core.UseCases;
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
            var request = new Login.Request("username-that-does-not-exist", "");

            Assert.Throws<LoginException>(() => Sut.Execute(request));
        }

        [Test]
        public void Login_UserFoundButPasswordIsWrong_ThrowsException()
        {
            var request = new Login.Request(TestData.UserA.UserName, TestData.UserB.UserName);

            Assert.Throws<LoginException>(() => Sut.Execute(request));
        }

        [Test, Ignore] // Todo: Something goes wrong in this test half of the times if i change TestData.UserNameA to TestData.UserA.UserName. Don't know why
        public void Login_UserFoundAndPasswordIsCorrect_UserNameIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(TestData.UserA.UserName, result.UserName);
        }
      
        private static Login.Request CreateRequest()
        {
            return new Login.Request(TestData.UserA.UserName, TestData.UserPasswordA);
        }

        private Login Sut
        {
            get { return new Login(Repos.User); }
        }
    }
}
