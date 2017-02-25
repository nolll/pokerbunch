using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public class WithRunningGame : Arrange
    {
        protected override string BunchId => BunchIdWithRunningGame;

        [Test]
        public void GameIsRunningIsTrue() => Assert.IsTrue(Result.GameIsRunning);
    }
}