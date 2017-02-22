using Core.Exceptions;
using NUnit.Framework;

namespace Tests.Core.UseCases.AddCashgameFormTests
{
    public class WithCurrentGame : Arrange
    {
        protected override string BunchId => BunchIdWithRunningGame;

        [Test]
        public void AddCashgameOptions_WithRunningCashgame_ThrowsException()
        {
            Assert.Throws<CashgameRunningException>(() => Sut.Execute(Request));
        }
    }
}