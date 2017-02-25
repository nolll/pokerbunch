using NUnit.Framework;

namespace Tests.Core.UseCases.LoginTests
{
    public class WhenExecuteWithCorrectPassword : Arrange
    {
        protected override string LoginName => ExistingUser;
        protected override string Password => CorrectPassword;

        [Test]
        public void ThrowsException()
        {
            Assert.AreEqual(LoginName, Result.UserName);
            Assert.AreEqual(Token, Result.Token);
        }
    }
}
