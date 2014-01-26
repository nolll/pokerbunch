using Core.Classes;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.PlayerCommands;

namespace Web.Tests.CommandTests.PlayerCommands
{
    public class DeletePlayerCommandTests : MockContainer
    {
        [Test]
        public void Execute_PlayerHasResults_ReturnsFalse()
        {
            var homegame = new FakeHomegame();
            var player = new FakePlayer();

            GetMock<ICashgameRepository>().Setup(o => o.HasPlayed(player)).Returns(true);

            var sut = GetSut(homegame, player);
            var result = sut.Execute();

            Assert.IsFalse(result);
        }

        [Test]
        public void Execute_PlayerHasNoResults_ReturnsTrue()
        {
            var homegame = new FakeHomegame();
            var player = new FakePlayer();

            GetMock<ICashgameRepository>().Setup(o => o.HasPlayed(player)).Returns(false);

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
