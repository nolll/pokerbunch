using Core.Exceptions;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class AppContextTests : TestBase
    {
        [Test]
        public void AppContext_WithoutUserName_AllPropertiesAreSet()
        {
            var result = Sut.Execute(new AppContext.Request(null));

            Assert.IsFalse(result.IsLoggedIn);
            Assert.IsEmpty(result.UserDisplayName);
            Assert.AreEqual("/-/auth/login", result.LoginUrl.Relative);
            Assert.AreEqual("/-/user/add", result.AddUserUrl.Relative);
            Assert.AreEqual("/-/user/forgotpassword", result.ForgotPasswordUrl.Relative);
        }

        [Test]
        public void AppContext_WithUserName_LoggedInPropertiesAreSet()
        {
            var result = Sut.Execute(new AppContext.Request(TestData.UserA.UserName));

            Assert.IsTrue(result.IsLoggedIn);
            Assert.AreEqual(TestData.UserDisplayNameA, result.UserDisplayName);
            Assert.AreEqual("/-/auth/logout", result.LogoutUrl.Relative);
            Assert.AreEqual("/-/user/details/user-name-a", result.UserDetailsUrl.Relative);
        }

        [Test]
        public void AppContext_WithInvalidUserName_LoggedInPropertiesAreSet()
        {
            var request = new AppContext.Request("1");
            Assert.Throws<NotLoggedInException>(() => Sut.Execute(request));
        }

        private AppContext Sut
        {
            get { return new AppContext(Repos.User); }
        }
    }
}