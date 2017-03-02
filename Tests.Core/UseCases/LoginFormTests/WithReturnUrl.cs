using NUnit.Framework;

namespace Tests.Core.UseCases.LoginFormTests
{
    public class WithReturnUrl : Arrange
    {
        protected override string ReturnUrl => ExistingReturnUrl;

        [Test]
        public void ReturnUrlIsSet()
        {
            Assert.AreEqual(ExistingReturnUrl, Result.ReturnUrl);
        }
    }
}
