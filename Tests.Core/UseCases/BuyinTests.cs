using System;
using System.Linq;
using Core.Exceptions;
using Core.UseCases.Buyin;
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

            Repos.Cashgame.SetupRunningGame();

            Services.Time.UtcNow = timestamp;

            var request = new BuyinRequest(Constants.SlugA, PlayerId, buyin, stack);
            Execute(request);

            var result = Repos.Checkpoint.Added;

            Assert.AreEqual(timestamp, result.Timestamp);
            Assert.AreEqual(buyin, result.Amount);
            Assert.AreEqual(savedStack, result.Stack);
        }

        private void Execute(BuyinRequest request)
        {
            BuyinInteractor.Execute(
                Repos.Bunch,
                Repos.Player,
                Repos.Cashgame,
                Repos.Checkpoint,
                Services.Time,
                request);
        }
    }
}
