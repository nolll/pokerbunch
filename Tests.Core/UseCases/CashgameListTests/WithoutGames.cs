using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameListTests
{
    public class WithoutGames : Arrange
    {
        protected override string BunchId => BunchIdWithoutGames;

        [Test]
        public void HasEmptyListOfGames()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(0, result.List.Count);
        }
    }
}