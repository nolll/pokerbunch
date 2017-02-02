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
            var result = Execute();
            Assert.AreEqual(LoginName, result.UserName);
            Assert.AreEqual(Token, result.Token);
        }
    }
}
