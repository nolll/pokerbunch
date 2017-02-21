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
            var result = Sut.Execute(Request);

            Assert.AreEqual(PlayerData.Name1, result.DisplayName);
        }

        [Test]
        public void PlayerIdIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(PlayerId, result.PlayerId);
        }

        [Test]
        public void AvatarUrlIsEmpty()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual("", result.AvatarUrl);
        }

        [Test]
        public void UserNameIsEmpty()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(string.Empty, result.UserName);
        }

        [Test]
        public void IsUserIsFalse()
        {
            var result = Sut.Execute(Request);

            Assert.IsFalse(result.IsUser);
        }

        [Test]
        public void CanDeleteIsFalse()
        {
            var result = Sut.Execute(Request);

            Assert.IsFalse(result.CanDelete);
        }
    }
}
