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
            var result = Execute(CreateRequest());

            Assert.AreEqual(2, result.GameItems.Count);
            Assert.AreEqual("2002-03-04", result.GameItems[0].Date.IsoString);
            Assert.AreEqual("/bunch-a/cashgame/details/2002-03-04", result.GameItems[0].Url.Relative);
            Assert.AreEqual("2001-02-03", result.GameItems[1].Date.IsoString);
            Assert.AreEqual("/bunch-a/cashgame/details/2001-02-03", result.GameItems[1].Url.Relative);
        }

        //todo: find a way to change the data in the repo
        //[Test]
        //public void Matrix_WithTwoGamesOnTheSameYear_SpansMultipleYearsIsFalse()
        //{
        //    var startTime1 = A.DateTime.WithDay(1).Build();
        //    var startTime2 = A.DateTime.WithDay(2).Build();
        //    SetupCashgamesAndPlayers(startTime1, startTime2);

        //    var result = Execute2(CreateRequest());

        //    Assert.IsFalse(result.SpansMultipleYears);
        //}

        [Test]
        public void Matrix_WithTwoGamesOnDifferentYears_SpansMultipleYearsIsTrue()
        {
            var result = Execute(CreateRequest());

            Assert.IsTrue(result.SpansMultipleYears);
        }

        private static MatrixRequest CreateRequest(int? year = null)
        {
            return new MatrixRequest(Constants.SlugA, year);
        }

        private MatrixResult Execute(MatrixRequest request)
        {
            return MatrixInteractor.Execute(
                Repos.Bunch,
                Repos.Cashgame,
                Repos.Player,
                request);
        }
    }
}
