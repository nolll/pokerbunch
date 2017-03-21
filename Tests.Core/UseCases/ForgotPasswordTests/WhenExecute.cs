using NUnit.Framework;

namespace Tests.Core.UseCases.ForgotPasswordTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void CallsResetPassword()
        {
            Assert.AreEqual(Email, PostedEmail);
        }
    }
}
