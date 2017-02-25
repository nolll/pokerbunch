using System.Linq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.UserListTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void ReturnsListOfUserItems()
        {
            Assert.AreEqual(2, Result.Users.Count);
            Assert.AreEqual(UserData.DisplayName1, Result.Users.First().DisplayName);
            Assert.AreEqual(UserData.UserName1, Result.Users.First().UserName);
        }
    }
}
