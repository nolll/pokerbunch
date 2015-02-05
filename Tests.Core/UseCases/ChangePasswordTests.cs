using Core.Exceptions;
using Core.UseCases.ChangePassword;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class ChangePasswordTests : TestBase
    {
        [Test]
        public void ChangePassword_EmptyPassword_ThrowsValidationException()
        {
            var request = new ChangePasswordRequest(Constants.UserIdA, "", "");

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void ChangePassword_DifferentPasswords_ThrowsValidationException()
        {
            var request = new ChangePasswordRequest(Constants.UserIdA, "a", "b");

            Assert.Throws<ValidationException>(() => Sut.Execute(request));
        }

        [Test]
        public void ChangePassword_EqualPasswords_SavesUserWithNewPassword()
        {
            var request = new ChangePasswordRequest(Constants.UserIdA, "a", "a");
            Sut.Execute(request);

            Assert.AreNotEqual(Constants.UserEncryptedPasswordA, Repos.User.Saved.EncryptedPassword);
        }
        
        [Test]
        public void ChangePassword_EqualPasswords_ReturnUrlIsSet()
        {
            var request = new ChangePasswordRequest(Constants.UserIdA, "a", "a");
            var result = Sut.Execute(request);

            Assert.AreEqual("/-/user/changedpassword", result.ReturnUrl.Relative);
        }

        private ChangePasswordInteractor Sut
        {
            get
            {
                return new ChangePasswordInteractor(
                    Repos.User,
                    Services.RandomService);
            }
        }
    }
}