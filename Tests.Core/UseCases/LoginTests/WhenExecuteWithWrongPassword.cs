using Core.Exceptions;
using NUnit.Framework;

namespace Tests.Core.UseCases.LoginTests
{
    public class WhenExecuteWithWrongPassword : Arrange
    {
        protected override bool ExecuteAutomatically => false;
        protected override string LoginName => ExistingUser;
        protected override string Password => WrongPassword;

        [Test]
        public void ThrowsException()
        {
            Assert.Throws<LoginException>(Execute);
        }
    }
}
