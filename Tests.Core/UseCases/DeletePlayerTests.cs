using Core.Entities;
using Core.Repositories;
using Core.Urls;
using Core.UseCases.DeletePlayer;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class DeletePlayerTests : TestBase
    {
        private const string Slug = "a";
        private const int PlayerId = 1;

        [Test]
        public void DeletePlayer_PlayerHasntPlayed_PlayerDeletedAndReturnUrlIsPlayerIndex()
        {
            var request = new DeletePlayerRequest(Slug, PlayerId);

            var result = Execute(request);

            Assert.IsInstanceOf<PlayerIndexUrl>(result.ReturnUrl);
            GetMock<IPlayerRepository>().Verify(o => o.Delete(It.IsAny<Bunch>(), It.IsAny<Player>()));
        }

        [Test]
        public void DeletePlayer_PlayerHasPlayed_ReturnUrlIsPlayerDetails()
        {
            var request = new DeletePlayerRequest(Slug, PlayerId);

            GetMock<ICashgameRepository>().Setup(o => o.HasPlayed(PlayerId)).Returns(true);

            var result = Execute(request);

            Assert.IsInstanceOf<PlayerDetailsUrl>(result.ReturnUrl);
        }

        private DeletePlayerResult Execute(DeletePlayerRequest request)
        {
            return DeletePlayerInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                GetMock<IPlayerRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                request);
        }
    }
}
