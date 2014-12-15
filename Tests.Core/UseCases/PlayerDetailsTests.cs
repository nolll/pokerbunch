using Core.Entities;
using Core.Repositories;
using Core.Urls;
using Core.UseCases.PlayerDetails;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class PlayerDetailsTests : TestBase
    {
        [Test]
        public void PlayerDetails_DisplayNameIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.AreEqual(Constants.PlayerNameA, result.DisplayName);
        }

        [Test]
        public void PlayerDetails_DeleteUrlIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<DeletePlayerUrl>(result.DeleteUrl);
        }

        [Test]
        public void PlayerDetails_InvitationUrlIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<InvitePlayerUrl>(result.InvitationUrl);
        }

        [Test]
        public void PlayerDetails_WithoutUser_AvatarUrlIsEmpty()
        {
            var result = Execute(CreateRequest(Constants.PlayerIdB));

            Assert.AreEqual("", result.AvatarUrl);
        }

        [Test]
        public void PlayerDetails_WithUser_AvatarUrlIsSet()
        {
            var result = Execute(CreateRequest());

            const string expected = "http://www.gravatar.com/avatar/0796c9df772de3f82c0c89377330471b?s=100";
            Assert.AreEqual(expected, result.AvatarUrl);
        }

        [Test]
        public void PlayerDetails_WithoutUser_UserUrlIsEmpty()
        {
            var result = Execute(CreateRequest(Constants.PlayerIdB));

            Assert.IsInstanceOf<EmptyUrl>(result.UserUrl);
        }

        [Test]
        public void PlayerDetails_WithUser_UserUrlIsSet()
        {
            var result = Execute(CreateRequest());

            Assert.IsInstanceOf<UserUrl>(result.UserUrl);
        }

        [Test]
        public void PlayerDetails_WithoutUser_IsUserIsFalse()
        {
            var result = Execute(CreateRequest(Constants.PlayerIdB));

            Assert.IsFalse(result.IsUser);
        }

        [Test]
        public void PlayerDetails_WithUser_IsUserIsTrue()
        {
            var result = Execute(CreateRequest());

            Assert.IsTrue(result.IsUser);
        }

        [Test]
        public void PlayerDetails_WithNormalUser_CanDeleteIsFalse()
        {
            var result = Execute(CreateRequest());

            Assert.IsFalse(result.CanDelete);
        }

        [Test]
        public void PlayerDetails_WithManagerAndPlayerHasNotPlayedGames_CanDeleteIsTrue()
        {
            Services.Auth.SetCurrentRole(Role.Manager);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.CanDelete);
        }

        [Test]
        public void PlayerDetails_WithManagerAndPlayerHasPlayedGames_CanDeleteIsFalse()
        {
            Services.Auth.SetCurrentRole(Role.Manager);
            SetupPlayedCashgames();

            var result = Execute(CreateRequest());

            Assert.IsFalse(result.CanDelete);
        }

        private static PlayerDetailsRequest CreateRequest(int playerId = Constants.PlayerIdA)
        {
            return new PlayerDetailsRequest(Constants.SlugA, playerId);
        }

        private void SetupPlayedCashgames()
        {
            GetMock<ICashgameRepository>().Setup(o => o.HasPlayed(Constants.PlayerIdA)).Returns(true);
        }
        
        private PlayerDetailsResult Execute(PlayerDetailsRequest request)
        {
            return PlayerDetailsInteractor.Execute(
                Services.Auth,
                Repos.Bunch,
                Repos.Player,
                GetMock<ICashgameRepository>().Object,
                Repos.User,
                request);
        }
    }
}
