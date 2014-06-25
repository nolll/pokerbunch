using Application.Services;
using Application.UseCases.AppContext;
using Core.Entities;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class AppContextInteractorTests : MockContainer
    {
        [Test]
        public void Execute_WithoutUser_AllPropertiesAreSet()
        {
            GetMock<IBaseContextInteractor>().Setup(o => o.Execute()).Returns(new BaseContextResultInTest());

            var result = Sut.Execute();

            Assert.IsFalse(result.IsLoggedIn);
            Assert.IsFalse(result.IsAdmin);
            Assert.IsEmpty(result.UserName);
            Assert.IsEmpty(result.UserDisplayName);
        }

        [Test]
        public void Execute_WithUser_LoggedInPropertiesAreSet()
        {
            const string userName = "a";
            const string userDisplayName = "b";
            var user = new UserInTest(userName: userName, displayName: userDisplayName);

            GetMock<IBaseContextInteractor>().Setup(o => o.Execute()).Returns(new BaseContextResultInTest());
            GetMock<IAuth>().Setup(o => o.CurrentUser).Returns(user);

            var result = Sut.Execute();

            Assert.IsTrue(result.IsLoggedIn);
            Assert.IsFalse(result.IsAdmin);
            Assert.AreEqual(userName, result.UserName);
            Assert.AreEqual(userDisplayName, result.UserDisplayName);
        }

        [Test]
        public void Execute_WithAdminUser_AdminIsTrue()
        {
            var user = new UserInTest(globalRole: Role.Admin);

            GetMock<IBaseContextInteractor>().Setup(o => o.Execute()).Returns(new BaseContextResultInTest());
            GetMock<IAuth>().Setup(o => o.CurrentUser).Returns(user);

            var result = Sut.Execute();

            Assert.IsTrue(result.IsAdmin);
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