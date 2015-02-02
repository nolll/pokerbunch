using Core.Entities;
using Core.Exceptions;
using Core.UseCases.Login;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class LoginTests : TestBase
    {
        private const string ReturnUrl = "g";

        [Test]
        public void Login_ReturnUrlIsSet()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(ReturnUrl, result.ReturnUrl.Relative);
        }

        [Test]
        public void Login_UserNotFound_ThrowsException()
        {
            var request = new LoginRequest("username-that-does-not-exist", "", false, "");

            Assert.Throws<LoginException>(() => Sut.Execute(request));
        }

        [Test]
        public void Login_UserFoundButPasswordIsWrong_ThrowsException()
        {
            var request = new LoginRequest(Constants.UserNameA, Constants.UserNameB, false, "");

            Assert.Throws<LoginException>(() => Sut.Execute(request));
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrect_UserIsSignedIn()
        {
            Sut.Execute(CreateRequest());

            Assert.IsNotNull(Services.Auth.UserIdentity);
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrect_UserIdentityHasUserProperties()
        {
            Sut.Execute(CreateRequest());

            Assert.AreEqual(Constants.UserIdA, Services.Auth.UserIdentity.UserId);
            Assert.AreEqual(Constants.UserNameA, Services.Auth.UserIdentity.UserName);
            Assert.AreEqual(Constants.UserDisplayNameA, Services.Auth.UserIdentity.DisplayName);
            Assert.IsFalse(Services.Auth.UserIdentity.IsAdmin);
            Assert.AreEqual(2, Services.Auth.UserIdentity.Bunches.Count);
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrectAndUserBelongsToABunch_UserIdentityBunchesPropertiesAreCorrect()
        {
            Sut.Execute(CreateRequest());

            Assert.AreEqual(2, Services.Auth.UserIdentity.Bunches.Count);
            Assert.AreEqual(Constants.SlugA, Services.Auth.UserIdentity.Bunches[0].Slug);
            Assert.AreEqual(Role.Player, Services.Auth.UserIdentity.Bunches[0].Role);
            Assert.AreEqual(Constants.PlayerNameA, Services.Auth.UserIdentity.Bunches[0].Name);
            Assert.AreEqual(Constants.PlayerIdA, Services.Auth.UserIdentity.Bunches[0].Id);
        }

        [Test]
        public void Login_UserFoundAndPasswordIsCorrectAndUserBelongsToTwoBunch_UserIdentityBunchesLengthIsCorrect()
        {
            Sut.Execute(CreateRequest());

            Assert.AreEqual(2, Services.Auth.UserIdentity.Bunches.Count);
        }

        [Test]
        public void Login_AdminUser_UserIdentityIsAdminIsTrue()
        {
            var request = new LoginRequest(Constants.UserNameB, Constants.UserPasswordB, false, "");

            Sut.Execute(request);

            Assert.IsTrue(Services.Auth.UserIdentity.IsAdmin);
        }

        [Test]
        public void Login_RememberMeIsFalse_SignInDoesntSavePersistentCookie()
        {
            Sut.Execute(CreateRequest());

            Assert.IsFalse(Services.Auth.StayLoggedIn);
        }

        [Test]
        public void Login_RememberMeIsTrue_SignInSavesPersistentCookie()
        {
            Sut.Execute(CreateRequest(true));

            Assert.IsTrue(Services.Auth.StayLoggedIn);
        }

        private static LoginRequest CreateRequest(bool rememberMe = false)
        {
            return new LoginRequest(Constants.UserNameA, Constants.UserPasswordA, rememberMe, ReturnUrl);
        }

        private LoginInteractor Sut
        {
            get
            {
                return new LoginInteractor(
                    Repos.User,
                    Services.Auth,
                    Repos.Bunch,
                    Repos.Player);
            }
        }
    }
}
