using Core;
using Core.Entities;
using Core.Urls;
using Core.UseCases.UserDetails;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class UserDetailsTests : TestBase
    {
        [Test]
        public void UserDetails_UserNameIsSet()
        {
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            Assert.AreEqual(Constants.UserNameA, result.UserName);
        }

        [Test]
        public void UserDetails_DisplayNameIsSet()
        {
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            Assert.AreEqual(Constants.UserDisplayNameA, result.DisplayName);
        }

        [Test]
        public void UserDetails_RealNameIsSet()
        {
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            Assert.AreEqual(Constants.UserRealNameA, result.RealName);
        }

        [Test]
        public void UserDetails_EmailIsSet()
        {
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            Assert.AreEqual(Constants.UserEmailA, result.Email);
        }

        [Test]
        public void UserDetails_ViewingOtherUser_CanEditIsFalse()
        {
            SetupCurrentUser();

            var result = Execute(CreateRequest(Constants.UserNameB));

            Assert.IsFalse(result.CanEdit);
        }

        [Test]
        public void UserDetails_AdminUser_CanEditIsTrue()
        {
            SetupCurrentUserAsAdmin();

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.CanEdit);
        }

        [Test]
        public void UserDetails_ViewingOwnUser_CanEditIsTrue()
        {
            SetupCurrentUserAsDisplayUser();

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.CanEdit);
        }

        [Test]
        public void UserDetails_ViewingOtherUser_CanChangePasswordIsFalse()
        {
            SetupCurrentUser();

            var result = Execute(CreateRequest(Constants.UserNameB));

            Assert.IsFalse(result.CanChangePassword);
        }

        [Test]
        public void UserDetails_ViewingOwnUser_CanChangePasswordIsTrue()
        {
            SetupCurrentUserAsDisplayUser();

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.CanChangePassword);
        }

        [Test]
        public void UserDetails_EditUrlIsCorrectType()
        {
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<EditUserUrl>(result.EditUrl);
        }

        [Test]
        public void UserDetails_ChangePasswordUrlIsCorrectType()
        {
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<ChangePasswordUrl>(result.ChangePasswordUrl);
        }

        [Test]
        public void UserDetails_AvatarUrlIsSet()
        {
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            const string expected = "http://www.gravatar.com/avatar/0796c9df772de3f82c0c89377330471b?s=100";
            Assert.AreEqual(expected, result.AvatarUrl);
        }

        private static UserDetailsRequest CreateRequest(string userName = Constants.UserNameA)
        {
            return new UserDetailsRequest(userName);
        }

        private void SetupCurrentUser()
        {
            Services.Auth.CurrentIdentity = new CustomIdentity(true, new UserIdentity { UserId = Constants.UserIdA, UserName = Constants.UserNameA, IsAdmin = false });
        }

        private void SetupCurrentUserAsAdmin()
        {
            Services.Auth.CurrentIdentity = new CustomIdentity(true, new UserIdentity { UserId = Constants.UserIdB, UserName = Constants.UserNameB, IsAdmin = true });
        }

        private void SetupCurrentUserAsDisplayUser()
        {
            Services.Auth.CurrentIdentity = new CustomIdentity(true, new UserIdentity { UserId = Constants.UserIdA, UserName = Constants.UserNameA, IsAdmin = false });
        }
        
        private UserDetailsResult Execute(UserDetailsRequest request)
        {
            return UserDetailsInteractor.Execute(
                Services.Auth,
                Repos.User,
                request);
        }
    }
}
