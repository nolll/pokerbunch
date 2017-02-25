using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameListTests
{
    public class WithoutGames : Arrange
    {
        protected override string BunchId => BunchIdWithoutGames;

        [Test]
        public void HasEmptyListOfGames() => Assert.AreEqual(0, Result.List.Count);
    }
}