using Core.Classes;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.CashgameCommands;

namespace Tests.Web.CommandTests.CashgameCommands
{
    public class EndGameCommandTests : MockContainer
    {
        [Test]
        public void Execute_EndsGameAndReturnsTrue()
        {
            var homegame = new FakeHomegame();
            var cashgame = new FakeCashgame();

            var sut = GetSut(homegame, cashgame);
            var result = sut.Execute();

            Assert.IsTrue(result);
            GetMock<ICashgameRepository>().Verify(o => o.EndGame(homegame, cashgame));
        }

        private EndGameCommand GetSut(Homegame homegame, Cashgame cashgame)
        {
            return new EndGameCommand(
                GetMock<ICashgameRepository>().Object,
                homegame,
                cashgame);
        }
    }
}
