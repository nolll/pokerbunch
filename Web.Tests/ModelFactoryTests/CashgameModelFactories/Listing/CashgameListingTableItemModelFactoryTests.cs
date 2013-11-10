using System;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Listing;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Listing{

	public class CashgameListingTableItemModelFactoryTests : WebMockContainer {

		private Homegame _homegame;
		private bool _showYear;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_showYear = false;
		}

        [Test]
		public void TableItem_SetsPlayerCount(){
			var cashgame = new FakeCashgame(playerCount: 2);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear);

			Assert.AreEqual(2, result.PlayerCount);
		}

		[Test]
		public void TableItem_SetsLocation()
		{
		    const string location = "a";
            var cashgame = new FakeCashgame(location: location);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear);

            Assert.AreEqual(location, result.Location);
		}

		[Test]
		public void TableItem_WithDuration_SetsDuration()
		{
		    const string formatted = "a";
		    const int duration = 1;
		    var cashgame = new FakeCashgame(duration: duration);

            Mocks.GlobalizationMock.Setup(o => o.FormatDuration(duration)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear);

            Assert.AreEqual(formatted, result.Duration);
		}

		[Test]
		public void TableItem_SetsTurnover()
        {
            const string formatted = "a";
		    const int turnover = 1;
            var cashgame = new FakeCashgame(turnover: turnover);

            Mocks.GlobalizationMock.Setup(o => o.FormatCurrency(It.IsAny<CurrencySettings>(), turnover)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear);

            Assert.AreEqual(formatted, result.Turnover);
		}

		[Test]
		public void TableItem_SetsAvgBuyin()
		{
		    const string formatted = "a";
		    const int averageBuyin = 1;
		    var cashgame = new FakeCashgame(averageBuyin: averageBuyin);

            Mocks.GlobalizationMock.Setup(o => o.FormatCurrency(It.IsAny<CurrencySettings>(), averageBuyin)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear);

            Assert.AreEqual(formatted, result.AvgBuyin);
		}

		[Test]
		public void TableItem_WithNoPlayers_DoesNotThrowDivisionByZeroException()
		{
		    var cashgame = new FakeCashgame(startTime: new DateTime());

			var sut = GetSut();
            sut.Create(_homegame, cashgame, _showYear);
		}

		[Test]
		public void TableItem_SetsDetailsUrl()
		{
		    const string detailsUrl = "a";
            var cashgame = new FakeCashgame();
		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameDetailsUrl(_homegame, cashgame)).Returns(detailsUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear);

            Assert.AreEqual(detailsUrl, result.DetailsUrl);
		}

		[Test]
		public void TableItem_SetsDisplayDate()
		{
		    const string formatted = "a";
		    var startTime = DateTime.Parse("2010-01-01 01:00:00");
            var cashgame = new FakeCashgame(startTime: startTime);

            Mocks.GlobalizationMock.Setup(o => o.FormatShortDate(startTime, _showYear)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear);

            Assert.AreEqual(formatted, result.DisplayDate);
		}

		[Test]
		public void TableItem_WithShowDateSetToTrue_SetsDisplayDate(){
            const string formatted = "a";
            _showYear = true;
		    var startTime = DateTime.Parse("2010-01-01 01:00:00");
            var cashgame = new FakeCashgame(startTime: startTime);

            Mocks.GlobalizationMock.Setup(o => o.FormatShortDate(startTime, _showYear)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear);

            Assert.AreEqual(formatted, result.DisplayDate);
		}

		[Test]
		public void TableItem_WithPublishedGame_SetsEmptyPublishedClass(){
            var cashgame = new FakeCashgame(status: GameStatus.Published);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear);

            Assert.AreEqual("", result.PublishedClass);
		}

		[Test]
		public void TableItem_WithUnpublishedGame_SetsPublishedClassToUnpublished(){
            var cashgame = new FakeCashgame(status: GameStatus.Finished);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear);

			Assert.AreEqual("unpublished", result.PublishedClass);
		}

		private CashgameListingTableItemModelFactory GetSut(){
			return new CashgameListingTableItemModelFactory(
                Mocks.UrlProviderMock.Object,
                Mocks.GlobalizationMock.Object);
		}

	}

}