using Core.Classes;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.CashgameCommands;

namespace Tests.Web.CommandTests.CashgameCommands
{
    public class DeleteCheckpointCommandTests : MockContainer
    {
        [Test]
        public void Execute_CallsDeleteCheckpointAndReturnsTrue()
        {
            var cashgame = new FakeCashgame();
            const int checkpointId = 1;

            var sut = GetSut(cashgame, checkpointId);
            var result = sut.Execute();

            GetMock<ICheckpointRepository>().Verify(o => o.DeleteCheckpoint(cashgame, checkpointId));
            Assert.IsTrue(result);
        }

        private DeleteCheckpointCommand GetSut(Cashgame cashgame, int checkpointId)
        {
            return new DeleteCheckpointCommand(
                GetMock<ICheckpointRepository>().Object,
                cashgame,
                checkpointId);
        }
    }
}
