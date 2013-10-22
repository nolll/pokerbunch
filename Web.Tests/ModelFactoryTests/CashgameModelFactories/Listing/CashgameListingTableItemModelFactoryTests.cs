using System;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Listing;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Listing{

	public class CashgameListingTableItemModelFactoryTests : WebMockContainer {

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
		public void TableItem_SetsPlayerCount(){
			_cashgame.PlayerCount = 2;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

			Assert.AreEqual(2, result.PlayerCount);
		}

		[Test]
		public void TableItem_SetsLocation(){
			_cashgame.Location = "a";

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

            Assert.AreEqual("a", result.Location);
		}

		[Test]
		public void TableItem_WithDuration_SetsDuration(){
			_cashgame.Duration = 1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

            Assert.AreEqual("1m", result.Duration);
		}

		[Test]
		public void TableItem_SetsTurnover(){
			_cashgame.Turnover = 1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

            Assert.AreEqual("$1", result.Turnover);
		}

		[Test]
		public void TableItem_SetsAvgBuyin(){
			_cashgame.AverageBuyin = 1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

            Assert.AreEqual("$1", result.AvgBuyin);
		}

		[Test]
		public void TableItem_WithNoPlayers_DoesNotThrowDivisionByZeroException(){
			_cashgame.StartTime = new DateTime();

			var sut = GetSut();
            sut.Create(_homegame, _cashgame, _showYear);
		}

		[Test]
		public void TableItem_SetsDetailsUrl()
		{
		    const string detailsUrl = "a";
		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameDetailsUrl(_homegame, _cashgame)).Returns(detailsUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

            Assert.AreEqual(detailsUrl, result.DetailsUrl);
		}

		[Test]
		public void TableItem_SetsDisplayDate(){
			_cashgame.StartTime = DateTime.Parse("2010-01-01 01:00:00");

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

            Assert.AreEqual("Jan 1", result.DisplayDate);
		}

		[Test]
		public void TableItem_WithShowDateSetToTrue_SetsDisplayDate(){
			_showYear = true;
			_cashgame.StartTime = DateTime.Parse("2010-01-01 01:00:00");

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

            Assert.AreEqual("Jan 1 2010", result.DisplayDate);
		}

		[Test]
		public void TableItem_WithPublishedGame_SetsEmptyPublishedClass(){
			_cashgame.Status = GameStatus.Published;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

            Assert.AreEqual("", result.PublishedClass);
		}

		[Test]
		public void TableItem_WithUnpublishedGame_SetsPublishedClassToUnpublished(){
			_cashgame.Status = GameStatus.Finished;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

			Assert.AreEqual("unpublished", result.PublishedClass);
		}

		private CashgameListingTableItemModelFactory GetSut(){
			return new CashgameListingTableItemModelFactory(
                Mocks.UrlProviderMock.Object);
		}

	}

}