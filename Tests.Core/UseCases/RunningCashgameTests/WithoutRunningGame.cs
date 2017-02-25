using Core.Exceptions;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases.RunningCashgameTests
{
    public class WithoutRunningGame : Arrange
    {
        protected override string BunchId => BunchIdWithoutRunningGame;

        [Test]
        public void RunningCashgame_CashgameNotRunning_ThrowsException()
        {
            Assert.Throws<CashgameNotRunningException>(() => Execute());
        }
    }
}