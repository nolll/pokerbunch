using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using Core.UseCases.PlayerDetails;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class PlayerDetailsTests : TestBase
    {
        private const int PlayerId = 1;
        private const int UserId = 2;
        private const string DisplayName = "b";
        private const string Email = "c";

        [Test]
        public void PlayerDetails_DisplayNameIsSet()
        {
            SetupPlayer();

            var result = Execute(CreateRequest());

            Assert.AreEqual("b", result.DisplayName);
        }

        [Test]
        public void PlayerDetails_DeleteUrlIsSet()
        {
            SetupPlayer();

            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<DeletePlayerUrl>(result.DeleteUrl);
        }

        [Test]
        public void PlayerDetails_InvitationUrlIsSet()
        {
            SetupPlayer();

            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<InvitePlayerUrl>(result.InvitationUrl);
        }

        [Test]
        public void PlayerDetails_WithoutUser_AvatarUrlIsEmpty()
        {
            SetupPlayer();

            var result = Execute(CreateRequest());

            Assert.AreEqual("", result.AvatarUrl);
        }

        [Test]
        public void PlayerDetails_WithUser_AvatarUrlIsSet()
        {
            SetupPlayerAndUser();

            var result = Execute(CreateRequest());

            const string expected = "http://www.gravatar.com/avatar/4a8a08f09d37b73795649038408b5f33?s=100";
            Assert.AreEqual(expected, result.AvatarUrl);
        }

        [Test]
        public void PlayerDetails_WithoutUser_UserUrlIsEmpty()
        {
            SetupPlayer();

            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<EmptyUrl>(result.UserUrl);
        }

        [Test]
        public void PlayerDetails_WithUser_UserUrlIsSet()
        {
            SetupPlayerAndUser();

            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<UserUrl>(result.UserUrl);
        }

        [Test]
        public void PlayerDetails_WithoutUser_IsUserIsFalse()
        {
            SetupPlayer();

            var result = Execute(CreateRequest());

            Assert.IsFalse(result.IsUser);
        }

        [Test]
        public void PlayerDetails_WithUser_IsUserIsTrue()
        {
            SetupPlayerAndUser();

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.IsUser);
        }

        [Test]
        public void PlayerDetails_WithNormalUser_CanDeleteIsFalse()
        {
            SetupPlayerAndUser();

            var result = Execute(CreateRequest());

            Assert.IsFalse(result.CanDelete);
        }

        [Test]
        public void PlayerDetails_WithManagerAndPlayerHasNotPlayedGames_CanDeleteIsTrue()
        {
            SetupPlayerAndUser();
            SetupManager();

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.CanDelete);
        }

        [Test]
        public void PlayerDetails_WithManagerAndPlayerHasPlayedGames_CanDeleteIsFalse()
        {
            SetupPlayerAndUser();
            SetupManager();
            SetupPlayedCashgames();

            var result = Execute(CreateRequest());

            Assert.IsFalse(result.CanDelete);
        }

        private static PlayerDetailsRequest CreateRequest()
        {
            return new PlayerDetailsRequest(Constants.SlugA, PlayerId);
        }

        private void SetupPlayer()
        {
            SetupPlayer(CreatePlayer());
        }

        private void SetupPlayerAndUser()
        {
            SetupUser();
            SetupPlayer(CreatePlayer(UserId));
        }

        private Player CreatePlayer(int userId = 0)
        {
            return A.Player.WithDisplayName(DisplayName).WithUserId(userId).Build();
        }

        private void SetupManager()
        {
            GetMock<IAuth>().Setup(o => o.IsInRole(Constants.SlugA, Role.Manager)).Returns(true);
        }

        private void SetupPlayer(Player player)
        {
            GetMock<IPlayerRepository>().Setup(o => o.GetById(PlayerId)).Returns(player);
        }

        private void SetupUser(Role role = Role.Player)
        {
            var user = A.User.WithEmail(Email).WithGlobalRole(role).Build();
            GetMock<IUserRepository>().Setup(o => o.GetById(UserId)).Returns(user);
        }

        private void SetupPlayedCashgames()
        {
            GetMock<ICashgameRepository>().Setup(o => o.HasPlayed(PlayerId)).Returns(true);
        }
        
        private PlayerDetailsResult Execute(PlayerDetailsRequest request)
        {
            return PlayerDetailsInteractor.Execute(
                GetMock<IAuth>().Object,
                Repos.Bunch,
                GetMock<IPlayerRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                GetMock<IUserRepository>().Object,
                request);
        }
    }
}
