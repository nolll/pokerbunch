using System;
using Core.Classes;
using NUnit.Framework;
using Web.Models.CashgameModels.Matrix;
using Web.Models.UrlModels;

namespace Web.Tests.ModelTests.CashgameModels.Matrix{

	public class CashgameMatrixTableColumnHeaderModelTests {

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

			Assert.AreEqual("Jan 1", sut.Date);
		}

		[Test]
		public void ColumnHeader_ShowYearIsTrue_DateWithYearIsSet(){
			_cashgame.StartTime = DateTime.Parse("2010-01-01");
		    _showYear = true;

			var sut = GetSut();

			Assert.AreEqual("Jan 1 2010", sut.Date);
		}

		[Test]
		public void ColumnHeader_CashgameUrlIsSet(){
			_cashgame.StartTime = DateTime.Parse("2010-01-01");

			var sut = GetSut();

            Assert.IsInstanceOf<CashgameDetailsUrlModel>(sut.CashgameUrl);
		}

        private CashgameMatrixTableColumnHeaderModel GetSut()
        {
            return new CashgameMatrixTableColumnHeaderModel(_homegame, _cashgame, _showYear);
        }

	}

}