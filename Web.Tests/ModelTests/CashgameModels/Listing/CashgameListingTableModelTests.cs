using System;
using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Web.Models.CashgameModels.Listing;

namespace Web.Tests.ModelTests.CashgameModels.Listing{

	public class CashgameListingTableModelTests {

		private Homegame _homegame;
		private List<Cashgame> _cashgames;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_cashgames = new List<Cashgame>();
		}

		[Test]
		public void Table_WithOneCashgame_OneItemIsCorrectType(){
			_cashgames = GetCashgames();

			var sut = GetSut();

            Assert.IsInstanceOf<CashgameListingTableItemModel>(sut.ListItemModels[0]);
			Assert.AreEqual(3, sut.ListItemModels.Count);
		}

		private CashgameListingTableModel GetSut(){
			return new CashgameListingTableModel(_homegame, _cashgames);
		}

		private List<Cashgame> GetCashgames(){
			var cashgame1 = new Cashgame {Status = GameStatus.Finished, StartTime = DateTime.Parse("2010-01-01 01:00:00")};
		    var cashgame2 = new Cashgame {Status = GameStatus.Published, StartTime = DateTime.Parse("2010-01-02 01:00:00")};
		    var cashgame3 = new Cashgame {Status = GameStatus.Published, StartTime = DateTime.Parse("2011-01-01 01:00:00")};
		    return new List<Cashgame>{cashgame1, cashgame2, cashgame3};
		}

	}

}