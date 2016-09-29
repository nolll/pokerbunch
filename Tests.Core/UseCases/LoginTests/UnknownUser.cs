using Core.Exceptions;
using NUnit.Framework;

namespace Tests.Core.UseCases.LoginTests
{
    public class UnknownUser : Arrange
    {
        protected override string LoginName => UnknownUser;

        [Test]
        public void ThrowsException()
        {
            Assert.Throws<LoginException>(() => Execute());
        }
    }
}
