using System;
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
            var result = Execute();

            Assert.IsFalse(result.IsLoggedIn);
            Assert.IsEmpty(result.UserName);
            Assert.IsEmpty(result.UserDisplayName);
        }

        [Test]
        public void AppContext_WithUser_LoggedInPropertiesAreSet()
        {
            var user = AUser.Build();
            SetupUser(user);

            var result = Execute();

            Assert.IsTrue(result.IsLoggedIn);
            Assert.AreEqual("a", result.UserName);
            Assert.AreEqual("b", result.UserDisplayName);
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