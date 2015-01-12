﻿using Core;
using Core.Entities;
using Core.Services;
using Core.Urls;
using Core.UseCases.Home;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
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
            Assert.IsInstanceOf<ClearCacheUrl>(result.ClearCacheUrl);
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
            Services.Auth.CurrentIdentity = new CustomIdentity(true, new UserIdentity());

            var result = Execute();

            Assert.IsTrue(result.IsLoggedIn);
            Assert.IsFalse(result.IsAdmin);
        }

        [Test]
        public void Home_LoggedInAsAdmin_IsAdminIsTrue()
        {
            Services.Auth.CurrentIdentity = new CustomIdentity(true, new UserIdentity{IsAdmin = true});

            var result = Execute();

            Assert.IsTrue(result.IsAdmin);
        }

        private HomeResult Execute()
        {
            return HomeInteractor.Execute(Services.Auth);
        }
    }
}
