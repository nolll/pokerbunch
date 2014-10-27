using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases.PlayerBadges;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class PlayerBadgesTests : TestBase
    {
        private const string Slug = "a";
        private const int PlayerId = 1;

        [Test]
        public void PlayerBadges_ZeroGames_AllBadgesAreFalse()
        {
            SetupGames(0);

            var result = Execute(CreateRequest());

            Assert.IsFalse(result.PlayedOneGame);
            Assert.IsFalse(result.PlayedTenGames);
            Assert.IsFalse(result.Played50Games);
            Assert.IsFalse(result.Played100Games);
            Assert.IsFalse(result.Played200Games);
            Assert.IsFalse(result.Played500Games);
        }

        [Test]
        public void PlayerBadges_OneGame_PlayedOneGameIsTrue()
        {
            SetupGames(1);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.PlayedOneGame);
        }

        [Test]
        public void PlayerBadges_TenGames_PlayedTenGamesIsTrue()
        {
            SetupGames(10);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.PlayedTenGames);
        }

        [Test]
        public void PlayerBadges_50Games_Played50GamesIsTrue()
        {
            SetupGames(50);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.Played50Games);
        }

        [Test]
        public void PlayerBadges_100Games_Played100GamesIsTrue()
        {
            SetupGames(100);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.Played100Games);
        }

        [Test]
        public void PlayerBadges_200Games_Played200GamesIsTrue()
        {
            SetupGames(200);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.Played200Games);
        }

        [Test]
        public void PlayerBadges_500Games_Played500GamesIsTrue()
        {
            SetupGames(500);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.Played500Games);
        }

        private void SetupGames(int gameCount)
        {
            var bunch = A.Bunch.Build();
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(It.IsAny<string>())).Returns(bunch);

            var games = CreateGameList(gameCount);
            GetMock<ICashgameRepository>().Setup(o => o.GetFinished(It.IsAny<int>(), It.IsAny<int?>())).Returns(games);
        }

        private IList<Cashgame> CreateGameList(int gameCount)
        {
            var list = new List<Cashgame>(gameCount);
            for (var i = 0; i < gameCount; i++)
                list.Add(CreateGame());
            return list;
        }

        private Cashgame CreateGame()
        {
            var result = A.CashgameResult.WithPlayerId(PlayerId).Build();
            var results = new List<CashgameResult> {result};
            return A.Cashgame.WithResults(results).Build();
        }

        private PlayerBadgesRequest CreateRequest()
        {
            return new PlayerBadgesRequest(Slug, PlayerId);
        }

        private PlayerBadgesResult Execute(PlayerBadgesRequest request)
        {
            return PlayerBadgesInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                request);
        }
    }
}
