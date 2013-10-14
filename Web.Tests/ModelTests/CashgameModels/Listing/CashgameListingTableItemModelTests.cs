using System;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.Models.CashgameModels.Listing;
using Web.Models.UrlModels;

namespace Web.Tests.ModelTests.CashgameModels.Listing{

	public class CashgameListingTableItemModelTests : WebMockContainer {

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

			Assert.AreEqual(2, sut.PlayerCount);
		}

		[Test]
		public void TableItem_SetsLocation(){
			_cashgame.Location = "a";

			var sut = GetSut();

			Assert.AreEqual("a", sut.Location);
		}

		[Test]
		public void TableItem_WithDuration_SetsDuration(){
			_cashgame.Duration = 1;

			var sut = GetSut();

			Assert.AreEqual("1m", sut.Duration);
		}

		[Test]
		public void TableItem_SetsTurnover(){
			_cashgame.Turnover = 1;

			var sut = GetSut();

			Assert.AreEqual("$1", sut.Turnover);
		}

		[Test]
		public void TableItem_SetsAvgBuyin(){
			_cashgame.AverageBuyin = 1;

			var sut = GetSut();

			Assert.AreEqual("$1", sut.AvgBuyin);
		}

		[Test]
		public void TableItem_WithNoPlayers_DoesNotThrowDivisionByZeroException(){
			_cashgame.StartTime = new DateTime();

			GetSut();
		}

		[Test]
		public void TableItem_SetsDetailsUrl(){
			var sut = GetSut();

			Assert.IsInstanceOf<CashgameDetailsUrlModel>(sut.DetailsUrl);
		}

		[Test]
		public void TableItem_SetsDisplayDate(){
			_cashgame.StartTime = DateTime.Parse("2010-01-01 01:00:00");

			var sut = GetSut();

			Assert.AreEqual("Jan 1", sut.DisplayDate);
		}

		[Test]
		public void TableItem_WithShowDateSetToTrue_SetsDisplayDate(){
			_showYear = true;
			_cashgame.StartTime = DateTime.Parse("2010-01-01 01:00:00");

			var sut = GetSut();

			Assert.AreEqual("Jan 1 2010", sut.DisplayDate);
		}

		[Test]
		public void TableItem_WithPublishedGame_SetsEmptyPublishedClass(){
			_cashgame.Status = GameStatus.Published;

			var sut = GetSut();

			Assert.AreEqual("", sut.PublishedClass);
		}

		[Test]
		public void TableItem_WithUnpublishedGame_SetsPublishedClassToUnpublished(){
			_cashgame.Status = GameStatus.Finished;

			var sut = GetSut();

			Assert.AreEqual("unpublished", sut.PublishedClass);
		}

		private CashgameListingTableItemModel GetSut(){
			return new CashgameListingTableItemModel(_homegame, _cashgame, _showYear);
		}

	}

}