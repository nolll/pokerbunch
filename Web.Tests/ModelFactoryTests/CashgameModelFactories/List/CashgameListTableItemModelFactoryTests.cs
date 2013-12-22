using System;
using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.List;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.List{

	public class CashgameListTableItemModelFactoryTests : MockContainer {

		private Homegame _homegame;
		private bool _showYear;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
			_showYear = false;
		}

        [Test]
		public void TableItem_SetsPlayerCount(){
			var cashgame = new FakeCashgame(playerCount: 2);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);

			Assert.AreEqual(2, result.PlayerCount);
		}

		[Test]
		public void TableItem_SetsLocation()
		{
		    const string location = "a";
            var cashgame = new FakeCashgame(location: location);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);

            Assert.AreEqual(location, result.Location);
		}

		[Test]
		public void TableItem_WithDuration_SetsDuration()
		{
		    const string formatted = "a";
		    const int duration = 1;
		    var cashgame = new FakeCashgame(duration: duration);

            GetMock<IGlobalization>().Setup(o => o.FormatDuration(duration)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);

            Assert.AreEqual(formatted, result.Duration);
		}

		[Test]
		public void TableItem_SetsTurnover()
        {
            const string formatted = "a";
		    const int turnover = 1;
            var cashgame = new FakeCashgame(turnover: turnover);

            GetMock<IGlobalization>().Setup(o => o.FormatCurrency(It.IsAny<CurrencySettings>(), turnover)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);

            Assert.AreEqual(formatted, result.Turnover);
		}

		[Test]
		public void TableItem_SetsAvgBuyin()
		{
		    const string formatted = "a";
		    const int averageBuyin = 1;
		    var cashgame = new FakeCashgame(averageBuyin: averageBuyin);

            GetMock<IGlobalization>().Setup(o => o.FormatCurrency(It.IsAny<CurrencySettings>(), averageBuyin)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);

            Assert.AreEqual(formatted, result.AvgBuyin);
		}

		[Test]
		public void TableItem_WithNoPlayers_DoesNotThrowDivisionByZeroException()
		{
		    var cashgame = new FakeCashgame(startTime: new DateTime());

			var sut = GetSut();
            sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);
		}

		[Test]
		public void TableItem_SetsDetailsUrl()
		{
		    const string detailsUrl = "a";
            var cashgame = new FakeCashgame();
		    GetMock<IUrlProvider>().Setup(o => o.GetCashgameDetailsUrl(_homegame.Slug, cashgame.DateString)).Returns(detailsUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);

            Assert.AreEqual(detailsUrl, result.DetailsUrl);
		}

		[Test]
		public void TableItem_SetsDisplayDate()
		{
		    const string formatted = "a";
		    var startTime = DateTime.Parse("2010-01-01 01:00:00");
            var cashgame = new FakeCashgame(startTime: startTime);

            GetMock<IGlobalization>().Setup(o => o.FormatShortDate(startTime, _showYear)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);

            Assert.AreEqual(formatted, result.DisplayDate);
		}

		[Test]
		public void TableItem_WithShowDateSetToTrue_SetsDisplayDate(){
            const string formatted = "a";
            _showYear = true;
		    var startTime = DateTime.Parse("2010-01-01 01:00:00");
            var cashgame = new FakeCashgame(startTime: startTime);

            GetMock<IGlobalization>().Setup(o => o.FormatShortDate(startTime, _showYear)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);

            Assert.AreEqual(formatted, result.DisplayDate);
		}

		private CashgameListTableItemModelFactory GetSut(){
			return new CashgameListTableItemModelFactory(
                GetMock<IUrlProvider>().Object,
                GetMock<IGlobalization>().Object);
		}

	}

}