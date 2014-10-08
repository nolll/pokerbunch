using System.Collections.Generic;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.UseCases.Login;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Core.UseCases
{
    class LoginTests : TestBase
    {
        private const string LoginName = "a";
        private const string Password = "b";
        private const string UserName = "c";
        private const string UserDisplayName = "d";
        private const string Slug = "e";
        private const string PlayerDisplayName = "f";
        private const string ReturnUrl = "g";
        private const string EncryptedPassword = "e9d71f5ee7c92d6dc9e92ffdad17b8bd49418f98";
        private const int UserId = 1;
        private const int PlayerId = 2;

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

            GetMock<IAuth>().Verify(o => o.SignIn(It.IsAny<UserIdentity>(), It.IsAny<bool>()));
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrect_UserIdentityHasUserProperties()
        {
            SetupUserWithCorrectPassword();
            UserIdentity result = null;

            GetMock<IAuth>().
                Setup(o => o.SignIn(It.IsAny<UserIdentity>(), It.IsAny<bool>())).
                Callback((UserIdentity identity, bool createPersistentCookie) => result = identity);

            Execute(CreateRequest());

            Assert.AreEqual(UserId, result.UserId);
            Assert.AreEqual(UserName, result.UserName);
            Assert.AreEqual(UserDisplayName, result.DisplayName);
            Assert.IsFalse(result.IsAdmin);
            Assert.AreEqual(0, result.Bunches.Count);
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrectAndUserBelongsToABunch_UserIdentityBunchesPropertiesAreCorrect()
        {
            var user = SetupUserWithCorrectPassword();
            var bunch = A.Bunch.WithSlug(Slug).Build();
            var homegameList = new List<Bunch> { bunch };
            GetMock<IBunchRepository>().Setup(o => o.GetByUser(user)).Returns(homegameList);

            GetMock<IBunchRepository>().Setup(o => o.GetRole(It.IsAny<Bunch>(), It.IsAny<User>())).Returns(Role.Player);

            var player = A.Player.WithId(PlayerId).WithDisplayName(PlayerDisplayName).Build();
            GetMock<IPlayerRepository>().Setup(o => o.GetByUserName(It.IsAny<Bunch>(), UserName)).Returns(player);

            UserIdentity result = null;

            GetMock<IAuth>().
                Setup(o => o.SignIn(It.IsAny<UserIdentity>(), It.IsAny<bool>())).
                Callback((UserIdentity identity, bool createPersistentCookie) => result = identity);

            Execute(CreateRequest());

            Assert.AreEqual(1, result.Bunches.Count);
            Assert.AreEqual(Slug, result.Bunches[0].Slug);
            Assert.AreEqual(Role.Player, result.Bunches[0].Role);
            Assert.AreEqual(PlayerDisplayName, result.Bunches[0].Name);
            Assert.AreEqual(PlayerId, result.Bunches[0].Id);
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrectAndUserBelongsToTwoBunch_UserIdentityBunchesLengthIsCorrect()
        {
            var user = SetupUserWithCorrectPassword();
            var bunch = A.Bunch.WithSlug(Slug).Build();
            var homegameList = new List<Bunch> { bunch, bunch };
            GetMock<IBunchRepository>().Setup(o => o.GetByUser(user)).Returns(homegameList);

            GetMock<IBunchRepository>().Setup(o => o.GetRole(It.IsAny<Bunch>(), It.IsAny<User>())).Returns(Role.Player);

            var player = A.Player.WithId(PlayerId).WithDisplayName(PlayerDisplayName).Build();
            GetMock<IPlayerRepository>().Setup(o => o.GetByUserName(It.IsAny<Bunch>(), UserName)).Returns(player);

            UserIdentity result = null;

            GetMock<IAuth>().
                Setup(o => o.SignIn(It.IsAny<UserIdentity>(), It.IsAny<bool>())).
                Callback((UserIdentity identity, bool createPersistentCookie) => result = identity);

            Execute(CreateRequest());

            Assert.AreEqual(2, result.Bunches.Count);
        }

        [Test]
        public void Login_AdminUser_UserIdentityIsAdminIsTrue()
        {
            SetupAdminUserWithCorrectPassword();
            UserIdentity result = null;

            GetMock<IAuth>().
                Setup(o => o.SignIn(It.IsAny<UserIdentity>(), It.IsAny<bool>())).
                Callback((UserIdentity identity, bool createPersistentCookie) => result = identity);

            Execute(CreateRequest());

            Assert.IsTrue(result.IsAdmin);
        }

        [Test]
        public void Login_RememberMeIsFalse_SignInDoesntSavePersistentCookie()
        {
            SetupUserWithCorrectPassword();
            var result = false;

            GetMock<IAuth>().
                Setup(o => o.SignIn(It.IsAny<UserIdentity>(), It.IsAny<bool>())).
                Callback((UserIdentity identity, bool createPersistentCookie) => result = createPersistentCookie);

            Execute(CreateRequest());

            Assert.IsFalse(result);
        }

        [Test]
        public void Login_RememberMeIsTrue_SignInSavesPersistentCookie()
        {
            SetupUserWithCorrectPassword();
            var result = false;

            GetMock<IAuth>().
                Setup(o => o.SignIn(It.IsAny<UserIdentity>(), It.IsAny<bool>())).
                Callback((UserIdentity identity, bool createPersistentCookie) => result = createPersistentCookie);

            Execute(CreateRequest(true));

            Assert.IsTrue(result);
        }

        private LoginResult Execute(LoginRequest request)
        {
            return LoginInteractor.Execute(
                GetMock<IUserRepository>().Object,
                GetMock<IAuth>().Object,
                GetMock<IBunchRepository>().Object,
                GetMock<IPlayerRepository>().Object,
                request);
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
    }
}
