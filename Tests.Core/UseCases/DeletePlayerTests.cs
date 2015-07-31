using Core.Urls;
using Core.UseCases.DeletePlayer;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class DeletePlayerTests : TestBase
    {
        [Test]
        public void DeletePlayer_PlayerHasntPlayed_PlayerDeletedAndReturnUrlIsPlayerIndex()
        {
            const int playerIdThatHasNotPlayed = 3;

            var request = new DeletePlayerRequest(TestData.SlugA, playerIdThatHasNotPlayed);
            var result = Sut.Execute(request);

            Assert.IsInstanceOf<PlayerIndexUrl>(result.ReturnUrl);
            Assert.AreEqual(playerIdThatHasNotPlayed, Repos.Player.Deleted);
        }

        [Test]
        public void DeletePlayer_PlayerHasPlayed_ReturnUrlIsPlayerDetails()
        {
            var request = new DeletePlayerRequest(TestData.SlugA, TestData.PlayerIdA);
            var result = Sut.Execute(request);

            Assert.IsInstanceOf<PlayerDetailsUrl>(result.ReturnUrl);
        }

        private DeletePlayerInteractor Sut
        {
            get
            {
                return new DeletePlayerInteractor(
                    Repos.Player,
                    Repos.Cashgame);
            }
        }
    }
}
