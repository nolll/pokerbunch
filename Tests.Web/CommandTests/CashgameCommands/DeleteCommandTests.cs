using Core.Classes;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.CashgameCommands;

namespace Tests.Web.CommandTests.CashgameCommands
{
    public class DeleteCommandTests : MockContainer
    {
        [Test]
        public void Execute_CashgameWithPlayers_ReturnsFalse()
        {
            var cashgame = new FakeCashgame(playerCount: 1);

            var sut = GetSut(cashgame);
            var result = sut.Execute();

            Assert.IsFalse(result);
        }

        [Test]
        public void Execute_CashgameWithoutPlayers_CallsDeleteGameReturnsTrue()
        {
            var cashgame = new FakeCashgame();

            var sut = GetSut(cashgame);
            var result = sut.Execute();

            GetMock<ICashgameRepository>().Verify(o => o.DeleteGame(cashgame));

            Assert.IsTrue(result);
        }

        private DeleteCommand GetSut(Cashgame cashgame)
        {
            return new DeleteCommand(
                GetMock<ICashgameRepository>().Object,
                cashgame);
        }
    }
}
