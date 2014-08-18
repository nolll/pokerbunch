using Core.Entities;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.PlayerCommands;

namespace Tests.Web.CommandTests.PlayerCommands
{
    public class DeletePlayerCommandTests : MockContainer
    {
        [Test]
        public void Execute_PlayerHasResults_ReturnsFalse()
        {
            const int playerId = 1;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest(id: playerId);

            GetMock<ICashgameRepository>().Setup(o => o.HasPlayed(playerId)).Returns(true);

            var sut = GetSut(homegame, player);
            var result = sut.Execute();

            Assert.IsFalse(result);
        }

        [Test]
        public void Execute_PlayerHasNoResults_ReturnsTrue()
        {
            const int playerId = 1;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest(id: playerId);

            GetMock<ICashgameRepository>().Setup(o => o.HasPlayed(playerId)).Returns(false);

            var sut = GetSut(homegame, player);
            var result = sut.Execute();

            Assert.IsTrue(result);
        }

        private DeletePlayerCommand GetSut(Homegame homegame, Player player)
        {
            return new DeletePlayerCommand(
                GetMock<ICashgameRepository>().Object,
                GetMock<IPlayerRepository>().Object,
                homegame,
                player);
        }
    }
}
