using System;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases.BuyinTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void Buyin_StartedCashgame_AddsCheckpointWithCorrectValues()
        {
            var timestamp = DateTime.UtcNow;
            const int buyin = 1;
            const int stack = 2;
            const int savedStack = 3;

            Assert.AreEqual(CashgameId, PostedCashgameId);
            Assert.AreEqual(PlayerId, PostedPlayerId);
            Assert.AreEqual(AddedAmount, PostedAmount);
            Assert.AreEqual(Stack, PostedStack);
        }
    }
}
