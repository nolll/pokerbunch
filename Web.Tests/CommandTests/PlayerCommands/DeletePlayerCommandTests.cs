using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.PlayerCommands;

namespace Web.Tests.CommandTests.PlayerCommands
{
    public class DeletePlayerCommandTests : WebMockContainer
    {
        [Test]
        public void Execute_PlayerHasResults_ReturnsFalse()
        {
            var homegame = new FakeHomegame();
            var player = new FakePlayer();

            Mocks.CashgameRepositoryMock.Setup(o => o.HasPlayed(player)).Returns(true);

            var sut = GetSut(homegame, player);
            var result = sut.Execute();

            Assert.IsFalse(result);
        }

        [Test]
        public void Execute_PlayerHasNoResults_ReturnsTrue()
        {
            var homegame = new FakeHomegame();
            var player = new FakePlayer();

            Mocks.CashgameRepositoryMock.Setup(o => o.HasPlayed(player)).Returns(false);

            var sut = GetSut(homegame, player);
            var result = sut.Execute();

            Assert.IsTrue(result);
        }

        private DeletePlayerCommand GetSut(Homegame homegame, Player player)
        {
            return new DeletePlayerCommand(
                Mocks.CashgameRepositoryMock.Object,
                Mocks.PlayerRepositoryMock.Object,
                homegame,
                player);
        }
    }
}
