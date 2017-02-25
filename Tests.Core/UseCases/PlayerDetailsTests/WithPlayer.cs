using Core.Entities;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.PlayerDetailsTests
{
    public class WithPlayer : Arrange
    {
        protected override Role Role => Role.Player;
        protected override string PlayerId => IdForPlayerThatIsNotUser;

        [Test]
        public void DisplayNameIsSet()
        {
            Assert.AreEqual(PlayerData.Name1, Result.DisplayName);
        }

        [Test]
        public void PlayerIdIsSet()
        {
            Assert.AreEqual(PlayerId, Result.PlayerId);
        }

        [Test]
        public void AvatarUrlIsEmpty()
        {
            Assert.AreEqual("", Result.AvatarUrl);
        }

        [Test]
        public void UserNameIsEmpty()
        {
            Assert.AreEqual(string.Empty, Result.UserName);
        }

        [Test]
        public void IsUserIsFalse()
        {
            Assert.IsFalse(Result.IsUser);
        }

        [Test]
        public void CanDeleteIsFalse()
        {
            Assert.IsFalse(Result.CanDelete);
        }
    }
}
