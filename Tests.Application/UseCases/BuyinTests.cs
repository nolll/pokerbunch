using System;
using System.Linq;
using Application.Services;
using Application.Urls;
using Application.UseCases.Buyin;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class BuyinTests : MockContainer
    {
        private const string Slug = "a";
        private const int PlayerId = 1;
        private const int ValidBuyin = 1;
        private const int InvalidBuyin = 0;
        private const int ValidStack = 0;
        private const int InvalidStack = -1;

        [Test]
        public void Buyin_InvalidBuyin_ReturnsError()
        {
            var request = new BuyinRequest(Slug, PlayerId, InvalidBuyin, ValidStack);
            var result = Sut.Execute(request);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count());
        }

        [Test]
        public void Buyin_InvalidStackSize_ReturnsError()
        {
            var request = new BuyinRequest(Slug, PlayerId, ValidBuyin, InvalidStack);
            var result = Sut.Execute(request);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(1, result.Errors.Count());
        }

        [Test]
        public void Buyin_StartedCashgame_AddsCheckpointWithCorrectValues()
        {
            var timestamp = DateTime.Now;
            const int buyin = 1;
            const int stack = 2;
            const int savedStack = 3;
            Checkpoint result = null;
            
            SetupCashgame();
            GetMock<ITimeProvider>().Setup(o => o.GetTime()).Returns(timestamp);
            GetMock<ICheckpointRepository>().
                Setup(o => o.AddCheckpoint(It.IsAny<Cashgame>(), It.IsAny<Player>(), It.IsAny<Checkpoint>())).
                Callback((Cashgame cashgame, Player player, Checkpoint c) => result = c);

            var request = new BuyinRequest(Slug, PlayerId, buyin, stack);
            Sut.Execute(request);
            
            Assert.AreEqual(timestamp, result.Timestamp);
            Assert.AreEqual(buyin, result.Amount);
            Assert.AreEqual(savedStack, result.Stack);
        }

        [Test]
        public void Buyin_NotStartedCashgame_AddsCheckpointAndStartsGame()
        {
            SetupCashgameThatIsntStarted();

            var request = new BuyinRequest(Slug, PlayerId, ValidBuyin, ValidStack);
            Sut.Execute(request);

            GetMock<ICheckpointRepository>().Verify(o => o.AddCheckpoint(It.IsAny<Cashgame>(), It.IsAny<Player>(), It.IsAny<BuyinCheckpoint>()));
            GetMock<ICashgameRepository>().Verify(o => o.StartGame(It.IsAny<Cashgame>()));
        }

        [Test]
        public void Buyin_ReturnUrlIsSetToRunningCashgame()
        {
            SetupCashgame();

            var request = new BuyinRequest(Slug, PlayerId, ValidBuyin, ValidStack);
            var result = Sut.Execute(request);

            Assert.IsInstanceOf<RunningCashgameUrl>(result.ReturnUrl);
        }

        private void SetupCashgame()
        {
            var cashgame = new CashgameInTest(isStarted: true);
            SetupCashgame(cashgame);
        }

        private void SetupCashgameThatIsntStarted()
        {
            var cashgame = new CashgameInTest();
            SetupCashgame(cashgame);
        }

        private void SetupCashgame(Cashgame cashgame)
        {
            GetMock<ICashgameRepository>().Setup(o => o.GetRunning(It.IsAny<Homegame>())).Returns(cashgame);
        }

        private BuyinInteractor Sut
        {
            get { return new BuyinInteractor(
                GetMock<IHomegameRepository>().Object,
                GetMock<IPlayerRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                GetMock<ICheckpointRepository>().Object,
                GetMock<ITimeProvider>().Object);
            }
        }
    }
}
