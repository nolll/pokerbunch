using Core.Exceptions;
using NUnit.Framework;

namespace Tests.Core.UseCases.LoginTests
{
    public class WithWrongPassword : Arrange
    {
        protected override string LoginName => ExistingUser;
        protected override string Password => WrongPassword;

        [Test]
        public void ThrowsException()
        {
            Assert.Throws<LoginException>(() => Execute());
        }
    }
}
