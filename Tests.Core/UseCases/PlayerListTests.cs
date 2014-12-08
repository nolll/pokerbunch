using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.UseCases.PlayerList;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class PlayerListTests : TestBase
    {
        [Test]
        public void Execute_WithSlug_SlugAndPlayersAreSet()
        {
            const string playerName = "b";
            const int playerId = 1;

            var player = A.Player.WithId(playerId).WithDisplayName(playerName).Build();
            var players = new List<Player> { player };
            var request = new PlayerListRequest(Constants.SlugA);

            GetMock<IPlayerRepository>().Setup(o => o.GetList(It.IsAny<int>())).Returns(players);

            var result = Execute(request);

            Assert.AreEqual("/bunch-a/player/add", result.AddUrl.Relative);
            Assert.AreEqual(1, result.Players.Count);
            Assert.AreEqual("/bunch-a/player/details/1", result.Players[0].Url.Relative);
            Assert.AreEqual(playerName, result.Players[0].Name);
            Assert.IsFalse(result.CanAddPlayer);
        }

        [Test]
        public void Execute_PlayersAreSortedAlphabetically()
        {
            const string playerName1 = "b";
            const string playerName2 = "a";

            var player1 = A.Player.WithDisplayName(playerName1).Build();
            var player2 = A.Player.WithDisplayName(playerName2).Build();
            var players = new List<Player> { player1, player2 };
            var request = new PlayerListRequest(Constants.SlugA);

            GetMock<IPlayerRepository>().Setup(o => o.GetList(It.IsAny<int>())).Returns(players);

            var result = Execute(request);

            Assert.AreEqual(playerName2, result.Players[0].Name);
            Assert.AreEqual(playerName1, result.Players[1].Name);
        }

        [Test]
        public void Execute_PlayerIsManager_CanAddPlayerIsTrue()
        {
            const string playerName = "b";
            const int playerId = 1;

            var player = A.Player.WithId(playerId).WithDisplayName(playerName).Build();
            var players = new List<Player> { player };
            var request = new PlayerListRequest(Constants.SlugA);

            GetMock<IPlayerRepository>().Setup(o => o.GetList(It.IsAny<int>())).Returns(players);
            GetMock<IAuth>().Setup(o => o.IsInRole(Constants.SlugA, Role.Manager)).Returns(true);

            var result = Execute(request);

            Assert.IsTrue(result.CanAddPlayer);
        }

        private PlayerListResult Execute(PlayerListRequest request)
        {
            return PlayerListInteractor.Execute(
                Repos.Bunch,
                GetMock<IPlayerRepository>().Object,
                GetMock<IAuth>().Object,
                request);
        }
    }
}
