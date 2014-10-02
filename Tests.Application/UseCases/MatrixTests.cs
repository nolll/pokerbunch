using System.Collections.Generic;
using Application.Urls;
using Application.UseCases.Matrix;
using Core.Entities;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.UseCases
{
    class MatrixTests : TestBase
    {
        [Test]
        public void Matrix_WithTwoGames_GameItemsAreCorrectAndSortedByDateDescending()
        {
            const string slug = "a";

            var bunch = A.Bunch.Build();
            var startTime1 = A.DateTime.WithDay(1).Build();
            var cashgame1 = A.Cashgame.WithStartTime(startTime1).Build();
            var startTime2 = A.DateTime.WithDay(2).Build();
            var cashgame2 = A.Cashgame.WithStartTime(startTime2).Build();
            var cashgames = new List<Cashgame> {cashgame1, cashgame2};

            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(slug)).Returns(bunch);
            GetMock<ICashgameRepository>().Setup(o => o.GetPublished(It.IsAny<Bunch>(), It.IsAny<int?>())).Returns(cashgames);

            var request = new MatrixRequest(slug);
            var result = Execute(request);

            Assert.AreEqual(2, result.GameItems.Count);
            Assert.AreEqual("2001-01-02", result.GameItems[0].Date.IsoString);
            Assert.IsInstanceOf<CashgameUrl>(result.GameItems[0].Url);
            Assert.AreEqual("2001-01-01", result.GameItems[1].Date.IsoString);
            Assert.IsInstanceOf<CashgameUrl>(result.GameItems[1].Url);
            
        }

        private MatrixResult Execute(MatrixRequest request)
        {
            return MatrixInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                request);
        }
    }
}
