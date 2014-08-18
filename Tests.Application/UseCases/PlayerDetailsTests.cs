using Application.Services;
using Application.Urls;
using Application.UseCases.PlayerDetails;
using Core.Entities;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class PlayerDetailsTests : MockContainer
    {
        private const int PlayerId = 1;
        private const int UserId = 2;
        private const string Slug = "a";
        private const string DisplayName = "b";
        private const string Email = "c";

        [Test]
        public void PlayerDetails_DisplayNameIsSet()
        {
            SetupHomegame();
            SetupPlayer();

            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual("b", result.DisplayName);
        }

        [Test]
        public void PlayerDetails_DeleteUrlIsSet()
        {
            SetupHomegame();
            SetupPlayer();

            var result = Sut.Execute(CreateRequest());

            Assert.IsInstanceOf<DeletePlayerUrl>(result.DeleteUrl);
        }

        [Test]
        public void PlayerDetails_InvitationUrlIsSet()
        {
            SetupHomegame();
            SetupPlayer();

            var result = Sut.Execute(CreateRequest());

            Assert.IsInstanceOf<InvitePlayerUrl>(result.InvitationUrl);
        }

        [Test]
        public void PlayerDetails_WithoutUser_AvatarUrlIsEmpty()
        {
            SetupHomegame();
            SetupPlayer();

            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual("", result.AvatarUrl);
        }

        [Test]
        public void PlayerDetails_WithUser_AvatarUrlIsSet()
        {
            SetupHomegame();
            SetupPlayerAndUser();

            var result = Sut.Execute(CreateRequest());

            const string expected = "http://www.gravatar.com/avatar/4a8a08f09d37b73795649038408b5f33?s=100";
            Assert.AreEqual(expected, result.AvatarUrl);
        }

        [Test]
        public void PlayerDetails_WithoutUser_UserUrlIsEmpty()
        {
            SetupHomegame();
            SetupPlayer();

            var result = Sut.Execute(CreateRequest());

            Assert.IsInstanceOf<EmptyUrl>(result.UserUrl);
        }

        [Test]
        public void PlayerDetails_WithUser_UserUrlIsSet()
        {
            SetupHomegame();
            SetupPlayerAndUser();

            var result = Sut.Execute(CreateRequest());

            Assert.IsInstanceOf<UserUrl>(result.UserUrl);
        }

        [Test]
        public void PlayerDetails_WithoutUser_IsUserIsFalse()
        {
            SetupHomegame();
            SetupPlayer();

            var result = Sut.Execute(CreateRequest());

            Assert.IsFalse(result.IsUser);
        }

        [Test]
        public void PlayerDetails_WithUser_IsUserIsTrue()
        {
            SetupHomegame();
            SetupPlayerAndUser();

            var result = Sut.Execute(CreateRequest());

            Assert.IsTrue(result.IsUser);
        }

        [Test]
        public void PlayerDetails_WithNormalUser_CanDeleteIsFalse()
        {
            SetupHomegame();
            SetupPlayerAndUser();

            var result = Sut.Execute(CreateRequest());

            Assert.IsFalse(result.CanDelete);
        }

        [Test]
        public void PlayerDetails_WithManagerAndPlayerHasNotPlayedGames_CanDeleteIsTrue()
        {
            SetupHomegame();
            SetupPlayerAndUser();
            SetupManager();

            var result = Sut.Execute(CreateRequest());

            Assert.IsTrue(result.CanDelete);
        }

        [Test]
        public void PlayerDetails_WithManagerAndPlayerHasPlayedGames_CanDeleteIsFalse()
        {
            SetupHomegame();
            SetupPlayerAndUser();
            SetupManager();
            SetupPlayedCashgames();

            var result = Sut.Execute(CreateRequest());

            Assert.IsFalse(result.CanDelete);
        }

        private static PlayerDetailsRequest CreateRequest()
        {
            return new PlayerDetailsRequest(Slug, PlayerId);
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
            return new PlayerInTest(displayName: DisplayName, userId: userId);
        }

        private void SetupManager()
        {
            GetMock<IAuth>().Setup(o => o.IsInRole(Slug, Role.Manager)).Returns(true);
        }

        private void SetupPlayer(Player player)
        {
            GetMock<IPlayerRepository>().Setup(o => o.GetById(PlayerId)).Returns(player);
        }

        private void SetupHomegame()
        {
            var homegame = new HomegameInTest();
            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(Slug)).Returns(homegame);
        }

        private void SetupUser(Role role = Role.Player)
        {
            var user = new UserInTest(email: Email, globalRole: role);
            GetMock<IUserRepository>().Setup(o => o.GetById(UserId)).Returns(user);
        }

        private void SetupPlayedCashgames()
        {
            GetMock<ICashgameRepository>().Setup(o => o.HasPlayed(PlayerId)).Returns(true);
        }

        private PlayerDetailsInteractor Sut
        {
            get
            {
                return new PlayerDetailsInteractor(
                    GetMock<IAuth>().Object,
                    GetMock<IHomegameRepository>().Object,
                    GetMock<IPlayerRepository>().Object,
                    GetMock<ICashgameRepository>().Object,
                    GetMock<IUserRepository>().Object);
            }
        }
    }
}
