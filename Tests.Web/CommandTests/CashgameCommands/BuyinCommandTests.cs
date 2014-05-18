using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.Commands.CashgameCommands;
using Web.ModelMappers;
using Web.Models.CashgameModels.Buyin;

namespace Tests.Web.CommandTests.CashgameCommands
{
    public class BuyinCommandTests : MockContainer
    {
        private const int ValidBuyin = 1;
        private const int ValidStack = 0;
        private const int InvalidBuyin = 0;
        private const int InvalidStack = -1;

        [Test]
        public void Execute_WithInvalidBuyin_ReturnsFalse()
        {
            var player = new FakePlayer();
            var cashgame = new FakeCashgame();
            var model = new BuyinPostModel { BuyinAmount = InvalidBuyin, StackAmount = ValidStack };

            var sut = GetSut(player, cashgame, model);
            var result = sut.Execute();

            Assert.IsFalse(result);
        }

        [Test]
        public void Execute_WithInvalidStack_ReturnsFalse()
        {
            var player = new FakePlayer();
            var cashgame = new FakeCashgame();
            var model = new BuyinPostModel { BuyinAmount = ValidBuyin, StackAmount = InvalidStack };

            var sut = GetSut(player, cashgame, model);
            var result = sut.Execute();

            Assert.IsFalse(result);
        }

        [Test]
        public void Execute_WithValidData_ReturnsTrue()
        {
            var player = new FakePlayer();
            var cashgame = new FakeCashgame();
            var model = new BuyinPostModel { BuyinAmount = ValidBuyin, StackAmount = ValidStack };

            var sut = GetSut(player, cashgame, model);
            var result = sut.Execute();

            Assert.IsTrue(result);
        }

        [Test]
        public void Execute_WithValidData_CallsAddCheckpoint()
        {
            var player = new FakePlayer();
            var cashgame = new FakeCashgame();
            var model = new BuyinPostModel { BuyinAmount = ValidBuyin, StackAmount = ValidStack };
            var checkpoint = new FakeCheckpoint(type: CheckpointType.Buyin);

            GetMock<ICheckpointModelMapper>().Setup(o => o.GetCheckpoint(model)).Returns(checkpoint);

            var sut = GetSut(player, cashgame, model);
            sut.Execute();

            GetMock<ICheckpointRepository>().Verify(o => o.AddCheckpoint(cashgame, player, checkpoint));
        }

        [Test]
        public void Execute_WithCashgameThatIsStarted_DoesntCallStartGame()
        {
            var player = new FakePlayer();
            var cashgame = new FakeCashgame(isStarted: true);
            var model = new BuyinPostModel { BuyinAmount = ValidBuyin, StackAmount = ValidStack };

            var sut = GetSut(player, cashgame, model);
            sut.Execute();

            GetMock<ICashgameRepository>().Verify(o => o.StartGame(cashgame), Times.Never());
        }
        
        [Test]
        public void Execute_WithCashgameThatIsntStarted_CallsStartGame()
        {
            var player = new FakePlayer();
            var cashgame = new FakeCashgame();
            var model = new BuyinPostModel { BuyinAmount = ValidBuyin, StackAmount = ValidStack };

            var sut = GetSut(player, cashgame, model);
            sut.Execute();

            GetMock<ICashgameRepository>().Verify(o => o.StartGame(cashgame));
        }

        private BuyinCommand GetSut(
            Player player,
            Cashgame cashgame,
            BuyinPostModel model)
        {
            return new BuyinCommand(
                GetMock<ICheckpointModelMapper>().Object,
                GetMock<ICheckpointRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                player,
                cashgame,
                model);
        }
    }
}
