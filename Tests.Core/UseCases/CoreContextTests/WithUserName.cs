using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CoreContextTests
{
    public class WithUserName : Arrange
    {
        protected override string UserName => UserData.UserName1;

        [Test]
        public void AppContext_WithUserName_LoggedInPropertiesAreSet()
        {
            Assert.IsTrue(Result.IsLoggedIn);
            Assert.AreEqual(DisplayName, Result.UserDisplayName);
            Assert.AreEqual(UserName, Result.UserName);
        }
    }
}