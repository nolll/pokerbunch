using NUnit.Framework;

namespace Tests.Core.UseCases.EditUserFormTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void UserNameIsSet()
        {
            Assert.AreEqual(UserName, Result.UserName);
        }

        [Test]
        public void RealNameIsSet()
        {
            Assert.AreEqual(RealName, Result.RealName);
        }

        [Test]
        public void DisplayNameIsSet()
        {
            Assert.AreEqual(DisplayName, Result.DisplayName);
        }

        [Test]
        public void EmailIsSet()
        {
            Assert.AreEqual(Email, Result.Email);
        }
    }
}
