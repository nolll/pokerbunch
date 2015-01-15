using Core.UseCases.PlayerBadges;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class PlayerBadgesTests : TestBase
    {
        [Test]
        public void PlayerBadges_ZeroGames_AllBadgesAreFalse()
        {
            Repos.Cashgame.ClearList();

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
            var result = Execute(CreateRequest());

            Assert.IsTrue(result.PlayedOneGame);
        }

        [Test]
        public void PlayerBadges_TenGames_PlayedTenGamesIsTrue()
        {
            Repos.Cashgame.SetupManyGames(10);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.PlayedTenGames);
        }

        [Test]
        public void PlayerBadges_50Games_Played50GamesIsTrue()
        {
            Repos.Cashgame.SetupManyGames(50);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.Played50Games);
        }

        [Test]
        public void PlayerBadges_100Games_Played100GamesIsTrue()
        {
            Repos.Cashgame.SetupManyGames(100);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.Played100Games);
        }

        [Test]
        public void PlayerBadges_200Games_Played200GamesIsTrue()
        {
            Repos.Cashgame.SetupManyGames(200);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.Played200Games);
        }

        [Test]
        public void PlayerBadges_500Games_Played500GamesIsTrue()
        {
            Repos.Cashgame.SetupManyGames(500);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.Played500Games);
        }

        private PlayerBadgesRequest CreateRequest()
        {
            return new PlayerBadgesRequest(Constants.SlugA, Constants.PlayerIdA);
        }

        private PlayerBadgesResult Execute(PlayerBadgesRequest request)
        {
            return PlayerBadgesInteractor.Execute(
                Repos.Bunch,
                Repos.Cashgame,
                request);
        }
    }
}
