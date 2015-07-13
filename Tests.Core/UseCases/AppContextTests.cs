using Core.Exceptions;
using Core.UseCases.AppContext;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class AppContextTests : TestBase
    {
        [Test]
        public void AppContext_WithoutUserName_AllPropertiesAreSet()
        {
            var result = Sut.Execute(new AppContextRequest(null));

            Assert.IsFalse(result.IsLoggedIn);
            Assert.IsEmpty(result.UserDisplayName);
            Assert.AreEqual("/-/auth/login", result.LoginUrl.Relative);
            Assert.AreEqual("/-/user/add", result.AddUserUrl.Relative);
            Assert.AreEqual("/-/user/forgotpassword", result.ForgotPasswordUrl.Relative);
        }

        [Test]
        public void AppContext_WithUserName_LoggedInPropertiesAreSet()
        {
            var result = Sut.Execute(new AppContextRequest(Constants.UserNameA));

            Assert.IsTrue(result.IsLoggedIn);
            Assert.AreEqual(Constants.UserDisplayNameA, result.UserDisplayName);
            Assert.AreEqual("/-/auth/logout", result.LogoutUrl.Relative);
            Assert.AreEqual("/-/user/details/user-name-a", result.UserDetailsUrl.Relative);
        }

        [Test]
        public void AppContext_WithInvalidUserName_LoggedInPropertiesAreSet()
        {
            var request = new AppContextRequest("1");
            Assert.Throws<NotLoggedInException>(() => Sut.Execute(request));
        }

        private AppContextInteractor Sut
        {
            get { return new AppContextInteractor(Repos.User); }
        }
    }
}