using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using Core.UseCases.UserDetails;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class UserDetailsTests : TestBase
    {
        private const string UserName = "a";
        private const string DisplayName = "b";
        private const string RealName = "c";
        private const string Email = "d";

        [Test]
        public void UserDetails_UserNameIsSet()
        {
            SetupDisplayUser();
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            Assert.AreEqual(UserName, result.UserName);
        }

        [Test]
        public void UserDetails_DisplayNameIsSet()
        {
            SetupDisplayUser();
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            Assert.AreEqual(DisplayName, result.DisplayName);
        }

        [Test]
        public void UserDetails_RealNameIsSet()
        {
            SetupDisplayUser();
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            Assert.AreEqual(RealName, result.RealName);
        }

        [Test]
        public void UserDetails_EmailIsSet()
        {
            SetupDisplayUser();
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            Assert.AreEqual(Email, result.Email);
        }

        [Test]
        public void UserDetails_NormalUser_CanEditIsFalse()
        {
            SetupDisplayUser();
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            Assert.IsFalse(result.CanEdit);
        }

        [Test]
        public void UserDetails_AdminUser_CanEditIsTrue()
        {
            SetupDisplayUser();
            SetupCurrentUserAsAdmin();

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.CanEdit);
        }

        [Test]
        public void UserDetails_ViewingOwnUser_CanEditIsTrue()
        {
            SetupDisplayUser();
            SetupCurrentUserAsDisplayUser();

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.CanEdit);
        }

        [Test]
        public void UserDetails_ViewingOtherUser_CanChangePasswordIsFalse()
        {
            SetupDisplayUser();
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            Assert.IsFalse(result.CanChangePassword);
        }

        [Test]
        public void UserDetails_ViewingOwnUser_CanChangePasswordIsTrue()
        {
            SetupDisplayUser();
            SetupCurrentUserAsDisplayUser();

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.CanChangePassword);
        }

        [Test]
        public void UserDetails_EditUrlIsCorrectType()
        {
            SetupDisplayUser();
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<EditUserUrl>(result.EditUrl);
        }

        [Test]
        public void UserDetails_ChangePasswordUrlIsCorrectType()
        {
            SetupDisplayUser();
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<ChangePasswordUrl>(result.ChangePasswordUrl);
        }

        [Test]
        public void UserDetails_AvatarUrlIsSet()
        {
            SetupDisplayUser();
            SetupCurrentUser();

            var result = Execute(CreateRequest());

            const string expected = "http://www.gravatar.com/avatar/8277e0910d750195b448797616e091ad?s=100";
            Assert.AreEqual(expected, result.AvatarUrl);
        }

        private static UserDetailsRequest CreateRequest()
        {
            return new UserDetailsRequest(UserName);
        }

        private void SetupDisplayUser()
        {
            GetMock<IUserRepository>().Setup(o => o.GetByNameOrEmail(UserName)).Returns(CreateDisplayUser);
        }

        private User CreateDisplayUser()
        {
            return new UserInTest(userName: UserName, displayName: DisplayName, realName: RealName, email: Email);
        }

        private void SetupCurrentUser()
        {
            SetupCurrentUser(Role.Player);
        }

        private void SetupCurrentUserAsAdmin()
        {
            SetupCurrentUser(Role.Admin);
        }

        private void SetupCurrentUserAsDisplayUser()
        {
            SetupCurrentUser(Role.Admin, CreateDisplayUser());
        }

        private void SetupCurrentUser(Role role, User displayUser = null)
        {
            var currentUser = displayUser ?? new UserInTest(globalRole: role);
            GetMock<IAuth>().Setup(o => o.CurrentUser).Returns(currentUser);
        }

        private UserDetailsResult Execute(UserDetailsRequest request)
        {
            return UserDetailsInteractor.Execute(
                GetMock<IAuth>().Object,
                GetMock<IUserRepository>().Object,
                request);
        }
    }
}
