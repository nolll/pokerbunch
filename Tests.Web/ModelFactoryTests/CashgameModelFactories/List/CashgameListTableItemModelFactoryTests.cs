using System;
using Application.Services;
using Core.Entities;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.List;
using Web.Models.UrlModels;
using Web.Services;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.List{

	public class CashgameListTableItemModelFactoryTests : MockContainer {

		private Homegame _homegame;
		private bool _showYear;

        [SetUp]
		public void SetUp(){
			_homegame = new HomegameInTest();
			_showYear = false;
		}

        [Test]
		public void TableItem_SetsPlayerCount(){
			var cashgame = new CashgameInTest(playerCount: 2);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);

			Assert.AreEqual(2, result.PlayerCount);
		}

		[Test]
		public void TableItem_SetsLocation()
		{
		    const string location = "a";
            var cashgame = new CashgameInTest(location: location);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);

            Assert.AreEqual(location, result.Location);
		}

		[Test]
		public void TableItem_WithDuration_SetsDuration()
		{
		    const string formatted = "a";
		    const int duration = 1;
		    var cashgame = new CashgameInTest(duration: duration);

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
            var cashgame = new CashgameInTest(turnover: turnover);

            GetMock<IGlobalization>().Setup(o => o.FormatCurrency(It.IsAny<Currency>(), turnover)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);

            Assert.AreEqual(formatted, result.Turnover);
		}

		[Test]
		public void TableItem_SetsAvgBuyin()
		{
		    const string formatted = "a";
		    const int averageBuyin = 1;
		    var cashgame = new CashgameInTest(averageBuyin: averageBuyin);

            GetMock<IGlobalization>().Setup(o => o.FormatCurrency(It.IsAny<Currency>(), averageBuyin)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);

            Assert.AreEqual(formatted, result.AvgBuyin);
		}

		[Test]
		public void TableItem_WithNoPlayers_DoesNotThrowDivisionByZeroException()
		{
		    var cashgame = new CashgameInTest(startTime: new DateTime());

			var sut = GetSut();
            sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);
		}

		[Test]
		public void TableItem_SetsDetailsUrl()
		{
            var cashgame = new CashgameInTest();

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);

            Assert.IsInstanceOf<CashgameDetailsUrlModel>(result.DetailsUrl);
		}

		[Test]
		public void TableItem_SetsDisplayDate()
		{
		    const string formatted = "a";
		    var startTime = DateTime.Parse("2010-01-01 01:00:00");
            var cashgame = new CashgameInTest(startTime: startTime);

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
            var cashgame = new CashgameInTest(startTime: startTime);

            GetMock<IGlobalization>().Setup(o => o.FormatShortDate(startTime, _showYear)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, _showYear, ListSortOrder.date);

            Assert.AreEqual(formatted, result.DisplayDate);
		}

		private CashgameListTableItemModelFactory GetSut(){
			return new CashgameListTableItemModelFactory(
                GetMock<IGlobalization>().Object);
		}

	}

}