using Core.Entities;
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
            var result = Sut.Execute(CreateRequest(Constants.PlayerIdA, Constants.UserNameA));

            Assert.AreEqual(Constants.PlayerNameA, result.DisplayName);
        }

        [Test]
        public void PlayerDetails_DeleteUrlIsSet()
        {
            var result = Sut.Execute(CreateRequest(Constants.PlayerIdA, Constants.UserNameA));

            Assert.IsInstanceOf<DeletePlayerUrl>(result.DeleteUrl);
        }

        [Test]
        public void PlayerDetails_InvitationUrlIsSet()
        {
            var result = Sut.Execute(CreateRequest(Constants.PlayerIdA, Constants.UserNameA));

            Assert.IsInstanceOf<InvitePlayerUrl>(result.InvitationUrl);
        }

        [Test]
        public void PlayerDetails_WithoutUser_AvatarUrlIsEmpty()
        {
            var result = Sut.Execute(CreateRequest(Constants.PlayerIdD, Constants.UserNameA));

            Assert.AreEqual("", result.AvatarUrl);
        }

        [Test]
        public void PlayerDetails_WithUser_AvatarUrlIsSet()
        {
            var result = Sut.Execute(CreateRequest(Constants.PlayerIdA, Constants.UserNameA));

            const string expected = "http://www.gravatar.com/avatar/0796c9df772de3f82c0c89377330471b?s=100";
            Assert.AreEqual(expected, result.AvatarUrl);
        }

        [Test]
        public void PlayerDetails_WithoutUser_UserUrlIsEmpty()
        {
            var result = Sut.Execute(CreateRequest(Constants.PlayerIdD, Constants.UserNameA));

            Assert.IsInstanceOf<EmptyUrl>(result.UserUrl);
        }

        [Test]
        public void PlayerDetails_WithUser_UserUrlIsSet()
        {
            var result = Sut.Execute(CreateRequest(Constants.PlayerIdA, Constants.UserNameA));

            Assert.IsInstanceOf<UserUrl>(result.UserUrl);
        }

        [Test]
        public void PlayerDetails_WithoutUser_IsUserIsFalse()
        {
            var result = Sut.Execute(CreateRequest(Constants.PlayerIdD, Constants.UserNameA));

            Assert.IsFalse(result.IsUser);
        }

        [Test]
        public void PlayerDetails_WithUser_IsUserIsTrue()
        {
            var result = Sut.Execute(CreateRequest(Constants.PlayerIdA, Constants.UserNameA));

            Assert.IsTrue(result.IsUser);
        }

        [Test]
        public void PlayerDetails_WithNormalUser_CanDeleteIsFalse()
        {
            var result = Sut.Execute(CreateRequest(Constants.PlayerIdA, Constants.UserNameA));

            Assert.IsFalse(result.CanDelete);
        }

        [Test]
        public void PlayerDetails_WithManagerAndPlayerHasNotPlayedGames_CanDeleteIsTrue()
        {
            var result = Sut.Execute(CreateRequest(Constants.PlayerIdD, Constants.UserNameC));

            Assert.IsTrue(result.CanDelete);
        }

        [Test]
        public void PlayerDetails_WithManagerAndPlayerHasPlayedGames_CanDeleteIsFalse()
        {
            var result = Sut.Execute(CreateRequest(Constants.PlayerIdA, Constants.UserNameA));

            Assert.IsFalse(result.CanDelete);
        }

        private static PlayerDetailsRequest CreateRequest(int playerId, string userName)
        {
            return new PlayerDetailsRequest(Constants.SlugA, playerId, userName);
        }

        private PlayerDetailsInteractor Sut
        {
            get
            {
                return new PlayerDetailsInteractor(
                    Repos.Bunch,
                    Repos.Player,
                    Repos.Cashgame,
                    Repos.User);
            }
        }
    }
}
