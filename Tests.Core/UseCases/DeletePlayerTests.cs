﻿using Core.Repositories;
using Core.Urls;
using Core.UseCases.DeletePlayer;
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
            Assert.AreEqual(PlayerId, Repos.Player.Deleted);
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
                Repos.Player,
                GetMock<ICashgameRepository>().Object,
                request);
        }
    }
}
