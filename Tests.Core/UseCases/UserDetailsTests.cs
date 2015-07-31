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
            var result = Sut.Execute(new UserDetailsRequest(Constants.UserNameA, Constants.UserNameA));

            Assert.AreEqual(Constants.UserNameA, result.UserName);
        }

        [Test]
        public void UserDetails_DisplayNameIsSet()
        {
            var result = Sut.Execute(new UserDetailsRequest(Constants.UserNameA, Constants.UserNameA));

            Assert.AreEqual(Constants.UserDisplayNameA, result.DisplayName);
        }

        [Test]
        public void UserDetails_RealNameIsSet()
        {
            var result = Sut.Execute(new UserDetailsRequest(Constants.UserNameA, Constants.UserNameA));

            Assert.AreEqual(Constants.UserRealNameA, result.RealName);
        }

        [Test]
        public void UserDetails_EmailIsSet()
        {
            var result = Sut.Execute(new UserDetailsRequest(Constants.UserNameA, Constants.UserNameA));

            Assert.AreEqual(Constants.UserEmailA, result.Email);
        }

        [Test]
        public void UserDetails_ViewingOtherUser_CanEditIsFalse()
        {
            var result = Sut.Execute(new UserDetailsRequest(Constants.UserNameA, Constants.UserNameC));

            Assert.IsFalse(result.CanEdit);
        }

        [Test]
        public void UserDetails_AdminUser_CanEditIsTrue()
        {
            var result = Sut.Execute(new UserDetailsRequest(Constants.AdminUser.UserName, Constants.UserC.UserName));

            Assert.IsTrue(result.CanEdit);
        }

        [Test]
        public void UserDetails_ViewingOwnUser_CanEditIsTrue()
        {
            var result = Sut.Execute(new UserDetailsRequest(Constants.UserNameA, Constants.UserNameA));

            Assert.IsTrue(result.CanEdit);
        }

        [Test]
        public void UserDetails_ViewingOtherUser_CanChangePasswordIsFalse()
        {
            var result = Sut.Execute(new UserDetailsRequest(Constants.UserNameA, Constants.UserNameC));

            Assert.IsFalse(result.CanChangePassword);
        }

        [Test]
        public void UserDetails_ViewingOwnUser_CanChangePasswordIsTrue()
        {
            var result = Sut.Execute(new UserDetailsRequest(Constants.UserNameA, Constants.UserNameA));

            Assert.IsTrue(result.CanChangePassword);
        }

        [Test]
        public void UserDetails_EditUrlIsCorrectType()
        {
            var result = Sut.Execute(new UserDetailsRequest(Constants.UserNameA, Constants.UserNameA));

            Assert.IsInstanceOf<EditUserUrl>(result.EditUrl);
        }

        [Test]
        public void UserDetails_ChangePasswordUrlIsCorrectType()
        {
            var result = Sut.Execute(new UserDetailsRequest(Constants.UserNameA, Constants.UserNameA));

            Assert.IsInstanceOf<ChangePasswordUrl>(result.ChangePasswordUrl);
        }

        [Test]
        public void UserDetails_AvatarUrlIsSet()
        {
            var result = Sut.Execute(new UserDetailsRequest(Constants.UserNameA, Constants.UserNameA));

            const string expected = "http://www.gravatar.com/avatar/0796c9df772de3f82c0c89377330471b?s=100";
            Assert.AreEqual(expected, result.AvatarUrl);
        }
        
        private UserDetailsInteractor Sut
        {
            get { return new UserDetailsInteractor(Repos.User); }
        }
    }
}
