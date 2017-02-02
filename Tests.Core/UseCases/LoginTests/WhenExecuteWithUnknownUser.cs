using Core.Exceptions;
using NUnit.Framework;

namespace Tests.Core.UseCases.LoginTests
{
    public class WhenExecuteWithUnknownUser : Arrange
    {
        protected override string LoginName => UnknownUser;

        [Test]
        public void ThrowsException()
        {
            Assert.Throws<LoginException>(() => Execute());
        }
    }
}
