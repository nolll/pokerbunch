using Core.Entities;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.PlayerDetailsTests
{
    public class WithPlayerThatIsUser : Arrange
    {
        protected override Role Role => Role.Player;
        protected override string PlayerId => IdForPlayerThatIsUser;

        [Test]
        public void PlayerDetails_WithUser_AvatarUrlIsSet()
        {
            const string expected = "http://www.gravatar.com/avatar/111d68d06e2d317b5a59c2c6c5bad808?s=100";
            Assert.AreEqual(expected, Result.AvatarUrl);
        }

        [Test]
        public void PlayerDetails_WithUser_UserNameIsSet()
        {
            Assert.AreEqual(UserData.UserName1, Result.UserName);
        }

        [Test]
        public void PlayerDetails_WithUser_IsUserIsTrue()
        {
            Assert.IsTrue(Result.IsUser);
        }
    }
}