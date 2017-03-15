using Core.Entities;
using NUnit.Framework;

namespace Tests.Core.UseCases.DeleteCheckpointTests
{
    public class WhenExecute : Arrange
    {
        [Test]
        public void DeletesCheckpointAndReturnsCorrectValues()
        {
            Assert.AreEqual(3, UpdatedCashgame.Checkpoints.Count);
            Assert.AreEqual(BunchId, Result.Slug);
            Assert.AreEqual(CashgameId, Result.CashgameId);
            Assert.IsFalse(Result.GameIsRunning);
        }
    }

    public class WithRunningGame : Arrange
    {
        protected override GameStatus GameStatus => GameStatus.Running;

        [Test]
        public void DeletesCheckpointAndReturnsCorrectValues()
        {
            Assert.IsTrue(Result.GameIsRunning);
        }
    }
}