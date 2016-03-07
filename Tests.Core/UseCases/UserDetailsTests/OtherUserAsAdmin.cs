using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases.UserDetailsTests
{
    public class OtherUserAsAdmin : Arrange
    {
        [Test]
        public void UserNameIsSet()
        {
            Assert.AreEqual(ViewUserName, Result.UserName);
        }

        [Test]
        public void UserDetails_DisplayNameIsSet()
        {
            Assert.AreEqual(TestData.UserA.DisplayName, Result.DisplayName);
        }

        [Test]
        public void UserDetails_RealNameIsSet()
        {
            Assert.AreEqual(TestData.UserA.RealName, Result.RealName);
        }

        [Test]
        public void UserDetails_EmailIsSet()
        {
            Assert.AreEqual(TestData.UserA.Email, Result.Email);
        }

        [Test]
        public void UserDetails_ViewingOtherUser_CanEditIsFalse()
        {
            Assert.IsFalse(Result.CanEdit);
        }

        [Test]
        public void UserDetails_AdminUser_CanEditIsTrue()
        {
            Assert.IsTrue(Result.CanEdit);
        }

        [Test]
        public void UserDetails_ViewingOwnUser_CanEditIsTrue()
        {
            Assert.IsTrue(Result.CanEdit);
        }

        [Test]
        public void UserDetails_ViewingOtherUser_CanChangePasswordIsFalse()
        {
            Assert.IsFalse(Result.CanChangePassword);
        }

        [Test]
        public void UserDetails_ViewingOwnUser_CanChangePasswordIsTrue()
        {
            Assert.IsTrue(Result.CanChangePassword);
        }

        [Test]
        public void UserDetails_EditUrlIsCorrectType()
        {
            Assert.AreEqual("user-name-a", Result.UserName);
        }

        [Test]
        public void UserDetails_AvatarUrlIsSet()
        {
            Assert.AreEqual("http://www.gravatar.com/avatar/0796c9df772de3f82c0c89377330471b?s=100", Result.AvatarUrl);
        }
    }
}
