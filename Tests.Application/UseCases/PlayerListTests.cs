using System.Collections.Generic;
using Application.Services;
using Application.UseCases.PlayerList;
using Core.Entities;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class PlayerListTests : MockContainer
    {
        [Test]
        public void Execute_WithSlug_SlugAndPlayersAreSet()
        {
            const string slug = "a";
            const string playerName = "b";
            const int playerId = 1;

            var homegame = new BunchInTest(slug: slug);
            var player = new PlayerInTest(id: playerId, displayName: playerName);
            var players = new List<Player> { player };
            var request = new PlayerListRequest(slug);

            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<IPlayerRepository>().Setup(o => o.GetList(homegame)).Returns(players);

            var result = Execute(request);

            Assert.AreEqual(slug, result.Slug);
            Assert.AreEqual(1, result.Players.Count);
            Assert.AreEqual(playerId, result.Players[0].Id);
            Assert.AreEqual(playerName, result.Players[0].Name);
            Assert.IsFalse(result.CanAddPlayer);
        }

        [Test]
        public void Execute_PlayersAreSortedAlphabetically()
        {
            const string slug = "c";
            const string playerName1 = "b";
            const string playerName2 = "a";

            var homegame = new BunchInTest();
            var player1 = new PlayerInTest(displayName: playerName1);
            var player2 = new PlayerInTest(displayName: playerName2);
            var players = new List<Player> { player1, player2 };
            var request = new PlayerListRequest(slug);

            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<IPlayerRepository>().Setup(o => o.GetList(It.IsAny<Bunch>())).Returns(players);

            var result = Execute(request);

            Assert.AreEqual(playerName2, result.Players[0].Name);
            Assert.AreEqual(playerName1, result.Players[1].Name);
        }

        [Test]
        public void Execute_PlayerIsManager_CanAddPlayerIsTrue()
        {
            const string slug = "a";
            const string playerName = "b";
            const int playerId = 1;

            var homegame = new BunchInTest(slug: slug);
            var player = new PlayerInTest(id: playerId, displayName: playerName);
            var players = new List<Player> { player };
            var request = new PlayerListRequest(slug);

            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<IPlayerRepository>().Setup(o => o.GetList(homegame)).Returns(players);
            GetMock<IAuth>().Setup(o => o.IsInRole(slug, Role.Manager)).Returns(true);

            var result = Execute(request);

            Assert.IsTrue(result.CanAddPlayer);
        }

        private PlayerListResult Execute(PlayerListRequest request)
        {
            return PlayerListInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                GetMock<IPlayerRepository>().Object,
                GetMock<IAuth>().Object,
                request);
        }
    }
}
