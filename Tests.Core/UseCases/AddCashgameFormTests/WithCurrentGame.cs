using Core.Exceptions;
using NUnit.Framework;

namespace Tests.Core.UseCases.AddCashgameFormTests
{
    public class WithCurrentGame : Arrange
    {
        protected override string BunchId => BunchIdWithRunningGame;

        [Test]
        public void ThrowsException() => Assert.Throws<CashgameRunningException>(() => Execute());
    }
}