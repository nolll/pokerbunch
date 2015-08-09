using Core.UseCases.DeletePlayer;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class DeletePlayerTests : TestBase
    {
        [Test]
        public void DeletePlayer_PlayerHasntPlayed_PlayerDeletedAndReturnUrlIsPlayerIndex()
        {
            const int playerIdThatHasNotPlayed = 3;

            var request = new DeletePlayerRequest(TestData.SlugA, playerIdThatHasNotPlayed);
            var result = Sut.Execute(request);

            Assert.IsTrue(result.Deleted);
            Assert.AreEqual(TestData.SlugA, result.Slug);
            Assert.AreEqual(playerIdThatHasNotPlayed, result.PlayerId);
            Assert.AreEqual(playerIdThatHasNotPlayed, Repos.Player.Deleted);
        }

        [Test]
        public void DeletePlayer_PlayerHasPlayed_ReturnUrlIsPlayerDetails()
        {
            var request = new DeletePlayerRequest(TestData.SlugA, TestData.PlayerIdA);
            var result = Sut.Execute(request);

            Assert.IsFalse(result.Deleted);
            Assert.AreEqual(TestData.SlugA, result.Slug);
            Assert.AreEqual(TestData.PlayerIdA, result.PlayerId);
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
