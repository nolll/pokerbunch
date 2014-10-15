﻿using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Urls;
using Core.UseCases.Matrix;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class MatrixTests : TestBase
    {
        private const string Slug = "a";

        [Test]
        public void Matrix_WithTwoGames_GameItemsAreCorrectAndSortedByDateDescending()
        {
            var startTime1 = A.DateTime.WithDay(1).Build();
            var startTime2 = A.DateTime.WithDay(2).Build();
            SetupCashgamesAndPlayers(startTime1, startTime2);

            var result = Execute(CreateRequest());

            Assert.AreEqual(2, result.GameItems.Count);
            Assert.AreEqual("2001-01-02", result.GameItems[0].Date.IsoString);
            Assert.IsInstanceOf<CashgameUrl>(result.GameItems[0].Url);
            Assert.AreEqual("2001-01-01", result.GameItems[1].Date.IsoString);
            Assert.IsInstanceOf<CashgameUrl>(result.GameItems[1].Url);
        }

        [Test]
        public void Matrix_WithTwoGamesOnTheSameYear_SpansMultipleYearsIsFalse()
        {
            var startTime1 = A.DateTime.WithDay(1).Build();
            var startTime2 = A.DateTime.WithDay(2).Build();
            SetupCashgamesAndPlayers(startTime1, startTime2);

            var result = Execute(CreateRequest());

            Assert.IsFalse(result.SpansMultipleYears);
        }

        [Test]
        public void Matrix_WithTwoGamesOnDifferentYears_SpansMultipleYearsIsTrue()
        {
            var startTime1 = A.DateTime.WithYear(2011).Build();
            var startTime2 = A.DateTime.WithYear(2010).Build();
            SetupCashgamesAndPlayers(startTime1, startTime2);

            var result = Execute(CreateRequest());

            Assert.IsTrue(result.SpansMultipleYears);
        }

        private static MatrixRequest CreateRequest(int? year = null)
        {
            return new MatrixRequest(Slug, year);
        }

        private void SetupCashgamesAndPlayers(DateTime dt1, DateTime dt2)
        {
            var bunch = A.Bunch.Build();
            var cashgame1 = A.Cashgame.WithStartTime(dt1).Build();
            var cashgame2 = A.Cashgame.WithStartTime(dt2).Build();
            var cashgames = new List<Cashgame> { cashgame1, cashgame2 };
            var players = new List<Player>();

            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(Slug)).Returns(bunch);
            GetMock<ICashgameRepository>().Setup(o => o.GetPublished(It.IsAny<Bunch>(), It.IsAny<int?>())).Returns(cashgames);
            GetMock<IPlayerRepository>().Setup(o => o.GetList(It.IsAny<Bunch>())).Returns(players);
        }

        private MatrixResult Execute(MatrixRequest request)
        {
            return MatrixInteractor.Execute(
                GetMock<IBunchRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                GetMock<IPlayerRepository>().Object,
                request);
        }
    }
}