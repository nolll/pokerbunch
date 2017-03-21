using NUnit.Framework;

namespace Tests.Core.UseCases.ChangePasswordTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void ChangePassword_EqualPasswords_SavesUserWithNewPassword()
        {
            Assert.AreEqual(OldPassword, PostedOldPassword);
            Assert.AreEqual(NewPassword, PostedNewPassword);
            Assert.AreEqual(Repeat, PostedRepeat);
        }
    }
}