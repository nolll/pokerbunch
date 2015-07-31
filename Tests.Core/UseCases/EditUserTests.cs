using Core.Exceptions;
using Core.UseCases.EditUser;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class EditUserTests : TestBase
    {
        private const string ChangedDisplayName = "changeddisplayname";
        private const string RealName = "realname";
        private const string ChangedEmail = "email@example.com";

        [Test]
        public void EditUser_EmptyDisplayName_ThrowsException()
        {
            var request = new EditUserRequest(TestData.UserNameA, "", RealName, ChangedEmail);

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void EditUser_EmptyEmail_ThrowsException()
        {
            var request = new EditUserRequest(TestData.UserNameA, ChangedDisplayName, RealName, "");

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void EditUser_InvalidEmail_ThrowsException()
        {
            var request = new EditUserRequest(TestData.UserNameA, ChangedDisplayName, RealName, "a");

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void EditUser_ValidInput_ReturnUrlIsSet()
        {
            var request = new EditUserRequest(TestData.UserNameA, ChangedDisplayName, RealName, ChangedEmail);

            var result = Sut.Execute(request);

            Assert.AreEqual("/-/user/details/user-name-a", result.ReturnUrl.Relative);
        }

        [Test]
        public void EditUser_ValidInput_UserIsSaved()
        {
            var request = new EditUserRequest(TestData.UserNameA, ChangedDisplayName, RealName, ChangedEmail);

            Sut.Execute(request);

            Assert.AreEqual(TestData.UserNameA, Repos.User.Saved.UserName);
            Assert.AreEqual(ChangedDisplayName, Repos.User.Saved.DisplayName);
            Assert.AreEqual(ChangedEmail, Repos.User.Saved.Email);
        }

        private EditUserInteractor Sut
        {
            get
            {
                return new EditUserInteractor(Repos.User);
            }
        }
    }
}