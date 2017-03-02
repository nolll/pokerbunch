using NUnit.Framework;

namespace Tests.Core.UseCases.LoginFormTests
{
    public class WithoutReturnUrl : Arrange
    {
        [Test]
        public void ReturnUrlIsHomeUrl()
        {
            Assert.AreEqual(HomeUrl, Result.ReturnUrl);
        }
    }
}