using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.UseCases.PlayerList;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Core.UseCases
{
    class PlayerListTests : TestBase
    {
        [Test]
        public void Execute_WithSlug_SlugAndPlayersAreSet()
        {
            const string slug = "a";
            const string playerName = "b";
            const int playerId = 1;

            var bunch = A.Bunch.WithSlug(slug).Build();
            var player = new PlayerInTest(id: playerId, displayName: playerName);
            var players = new List<Player> { player };
            var request = new PlayerListRequest(slug);

            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(slug)).Returns(bunch);
            GetMock<IPlayerRepository>().Setup(o => o.GetList(bunch)).Returns(players);

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

            var bunch = A.Bunch.Build();
            var player1 = new PlayerInTest(displayName: playerName1);
            var player2 = new PlayerInTest(displayName: playerName2);
            var players = new List<Player> { player1, player2 };
            var request = new PlayerListRequest(slug);

            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(slug)).Returns(bunch);
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

            var bunch = A.Bunch.WithSlug(slug).Build();
            var player = new PlayerInTest(id: playerId, displayName: playerName);
            var players = new List<Player> { player };
            var request = new PlayerListRequest(slug);

            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(slug)).Returns(bunch);
            GetMock<IPlayerRepository>().Setup(o => o.GetList(bunch)).Returns(players);
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
