using Core.Entities;
using Core.Services;
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
            var user = A.User.WithUserName(userName).WithDisplayName(displayName).Build();
            SetupUser(user);

            var result = Execute();

            Assert.IsTrue(result.IsLoggedIn);
            Assert.AreEqual("b", result.UserDisplayName);
            Assert.AreEqual("/-/auth/logout", result.LogoutUrl.Relative);
            Assert.AreEqual("/-/user/details/a", result.UserDetailsUrl.Relative);
        }

        private AppContextResult Execute()
        {
            return AppContextInteractor.Execute(BaseContextFunc, GetMock<IAuth>().Object);
        }

        private BaseContextResult BaseContextFunc()
        {
            return new BaseContextResultInTest();
        }  

        private void SetupUser(User user)
        {
            GetMock<IAuth>().Setup(o => o.CurrentUser).Returns(user);
        }
    }
}