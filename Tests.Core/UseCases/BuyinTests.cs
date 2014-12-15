using System;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Urls;
using Core.UseCases.Buyin;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class BuyinTests : TestBase
    {
        private const int PlayerId = 1;
        private const int ValidBuyin = 1;
        private const int InvalidBuyin = 0;
        private const int ValidStack = 0;
        private const int InvalidStack = -1;

        [Test]
        public void Buyin_InvalidBuyin_ReturnsError()
        {
            var request = new BuyinRequest(Constants.SlugA, PlayerId, InvalidBuyin, ValidStack);

            var ex = Assert.Throws<ValidationException>(() => Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        [Test]
        public void Buyin_InvalidStackSize_ReturnsError()
        {
            var request = new BuyinRequest(Constants.SlugA, PlayerId, ValidBuyin, InvalidStack);

            var ex = Assert.Throws<ValidationException>(() => Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        [Test]
        public void Buyin_StartedCashgame_AddsCheckpointWithCorrectValues()
        {
            var timestamp = DateTime.Now;
            const int buyin = 1;
            const int stack = 2;
            const int savedStack = 3;
            
            SetupCashgame();

            Services.Time.UtcNow = timestamp;

            var request = new BuyinRequest(Constants.SlugA, PlayerId, buyin, stack);
            Execute(request);

            var result = Repos.Checkpoint.Added;

            Assert.AreEqual(timestamp, result.Timestamp);
            Assert.AreEqual(buyin, result.Amount);
            Assert.AreEqual(savedStack, result.Stack);
            GetMock<ICashgameRepository>().Verify(o => o.StartGame(It.IsAny<Cashgame>()), Times.Never);
        }

        [Test]
        public void Buyin_NotStartedCashgame_AddsCheckpointAndStartsGame()
        {
            SetupCashgameThatIsntStarted();

            var request = new BuyinRequest(Constants.SlugA, PlayerId, ValidBuyin, ValidStack);
            Execute(request);

            Assert.IsNotNull(Repos.Checkpoint.Added);
            GetMock<ICashgameRepository>().Verify(o => o.StartGame(It.IsAny<Cashgame>()));
        }

        [Test]
        public void Buyin_ReturnUrlIsSetToRunningCashgame()
        {
            SetupCashgame();

            var request = new BuyinRequest(Constants.SlugA, PlayerId, ValidBuyin, ValidStack);
            var result = Execute(request);

            Assert.IsInstanceOf<RunningCashgameUrl>(result.ReturnUrl);
        }

        private void SetupCashgame()
        {
            var cashgame = A.Cashgame.ThatIsStarted().Build();
            SetupCashgame(cashgame);
        }

        private void SetupCashgameThatIsntStarted()
        {
            var cashgame = A.Cashgame.Build();
            SetupCashgame(cashgame);
        }

        private void SetupCashgame(Cashgame cashgame)
        {
            GetMock<ICashgameRepository>().Setup(o => o.GetRunning(It.IsAny<Bunch>())).Returns(cashgame);
        }
        
        private BuyinResult Execute(BuyinRequest request)
        {
            return BuyinInteractor.Execute(
                Repos.Bunch,
                Repos.Player,
                GetMock<ICashgameRepository>().Object,
                Repos.Checkpoint,
                Services.Time,
                request);
        }
    }
}
