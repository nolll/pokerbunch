using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.PlayerDetailsTests
{
    public abstract class Arrange
    {
        protected PlayerDetails Sut;

        protected abstract Role Role { get; }

        [SetUp]
        public void Setup()
        {
            var brm = new Mock<IBunchRepository>();
            brm.Setup(o => o.Get(BunchData.Id1))
                .Returns(BunchData.Bunch(Role.Player));

            var prm = new Mock<IPlayerRepository>();
            prm.Setup(o => o.Get(PlayerData.Id1))
                .Returns(new Player(BunchData.Id1, PlayerData.Id1, null, PlayerData.Name1, Role, PlayerData.Color1));

            var cashgames = CashgameData.TwoGamesOnSameYearWithTwoPlayers;
            var crm = new Mock<ICashgameRepository>();
            crm.Setup(o => o.PlayerList(PlayerData.Id1))
                .Returns(cashgames);

            var urm = new Mock<IUserRepository>();
            
            Sut = new PlayerDetails(brm.Object, prm.Object, crm.Object, urm.Object);
        }

        protected PlayerDetails.Request Request => new PlayerDetails.Request(PlayerData.Id1);
    }

    public class WithPlayerThatIsUser : Arrange
    {
        [Test]
        public void PlayerDetails_WithUser_AvatarUrlIsSet()
        {
            var result = Sut.Execute(Request);

            const string expected = "http://www.gravatar.com/avatar/0796c9df772de3f82c0c89377330471b?s=100";
            Assert.AreEqual(expected, result.AvatarUrl);
        }
    }

    public class WithPlayerThatIsNotUser : Arrange
    {
        protected override Role Role => Role.Player;

        [Test]
        public void PlayerDetails_DisplayNameIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(PlayerData.Name1, result.DisplayName);
        }

        [Test]
        public void PlayerDetails_PlayerIdIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(PlayerData.Id1, result.PlayerId);
        }

        [Test]
        public void PlayerDetails_WithoutUser_AvatarUrlIsEmpty()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual("", result.AvatarUrl);
        }

        [Test]
        public void PlayerDetails_WithoutUser_UserUrlIsEmpty()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(string.Empty, result.UserName);
        }

        [Test]
        public void PlayerDetails_WithUser_UserNameIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual("user-name-a", result.UserName);
        }

        [Test]
        public void PlayerDetails_WithoutUser_IsUserIsFalse()
        {
            var result = Sut.Execute(Request);

            Assert.IsFalse(result.IsUser);
        }

        [Test]
        public void PlayerDetails_WithUser_IsUserIsTrue()
        {
            var result = Sut.Execute(Request);

            Assert.IsTrue(result.IsUser);
        }

        [Test]
        public void PlayerDetails_WithNormalUser_CanDeleteIsFalse()
        {
            var result = Sut.Execute(Request);

            Assert.IsFalse(result.CanDelete);
        }

        [Test]
        public void PlayerDetails_WithManagerAndPlayerHasNotPlayedGames_CanDeleteIsTrue()
        {
            var result = Sut.Execute(Request);

            Assert.IsTrue(result.CanDelete);
        }

        [Test]
        public void PlayerDetails_WithManagerAndPlayerHasPlayedGames_CanDeleteIsFalse()
        {
            var result = Sut.Execute(Request);

            Assert.IsFalse(result.CanDelete);
        }
    }

    public class WithPlayerThatIsNotUser : Arrange
    {
        
    }
}
