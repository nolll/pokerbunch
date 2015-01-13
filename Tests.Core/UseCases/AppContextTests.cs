using Core;
using Core.Entities;
using Core.UseCases.AppContext;
using Core.UseCases.BaseContext;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Core.UseCases
{
    class AppContextTests : TestBase
    {
        [Test]
        public void AppContext_WithoutUser_AllPropertiesAreSet()
        {
            var result = Execute();

            Assert.IsFalse(result.IsLoggedIn);
            Assert.IsEmpty(result.UserDisplayName);
            Assert.AreEqual("/-/auth/login", result.LoginUrl.Relative);
            Assert.AreEqual("/-/user/add", result.AddUserUrl.Relative);
            Assert.AreEqual("/-/user/forgotpassword", result.ForgotPasswordUrl.Relative);
        }

        [Test]
        public void AppContext_WithUser_LoggedInPropertiesAreSet()
        {
            const string userName = "a";
            const string displayName = "b";
            Services.Auth.CurrentIdentity = new CustomIdentity(new UserIdentity{DisplayName = displayName, UserName = userName});

            var result = Execute();

            Assert.IsTrue(result.IsLoggedIn);
            Assert.AreEqual("b", result.UserDisplayName);
            Assert.AreEqual("/-/auth/logout", result.LogoutUrl.Relative);
            Assert.AreEqual("/-/user/details/a", result.UserDetailsUrl.Relative);
        }

        private AppContextResult Execute()
        {
            return AppContextInteractor.Execute(
                BaseContextFunc,
                Services.Auth);
        }

        private BaseContextResult BaseContextFunc()
        {
            return new BaseContextResultInTest();
        }  
    }
}