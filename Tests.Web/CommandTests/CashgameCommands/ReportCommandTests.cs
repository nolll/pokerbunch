using Core.Classes;
using Core.Classes.Checkpoints;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.CashgameCommands;
using Web.ModelMappers;
using Web.Models.CashgameModels.Report;

namespace Tests.Web.CommandTests.CashgameCommands
{
    public class ReportCommandTests : MockContainer
    {
        private const int ValidStack = 1;
        private const int InvalidStack = 0;

        [Test]
        public void Execute_WithInvalidStack_ReturnsFalse()
        {
            var player = new FakePlayer();
            var cashgame = new FakeCashgame();
            var model = new ReportPostModel { StackAmount = InvalidStack };

            var sut = GetSut(player, cashgame, model);
            var result = sut.Execute();

            Assert.IsFalse(result);
        }

        [Test]
        public void Execute_WithValidData_ReturnsTrue()
        {
            var player = new FakePlayer();
            var cashgame = new FakeCashgame();
            var model = new ReportPostModel { StackAmount = ValidStack };

            var sut = GetSut(player, cashgame, model);
            var result = sut.Execute();

            Assert.IsTrue(result);
        }

        [Test]
        public void Execute_WithValidData_CallsAddCheckpoint()
        {
            var player = new FakePlayer();
            var cashgame = new FakeCashgame();
            var model = new ReportPostModel { StackAmount = ValidStack };
            var checkpoint = new FakeCheckpoint(type: CheckpointType.Report);

            GetMock<ICheckpointModelMapper>().Setup(o => o.GetCheckpoint(model)).Returns(checkpoint);

            var sut = GetSut(player, cashgame, model);
            sut.Execute();

            GetMock<ICheckpointRepository>().Verify(o => o.AddCheckpoint(cashgame, player, checkpoint));
        }

        private ReportCommand GetSut(
            Player player,
            Cashgame cashgame,
            ReportPostModel model)
        {
            return new ReportCommand(
                GetMock<ICheckpointModelMapper>().Object,
                GetMock<ICheckpointRepository>().Object,
                cashgame,
                player,
                model);
        }
    }
}
