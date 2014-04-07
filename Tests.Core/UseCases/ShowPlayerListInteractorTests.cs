using System.Collections.Generic;
using Core.Classes;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Core.UseCases
{
    class ShowPlayerListInteractorTests : MockContainer
    {
        private ShowPlayerListInteractor _sut;

        [SetUp]
        public virtual void SetUp()
        {
            _sut = new ShowPlayerListInteractor(
                GetMock<IHomegameRepository>().Object,
                GetMock<IPlayerRepository>().Object);
        }

        [Test]
        public void Execute_WithSlug_PlayersAreSet()
        {
            const string slug = "a";
            const string playerName = "b";
            var homegame = new FakeHomegame(slug: slug);
            var player = new FakePlayer(displayName: playerName);
            var players = new List<Player> {player};

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<IPlayerRepository>().Setup(o => o.GetList(homegame)).Returns(players);

            var result = _sut.Execute(slug);

            Assert.AreEqual(1, result.Players.Count);
            Assert.AreEqual(playerName, result.Players[0].Name);
        }
    }
}
