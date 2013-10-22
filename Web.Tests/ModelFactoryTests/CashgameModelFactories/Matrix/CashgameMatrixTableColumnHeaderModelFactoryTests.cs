using System;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Matrix;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Matrix{

	public class CashgameMatrixTableColumnHeaderModelFactoryTests : WebMockContainer
    {
        private Homegame _homegame;
		private Cashgame _cashgame;
	    private bool _showYear;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_cashgame = new Cashgame();
            _showYear = false;
		}

        [Test]
		public void ColumnHeader_DateIsSet(){
            const string formatted = "a";
            var startTime = DateTime.Parse("2010-01-01");
            _cashgame.StartTime = startTime;

            Mocks.GlobalizationMock.Setup(o => o.FormatShortDate(startTime, _showYear)).Returns(formatted);

            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

			Assert.AreEqual(formatted, result.Date);
		}

		[Test]
		public void ColumnHeader_ShowYearIsTrue_DateWithYearIsSet(){
            const string formatted = "a";
            var startTime = DateTime.Parse("2010-01-01");
            _cashgame.StartTime = startTime;
		    _showYear = true;

            Mocks.GlobalizationMock.Setup(o => o.FormatShortDate(startTime, _showYear)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

            Assert.AreEqual(formatted, result.Date);
		}

		[Test]
		public void ColumnHeader_CashgameUrlIsSet(){
			_cashgame.StartTime = DateTime.Parse("2010-01-01");
		    const string detailsUrl = "a";
		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameDetailsUrl(_homegame, _cashgame)).Returns(detailsUrl);

			var sut = GetSut();
		    var result = sut.Create(_homegame, _cashgame, _showYear);

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