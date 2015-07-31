using Core.UseCases.Matrix;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class MatrixTests : TestBase
    {
        [Test]
        public void Matrix_WithTwoGames_GameItemsAreCorrectAndSortedByDateDescending()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.AreEqual(2, result.GameItems.Count);
            Assert.AreEqual("2002-02-02", result.GameItems[0].Date.IsoString);
            Assert.AreEqual("/bunch-a/cashgame/details/2002-02-02", result.GameItems[0].Url.Relative);
            Assert.AreEqual("2001-01-01", result.GameItems[1].Date.IsoString);
            Assert.AreEqual("/bunch-a/cashgame/details/2001-01-01", result.GameItems[1].Url.Relative);
        }

        [Test]
        public void Matrix_WithTwoGamesOnTheSameYear_SpansMultipleYearsIsFalse()
        {
            Repos.Cashgame.SetupSingleYear();

            var result = Sut.Execute(CreateRequest());

            Assert.IsFalse(result.SpansMultipleYears);
        }

        [Test]
        public void Matrix_WithTwoGamesOnDifferentYears_SpansMultipleYearsIsTrue()
        {
            var result = Sut.Execute(CreateRequest());

            Assert.IsTrue(result.SpansMultipleYears);
        }

        private static MatrixRequest CreateRequest(int? year = null)
        {
            return new MatrixRequest(TestData.SlugA, year);
        }

        private MatrixInteractor Sut
        {
            get
            {
                return new MatrixInteractor(
                    Repos.Bunch,
                    Repos.Cashgame,
                    Repos.Player);
            }
        }
    }
}
