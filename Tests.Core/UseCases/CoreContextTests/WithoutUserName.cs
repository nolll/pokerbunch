using NUnit.Framework;

namespace Tests.Core.UseCases.CoreContextTests
{
    public class WithoutUserName : Arrange
    {
        protected override string UserName => null;

        [Test]
        public void AppContext_WithoutUserName_AllPropertiesAreSet()
        {
            Assert.IsFalse(Result.IsLoggedIn);
            Assert.IsEmpty(Result.UserDisplayName);
        }
    }
}