using System.Linq;
using Core.UseCases;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameContextTests
{
    public class WithSelectedOverviewPage : Arrange
    {
        [Test]
        public void GameIsRunningIsFalse()
        {
            var result = Sut.Execute(Request);

            Assert.IsFalse(result.GameIsRunning);
        }

        [Test]
        public void BunchContextIsSet()
        {
            var result = Sut.Execute(Request);

            Assert.IsInstanceOf<BunchContext.Result>(result.BunchContext);
        }

        [Test]
        public void GameIsRunningGameIsFalse()
        {
            var result = Sut.Execute(Request);

            Assert.IsFalse(result.GameIsRunning);
        }

        [Test]
        public void YearsAreOrderedDescending()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(LastYear, result.Years.First());
            Assert.AreEqual(FirstYear, result.Years.Last());
        }
    }
}