using Application.Services;
using Application.Urls;
using Application.UseCases.BaseContext;
using Application.UseCases.Home;
using Core.Entities;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class HomeTests : MockContainer
    {
        [Test]
        public void Home_UrlsAreSet()
        {
            var result = Sut.Execute();

            Assert.IsInstanceOf<AddHomegameUrl>(result.AddBunchUrl);
            Assert.IsInstanceOf<LoginUrl>(result.LoginUrl);
            Assert.IsInstanceOf<AddUserUrl>(result.AddUserUrl);
        }

        [Test]
        public void Home_NotLoggedIn_IsLoggedInAndIsAdminIsFalse()
        {
            var result = Sut.Execute();

            Assert.IsFalse(result.IsLoggedIn);
            Assert.IsFalse(result.IsAdmin);
        }

        [Test]
        public void Home_LoggedIn_IsLoggedInIsTrue()
        {
            SetupBaseContext();
            var user = AUser.Build();
            SetupUser(user);

            var result = Sut.Execute();

            Assert.IsTrue(result.IsLoggedIn);
            Assert.IsFalse(result.IsAdmin);
        }

        [Test]
        public void Home_LoggedInAsAdmin_IsAdminIsTrue()
        {
            SetupBaseContext();
            var user = AUser.IsAdmin().Build();
            SetupUser(user);

            var result = Sut.Execute();

            Assert.IsTrue(result.IsAdmin);
        }

        private void SetupUser(User user)
        {
            GetMock<IAuth>().Setup(o => o.CurrentUser).Returns(user);
        }

        private void SetupBaseContext()
        {
            GetMock<IBaseContextInteractor>().Setup(o => o.Execute()).Returns(new BaseContextResultInTest());
        }

        private HomeInteractor Sut
        {
            get
            {
                return new HomeInteractor(GetMock<IAuth>().Object);
            }
        }
    }
}
