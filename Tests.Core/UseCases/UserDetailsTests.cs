using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using Core.UseCases.UserDetails;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
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
            return A.User
                    .WithUserName(UserName)
                    .WithDisplayName(DisplayName)
                    .WithRealName(RealName)
                    .WithEmail(Email)
                    .Build();
        }

        private void SetupCurrentUser()
        {
            Services.Auth.CurrentUser = A.User.WithGlobalRole(Role.Player).Build();
        }

        private void SetupCurrentUserAsAdmin()
        {
            Services.Auth.CurrentUser = A.User.WithGlobalRole(Role.Admin).Build();
        }

        private void SetupCurrentUserAsDisplayUser()
        {
            Services.Auth.CurrentUser = CreateDisplayUser();
        }
        
        private UserDetailsResult Execute(UserDetailsRequest request)
        {
            return UserDetailsInteractor.Execute(
                Services.Auth,
                GetMock<IUserRepository>().Object,
                request);
        }
    }
}
