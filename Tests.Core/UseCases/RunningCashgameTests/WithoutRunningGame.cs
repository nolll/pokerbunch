using Core.Exceptions;
using NUnit.Framework;

namespace Tests.Core.UseCases.RunningCashgameTests
{
    public class WithoutRunningGame : Arrange
    {
        protected override ExecuteMode ExecuteMode => ExecuteMode.Manual;
        protected override string BunchId => BunchIdWithoutRunningGame;

        [Test]
        public void RunningCashgame_CashgameNotRunning_ThrowsException()
        {
            Assert.Throws<CashgameNotRunningException>(Execute);
        }
    }
}