﻿using System.Collections.Generic;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.UseCases.Login;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class LoginTests : TestBase
    {
        private const string LoginName = "a";
        private const string Password = "b";
        private const string UserName = "c";
        private const string UserDisplayName = "d";
        private const string Slug = "e";
        private const string ReturnUrl = "g";
        private const string EncryptedPassword = "e9d71f5ee7c92d6dc9e92ffdad17b8bd49418f98";
        private const int UserId = 1;

        [Test]
        public void Login_ReturnUrlIsSet()
        {
            SetupUserWithCorrectPassword();

            var result = Execute(CreateRequest());

            Assert.AreEqual(ReturnUrl, result.ReturnUrl.Relative);
        }

        [Test]
        public void Login_UserNotFound_ThrowsException()
        {
            Assert.Throws<LoginException>(() => Execute(CreateRequest()));
        }

        [Test]
        public void Login_UserFoundButPasswordIsWrong_ThrowsException()
        {
            SetupUserWithWrongPassword();

            Assert.Throws<LoginException>(() => Execute(CreateRequest()));
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrect_UserIsSignedIn()
        {
            SetupUserWithCorrectPassword();
            
            Execute(CreateRequest());

            Assert.IsNotNull(Services.Auth.UserIdentity);
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrect_UserIdentityHasUserProperties()
        {
            SetupUserWithCorrectPassword();

            Execute(CreateRequest());

            Assert.AreEqual(UserId, Services.Auth.UserIdentity.UserId);
            Assert.AreEqual(UserName, Services.Auth.UserIdentity.UserName);
            Assert.AreEqual(UserDisplayName, Services.Auth.UserIdentity.DisplayName);
            Assert.IsFalse(Services.Auth.UserIdentity.IsAdmin);
            Assert.AreEqual(0, Services.Auth.UserIdentity.Bunches.Count);
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrectAndUserBelongsToABunch_UserIdentityBunchesPropertiesAreCorrect()
        {
            var user = SetupUserWithCorrectPassword();
            var bunch = A.Bunch.WithSlug(Slug).Build();
            var homegameList = new List<Bunch> { bunch };
            GetMock<IBunchRepository>().Setup(o => o.GetByUserId(user.Id)).Returns(homegameList);

            Execute(CreateRequest());

            Assert.AreEqual(1, Services.Auth.UserIdentity.Bunches.Count);
            Assert.AreEqual(Slug, Services.Auth.UserIdentity.Bunches[0].Slug);
            Assert.AreEqual(Role.Player, Services.Auth.UserIdentity.Bunches[0].Role);
            Assert.AreEqual(Constants.PlayerNameA, Services.Auth.UserIdentity.Bunches[0].Name);
            Assert.AreEqual(Constants.PlayerIdA, Services.Auth.UserIdentity.Bunches[0].Id);
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrectAndUserBelongsToTwoBunch_UserIdentityBunchesLengthIsCorrect()
        {
            var user = SetupUserWithCorrectPassword();
            var bunch = A.Bunch.WithSlug(Slug).Build();
            var homegameList = new List<Bunch> { bunch, bunch };
            GetMock<IBunchRepository>().Setup(o => o.GetByUserId(user.Id)).Returns(homegameList);

            Execute(CreateRequest());

            Assert.AreEqual(2, Services.Auth.UserIdentity.Bunches.Count);
        }

        [Test]
        public void Login_AdminUser_UserIdentityIsAdminIsTrue()
        {
            SetupAdminUserWithCorrectPassword();

            Execute(CreateRequest());

            Assert.IsTrue(Services.Auth.UserIdentity.IsAdmin);
        }

        [Test]
        public void Login_RememberMeIsFalse_SignInDoesntSavePersistentCookie()
        {
            SetupUserWithCorrectPassword();

            Execute(CreateRequest());

            Assert.IsFalse(Services.Auth.StayLoggedIn);
        }

        [Test]
        public void Login_RememberMeIsTrue_SignInSavesPersistentCookie()
        {
            SetupUserWithCorrectPassword();

            Execute(CreateRequest(true));

            Assert.IsTrue(Services.Auth.StayLoggedIn);
        }

        private void SetupUserWithWrongPassword()
        {
            SetupUser(A.User.Build());
        }

        private User SetupUserWithCorrectPassword(Role role = Role.Player)
        {
            var user = A.User.WithId(UserId).WithUserName(UserName).WithDisplayName(UserDisplayName).WithEncryptedPassword(EncryptedPassword).WithGlobalRole(role).Build();
            SetupUser(user);
            return user;
        }

        private void SetupAdminUserWithCorrectPassword()
        {
            SetupUserWithCorrectPassword(Role.Admin);
        }

        private void SetupUser(User user)
        {
            GetMock<IUserRepository>().Setup(o => o.GetByNameOrEmail(LoginName)).Returns(user);
        }

        private static LoginRequest CreateRequest(bool rememberMe = false)
        {
            return new LoginRequest(LoginName, Password, rememberMe, ReturnUrl);
        }

        private LoginResult Execute(LoginRequest request)
        {
            return LoginInteractor.Execute(
                GetMock<IUserRepository>().Object,
                Services.Auth,
                GetMock<IBunchRepository>().Object,
                Repos.Player,
                request);
        }
    }
}
