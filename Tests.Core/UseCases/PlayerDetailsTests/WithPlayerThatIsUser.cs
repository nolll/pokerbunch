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
            var result = Sut.Execute(Request);

            const string expected = "http://www.gravatar.com/avatar/111d68d06e2d317b5a59c2c6c5bad808?s=100";
            Assert.AreEqual(expected, result.AvatarUrl);
        }

        [Test]
        public void PlayerDetails_WithUser_UserNameIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(UserData.UserName1, result.UserName);
        }

        [Test]
        public void PlayerDetails_WithUser_IsUserIsTrue()
        {
            var result = Sut.Execute(Request);

            Assert.IsTrue(result.IsUser);
        }
    }
}