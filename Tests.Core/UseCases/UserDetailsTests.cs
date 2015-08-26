using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class UserDetailsTests : TestBase
    {
        [Test]
        public void UserDetails_UserNameIsSet()
        {
            var result = Sut.Execute(new UserDetails.Request(TestData.UserA.UserName, TestData.UserA.UserName));

            Assert.AreEqual(TestData.UserA.UserName, result.UserName);
        }

        [Test]
        public void UserDetails_DisplayNameIsSet()
        {
            var result = Sut.Execute(new UserDetails.Request(TestData.UserA.UserName, TestData.UserA.UserName));

            Assert.AreEqual(TestData.UserA.DisplayName, result.DisplayName);
        }

        [Test]
        public void UserDetails_RealNameIsSet()
        {
            var result = Sut.Execute(new UserDetails.Request(TestData.UserA.UserName, TestData.UserA.UserName));

            Assert.AreEqual(TestData.UserA.RealName, result.RealName);
        }

        [Test]
        public void UserDetails_EmailIsSet()
        {
            var result = Sut.Execute(new UserDetails.Request(TestData.UserA.UserName, TestData.UserA.UserName));

            Assert.AreEqual(TestData.UserA.Email, result.Email);
        }

        [Test]
        public void UserDetails_ViewingOtherUser_CanEditIsFalse()
        {
            var result = Sut.Execute(new UserDetails.Request(TestData.UserA.UserName, TestData.UserC.UserName));

            Assert.IsFalse(result.CanEdit);
        }

        [Test]
        public void UserDetails_AdminUser_CanEditIsTrue()
        {
            var result = Sut.Execute(new UserDetails.Request(TestData.AdminUser.UserName, TestData.UserC.UserName));

            Assert.IsTrue(result.CanEdit);
        }

        [Test]
        public void UserDetails_ViewingOwnUser_CanEditIsTrue()
        {
            var result = Sut.Execute(new UserDetails.Request(TestData.UserA.UserName, TestData.UserA.UserName));

            Assert.IsTrue(result.CanEdit);
        }

        [Test]
        public void UserDetails_ViewingOtherUser_CanChangePasswordIsFalse()
        {
            var result = Sut.Execute(new UserDetails.Request(TestData.UserA.UserName, TestData.UserC.UserName));

            Assert.IsFalse(result.CanChangePassword);
        }

        [Test]
        public void UserDetails_ViewingOwnUser_CanChangePasswordIsTrue()
        {
            var result = Sut.Execute(new UserDetails.Request(TestData.UserA.UserName, TestData.UserA.UserName));

            Assert.IsTrue(result.CanChangePassword);
        }

        [Test]
        public void UserDetails_EditUrlIsCorrectType()
        {
            var result = Sut.Execute(new UserDetails.Request(TestData.UserA.UserName, TestData.UserA.UserName));

            Assert.AreEqual("user-name-a", result.UserName);
        }

        [Test]
        public void UserDetails_AvatarUrlIsSet()
        {
            var result = Sut.Execute(new UserDetails.Request(TestData.UserA.UserName, TestData.UserA.UserName));

            const string expected = "http://www.gravatar.com/avatar/0796c9df772de3f82c0c89377330471b?s=100";
            Assert.AreEqual(expected, result.AvatarUrl);
        }
        
        private UserDetails Sut
        {
            get { return new UserDetails(Repos.User); }
        }
    }
}
