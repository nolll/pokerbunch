using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public class WithRunningGame : Arrange
    {
        protected override string BunchId => BunchIdWithRunningGame;

        [Test]
        public void GameIsRunningIsTrue()
        {
            var result = Sut.Execute(Request);

            Assert.IsTrue(result.GameIsRunning);
        }
    }
}