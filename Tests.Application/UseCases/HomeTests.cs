using Application.Services;
using Application.Urls;
using Application.UseCases.Home;
using Core.Entities;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.UseCases
{
    class HomeTests : TestBase
    {
        [Test]
        public void Home_UrlsAreSet()
        {
            var result = Execute();

            Assert.IsInstanceOf<AddBunchUrl>(result.AddBunchUrl);
            Assert.IsInstanceOf<LoginUrl>(result.LoginUrl);
            Assert.IsInstanceOf<AddUserUrl>(result.AddUserUrl);
            Assert.IsInstanceOf<UserListUrl>(result.UserListUrl);
            Assert.IsInstanceOf<BunchListUrl>(result.BunchListUrl);
            Assert.IsInstanceOf<TestEmailUrl>(result.TestEmailUrl);
        }

        [Test]
        public void Home_NotLoggedIn_IsLoggedInAndIsAdminIsFalse()
        {
            var result = Execute();

            Assert.IsFalse(result.IsLoggedIn);
            Assert.IsFalse(result.IsAdmin);
        }

        [Test]
        public void Home_LoggedIn_IsLoggedInIsTrue()
        {
            var user = A.User.Build();
            SetupUser(user);

            var result = Execute();

            Assert.IsTrue(result.IsLoggedIn);
            Assert.IsFalse(result.IsAdmin);
        }

        [Test]
        public void Home_LoggedInAsAdmin_IsAdminIsTrue()
        {
            var user = A.User.IsAdmin().Build();
            SetupUser(user);

            var result = Execute();

            Assert.IsTrue(result.IsAdmin);
        }

        private void SetupUser(User user)
        {
            GetMock<IAuth>().Setup(o => o.CurrentUser).Returns(user);
        }

        private HomeResult Execute()
        {
            return HomeInteractor.Execute(GetMock<IAuth>().Object);
        }
    }
}
