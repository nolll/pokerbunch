using NUnit.Framework;

namespace Tests.Core.UseCases.EditUserTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void UserNameIsSet()
        {
            Assert.AreEqual(UserName, Result.UserName);
        }

        [Test]
        public void UserIsSaved()
        {
            Assert.AreEqual(UserName, PostedUser.UserName);
            Assert.AreEqual(ChangedRealName, PostedUser.RealName);
            Assert.AreEqual(ChangedDisplayName, PostedUser.DisplayName);
            Assert.AreEqual(ChangedEmail, PostedUser.Email);
        }
    }
}