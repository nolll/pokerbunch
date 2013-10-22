using System;
using Core.Classes;
using Moq;
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
		public void TableItem_WithDuration_SetsDuration()
		{
		    const string formatted = "a";
			_cashgame.Duration = 1;

            Mocks.GlobalizationMock.Setup(o => o.FormatDuration(_cashgame.Duration)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

            Assert.AreEqual(formatted, result.Duration);
		}

		[Test]
		public void TableItem_SetsTurnover()
        {
            const string formatted = "a";
			_cashgame.Turnover = 1;

            Mocks.GlobalizationMock.Setup(o => o.FormatCurrency(It.IsAny<CurrencySettings>(), _cashgame.Turnover)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

            Assert.AreEqual(formatted, result.Turnover);
		}

		[Test]
		public void TableItem_SetsAvgBuyin()
		{
		    const string formatted = "a";
			_cashgame.AverageBuyin = 1;

            Mocks.GlobalizationMock.Setup(o => o.FormatCurrency(It.IsAny<CurrencySettings>(), _cashgame.AverageBuyin)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

            Assert.AreEqual(formatted, result.AvgBuyin);
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
		public void TableItem_SetsDisplayDate()
		{
		    const string formatted = "a";
		    var startTime = DateTime.Parse("2010-01-01 01:00:00");
			_cashgame.StartTime = startTime;

            Mocks.GlobalizationMock.Setup(o => o.FormatShortDate(startTime, _showYear)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

            Assert.AreEqual(formatted, result.DisplayDate);
		}

		[Test]
		public void TableItem_WithShowDateSetToTrue_SetsDisplayDate(){
            const string formatted = "a";
            _showYear = true;
		    var startTime = DateTime.Parse("2010-01-01 01:00:00");
			_cashgame.StartTime = startTime;

            Mocks.GlobalizationMock.Setup(o => o.FormatShortDate(startTime, _showYear)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _showYear);

            Assert.AreEqual(formatted, result.DisplayDate);
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
                Mocks.UrlProviderMock.Object,
                Mocks.GlobalizationMock.Object);
		}

	}

}