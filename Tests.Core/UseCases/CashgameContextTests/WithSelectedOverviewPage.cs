using System.Linq;
using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public class WithSelectedOverviewPage : Arrange
    {
        [Test]
        public void GameIsRunningIsFalse() => Assert.IsFalse(Result.GameIsRunning);

        [Test]
        public void BunchContextIsSet() => Assert.IsInstanceOf<BunchContext.Result>(Result.BunchContext);

        [Test]
        public void GameIsRunningGameIsFalse() => Assert.IsFalse(Result.GameIsRunning);

        [Test]
        public void YearsAreOrderedDescending()
        {
            Assert.AreEqual(LastYear, Result.Years.First());
            Assert.AreEqual(FirstYear, Result.Years.Last());
        }
    }
}