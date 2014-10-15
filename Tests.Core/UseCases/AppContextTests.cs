﻿using Core.Entities;
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
            Assert.IsEmpty(result.UserName);
            Assert.IsEmpty(result.UserDisplayName);
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