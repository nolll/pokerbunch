using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.MatrixTests
{
    public class WithTwoGames : Arrange
    {
        [Test]
        public void GameItemsAreCorrectAndSortedByDateDescending()
        {
            var result = Sut.Execute(Request);

            Assert.AreEqual(2, result.GameItems.Count);
            Assert.AreEqual("2001-01-02", result.GameItems[0].Date.IsoString);
            Assert.AreEqual(CashgameData.Id2, result.GameItems[0].Id);
            Assert.AreEqual("2001-01-01", result.GameItems[1].Date.IsoString);
            Assert.AreEqual(CashgameData.Id1, result.GameItems[1].Id);
        }

        [Test]
        public void SpansMultipleYearsIsFalse()
        {
            var result = Sut.Execute(Request);

            Assert.IsFalse(result.SpansMultipleYears);
        }
    }
}
