using System;
using Application.Services;
using Core.Entities;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.Models.UrlModels;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Matrix
{
    public class CashgameMatrixTableColumnHeaderModelFactoryTests : MockContainer
    {
        private Homegame _homegame;
        private bool _showYear;

        [SetUp]
        public void SetUp()
        {
            _homegame = new FakeHomegame();
            _showYear = false;
        }

        [Test]
        public void ColumnHeader_DateIsSet()
        {
            const string formatted = "a";
            var startTime = DateTime.Parse("2010-01-01");
            var cashgame = new FakeCashgame(startTime: startTime);

            GetMock<IGlobalization>().Setup(o => o.FormatShortDate(startTime, _showYear)).Returns(formatted);

            var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear);

            Assert.AreEqual(formatted, result.Date);
        }

        [Test]
        public void ColumnHeader_ShowYearIsTrue_DateWithYearIsSet()
        {
            const string formatted = "a";
            var startTime = DateTime.Parse("2010-01-01");
            var cashgame = new FakeCashgame(startTime: startTime);
            _showYear = true;

            GetMock<IGlobalization>().Setup(o => o.FormatShortDate(startTime, _showYear)).Returns(formatted);

            var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear);

            Assert.AreEqual(formatted, result.Date);
        }

        [Test]
        public void ColumnHeader_CashgameUrlIsSet()
        {
            var cashgame = new FakeCashgame(startTime: DateTime.Parse("2010-01-01"));

            var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear);

            Assert.IsInstanceOf<CashgameDetailsUrlModel>(result.CashgameUrl);
        }

        private CashgameMatrixTableColumnHeaderModelFactory GetSut()
        {
            return new CashgameMatrixTableColumnHeaderModelFactory(
                GetMock<IGlobalization>().Object);
        }
    }
}