using Application.Services;
using Application.UseCases.AppContext;
using Application.UseCases.BaseContext;
using Core.Entities;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class AppContextTests : MockContainer
    {
        [Test]
        public void AppContext_WithoutUser_AllPropertiesAreSet()
        {
            SetupBaseContext();

            var result = Sut.Execute();

            Assert.IsFalse(result.IsLoggedIn);
            Assert.IsFalse(result.IsAdmin);
            Assert.IsEmpty(result.UserName);
            Assert.IsEmpty(result.UserDisplayName);
        }

        [Test]
        public void AppContext_WithUser_LoggedInPropertiesAreSet()
        {
            SetupBaseContext();
            var user = AUser.Build();
            SetupUser(user);

            var result = Sut.Execute();

            Assert.IsTrue(result.IsLoggedIn);
            Assert.IsFalse(result.IsAdmin);
            Assert.AreEqual("a", result.UserName);
            Assert.AreEqual("b", result.UserDisplayName);
        }

        [Test]
        public void AppContext_WithAdminUser_AdminIsTrue()
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

        private AppContextInteractor Sut
        {
            get {
                return new AppContextInteractor(
                    GetMock<IBaseContextInteractor>().Object,
                    GetMock<IAuth>().Object);
            }
        }
    }
}