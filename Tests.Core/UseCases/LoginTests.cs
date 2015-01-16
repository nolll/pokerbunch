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
        private const string UserName = "c";
        private const string UserDisplayName = "d";
        private const string ReturnUrl = "g";
        private const string EncryptedPassword = "e9d71f5ee7c92d6dc9e92ffdad17b8bd49418f98";
        private const int UserId = 1;

        [Test]
        public void Login_ReturnUrlIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(ReturnUrl, result.ReturnUrl.Relative);
        }

        [Test]
        public void Login_UserNotFound_ThrowsException()
        {
            var request = new LoginRequest("username-that-does-not-exist", "", false, "");

            Assert.Throws<LoginException>(() => Execute(request));
        }

        [Test]
        public void Login_UserFoundButPasswordIsWrong_ThrowsException()
        {
            var request = new LoginRequest(Constants.UserNameA, Constants.UserNameB, false, "");

            Assert.Throws<LoginException>(() => Execute(request));
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrect_UserIsSignedIn()
        {
            Execute(CreateRequest());

            Assert.IsNotNull(Services.Auth.UserIdentity);
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrect_UserIdentityHasUserProperties()
        {
            Execute(CreateRequest());

            Assert.AreEqual(Constants.UserIdA, Services.Auth.UserIdentity.UserId);
            Assert.AreEqual(Constants.UserNameA, Services.Auth.UserIdentity.UserName);
            Assert.AreEqual(Constants.UserDisplayNameA, Services.Auth.UserIdentity.DisplayName);
            Assert.IsFalse(Services.Auth.UserIdentity.IsAdmin);
            Assert.AreEqual(2, Services.Auth.UserIdentity.Bunches.Count);
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrectAndUserBelongsToABunch_UserIdentityBunchesPropertiesAreCorrect()
        {
            Execute(CreateRequest());

            Assert.AreEqual(2, Services.Auth.UserIdentity.Bunches.Count);
            Assert.AreEqual(Constants.SlugA, Services.Auth.UserIdentity.Bunches[0].Slug);
            Assert.AreEqual(Role.Player, Services.Auth.UserIdentity.Bunches[0].Role);
            Assert.AreEqual(Constants.PlayerNameA, Services.Auth.UserIdentity.Bunches[0].Name);
            Assert.AreEqual(Constants.PlayerIdA, Services.Auth.UserIdentity.Bunches[0].Id);
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrectAndUserBelongsToTwoBunch_UserIdentityBunchesLengthIsCorrect()
        {
            Execute(CreateRequest());

            Assert.AreEqual(2, Services.Auth.UserIdentity.Bunches.Count);
        }

        [Test]
        public void Login_AdminUser_UserIdentityIsAdminIsTrue()
        {
            var request = new LoginRequest(Constants.UserNameB, Constants.UserPasswordB, false, "");

            Execute(request);

            Assert.IsTrue(Services.Auth.UserIdentity.IsAdmin);
        }

        [Test]
        public void Login_RememberMeIsFalse_SignInDoesntSavePersistentCookie()
        {
            Execute(CreateRequest());

            Assert.IsFalse(Services.Auth.StayLoggedIn);
        }

        [Test]
        public void Login_RememberMeIsTrue_SignInSavesPersistentCookie()
        {
            Execute(CreateRequest(true));

            Assert.IsTrue(Services.Auth.StayLoggedIn);
        }

        private static LoginRequest CreateRequest(bool rememberMe = false)
        {
            return new LoginRequest(Constants.UserNameA, Constants.UserPasswordA, rememberMe, ReturnUrl);
        }

        private LoginResult Execute(LoginRequest request)
        {
            return LoginInteractor.Execute(
                Repos.User,
                Services.Auth,
                Repos.Bunch,
                Repos.Player,
                request);
        }
    }
}
