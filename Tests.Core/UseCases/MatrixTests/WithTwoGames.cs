using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.MatrixTests
{
    public class WithTwoGames : Arrange
    {
        [Test]
        public void GameItemsAreCorrectAndSortedByDateDescending()
        {
            Assert.AreEqual(2, Result.GameItems.Count);
            Assert.AreEqual("2001-01-02", Result.GameItems[0].Date.IsoString);
            Assert.AreEqual(CashgameData.Id2, Result.GameItems[0].Id);
            Assert.AreEqual("2001-01-01", Result.GameItems[1].Date.IsoString);
            Assert.AreEqual(CashgameData.Id1, Result.GameItems[1].Id);
        }

        [Test]
        public void SpansMultipleYearsIsFalse()
        {
            Assert.IsFalse(Result.SpansMultipleYears);
        }
    }
}
