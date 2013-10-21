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
			_cashgame.StartTime = DateTime.Parse("2010-01-01");

            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

			Assert.AreEqual("Jan 1", result.Date);
		}

		[Test]
		public void ColumnHeader_ShowYearIsTrue_DateWithYearIsSet(){
			_cashgame.StartTime = DateTime.Parse("2010-01-01");
		    _showYear = true;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

			Assert.AreEqual("Jan 1 2010", result.Date);
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
                Mocks.UrlProviderMock.Object);
        }

	}

}