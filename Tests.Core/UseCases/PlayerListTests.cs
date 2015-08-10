using Core.UseCases.PlayerList;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class PlayerListTests : TestBase
    {
        [Test]
        public void Execute_WithSlug_SlugAndPlayersAreSet()
        {
            var request = new PlayerListRequest(TestData.SlugA, TestData.UserNameA);

            var result = Sut.Execute(request);

            Assert.AreEqual("/bunch-a/player/add", result.AddUrl.Relative);
            Assert.AreEqual(4, result.Players.Count);
            Assert.AreEqual(1, result.Players[0].Id);
            Assert.AreEqual(TestData.PlayerNameA, result.Players[0].Name);
            Assert.IsFalse(result.CanAddPlayer);
        }

        [Test]
        public void Execute_PlayersAreSortedAlphabetically()
        {
            var request = new PlayerListRequest(TestData.SlugA, TestData.UserNameA);

            var result = Sut.Execute(request);

            Assert.AreEqual(TestData.PlayerNameA, result.Players[0].Name);
            Assert.AreEqual(TestData.PlayerNameB, result.Players[1].Name);
        }

        [Test]
        public void Execute_PlayerIsManager_CanAddPlayerIsTrue()
        {
            var request = new PlayerListRequest(TestData.SlugA, TestData.UserNameC);

            var result = Sut.Execute(request);

            Assert.IsTrue(result.CanAddPlayer);
        }

        private PlayerListInteractor Sut
        {
            get
            {
                return new PlayerListInteractor(
                    Repos.Bunch,
                    Repos.User,
                    Repos.Player);
            }
        }
    }
}
