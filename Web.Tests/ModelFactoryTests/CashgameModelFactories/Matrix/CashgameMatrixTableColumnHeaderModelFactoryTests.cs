using System;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Matrix;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Matrix{

	public class CashgameMatrixTableColumnHeaderModelFactoryTests : MockContainer
    {
        private Homegame _homegame;
	    private bool _showYear;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
            _showYear = false;
		}

        [Test]
		public void ColumnHeader_DateIsSet(){
            const string formatted = "a";
            var startTime = DateTime.Parse("2010-01-01");
            var cashgame = new FakeCashgame(startTime: startTime);

            Mocks.GlobalizationMock.Setup(o => o.FormatShortDate(startTime, _showYear)).Returns(formatted);

            var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear);

			Assert.AreEqual(formatted, result.Date);
		}

		[Test]
		public void ColumnHeader_ShowYearIsTrue_DateWithYearIsSet(){
            const string formatted = "a";
            var startTime = DateTime.Parse("2010-01-01");
            var cashgame = new FakeCashgame(startTime: startTime);
		    _showYear = true;

            Mocks.GlobalizationMock.Setup(o => o.FormatShortDate(startTime, _showYear)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear);

            Assert.AreEqual(formatted, result.Date);
		}

		[Test]
		public void ColumnHeader_CashgameUrlIsSet(){
            const string detailsUrl = "a";
            var cashgame = new FakeCashgame(startTime: DateTime.Parse("2010-01-01"));
            Mocks.UrlProviderMock.Setup(o => o.GetCashgameDetailsUrl(_homegame, cashgame)).Returns(detailsUrl);

			var sut = GetSut();
		    var result = sut.Create(_homegame, cashgame, _showYear);

            Assert.AreEqual(detailsUrl, result.CashgameUrl);
		}

        private CashgameMatrixTableColumnHeaderModelFactory GetSut()
        {
            return new CashgameMatrixTableColumnHeaderModelFactory(
                Mocks.UrlProviderMock.Object,
                Mocks.GlobalizationMock.Object);
        }

	}

}