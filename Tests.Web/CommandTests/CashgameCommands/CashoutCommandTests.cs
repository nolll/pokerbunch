using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.CashgameCommands;
using Web.ModelMappers;
using Web.Models.CashgameModels.Cashout;

namespace Tests.Web.CommandTests.CashgameCommands
{
    public class CashoutCommandTests : MockContainer
    {
        private const int ValidStack = 0;
        private const int InvalidStack = -1;

        [Test]
        public void Execute_WithInvalidStack_ReturnsFalse()
        {
            var player = new PlayerInTest();
            var cashgame = new CashgameInTest();
            var cashgameResult = new CashgameResultInTest();
            var model = new CashoutPostModel { StackAmount = InvalidStack };

            var sut = GetSut(player, cashgame, cashgameResult, model);
            var result = sut.Execute();

            Assert.IsFalse(result);
        }

        [Test]
        public void Execute_WithValidData_ReturnsTrue()
        {
            var player = new PlayerInTest();
            var cashgame = new CashgameInTest();
            var cashgameResult = new CashgameResultInTest();
            var model = new CashoutPostModel { StackAmount = ValidStack };

            var sut = GetSut(player, cashgame, cashgameResult, model);
            var result = sut.Execute();

            Assert.IsTrue(result);
        }

        [Test]
        public void Execute_WithValidDataAndNonexistingCheckpoint_CallsAddCheckpoint()
        {
            var player = new PlayerInTest();
            var cashgame = new CashgameInTest();
            var cashgameResult = new CashgameResultInTest();
            var model = new CashoutPostModel { StackAmount = ValidStack };
            var checkpoint = new CheckpointInTest(type: CheckpointType.Cashout);

            GetMock<ICheckpointModelMapper>().Setup(o => o.GetCheckpoint(model, null)).Returns(checkpoint);

            var sut = GetSut(player, cashgame, cashgameResult, model);
            sut.Execute();

            GetMock<ICheckpointRepository>().Verify(o => o.AddCheckpoint(cashgame, player, checkpoint));
        }

        [Test]
        public void Execute_WithValidDataAndExistingCheckpoint_CallsUpdateCheckpoint()
        {
            var player = new PlayerInTest();
            var cashgame = new CashgameInTest();
            var checkpoint = new CheckpointInTest(type: CheckpointType.Cashout);
            var cashgameResult = new CashgameResultInTest(cashoutCheckpoint: checkpoint);
            var model = new CashoutPostModel { StackAmount = ValidStack };

            GetMock<ICheckpointModelMapper>().Setup(o => o.GetCheckpoint(model, checkpoint)).Returns(checkpoint);

            var sut = GetSut(player, cashgame, cashgameResult, model);
            sut.Execute();

            GetMock<ICheckpointRepository>().Verify(o => o.UpdateCheckpoint(cashgame, checkpoint));
        }

        private CashoutCommand GetSut(
            Player player,
            Cashgame cashgame,
            CashgameResult cashgameResult,
            CashoutPostModel model)
        {
            return new CashoutCommand(
                GetMock<ICheckpointRepository>().Object,
                GetMock<ICheckpointModelMapper>().Object,
                cashgame,
                player,
                cashgameResult,
                model);
        }
    }
}
