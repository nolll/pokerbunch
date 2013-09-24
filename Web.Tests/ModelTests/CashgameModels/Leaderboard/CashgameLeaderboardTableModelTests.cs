using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Web.Models.CashgameModels.Leaderboard;

namespace Web.Tests.ModelTests.CashgameModels.Leaderboard{

	public class CashgameLeaderboardTableModelTests {

		private Homegame _homegame;
		private CashgameSuite _suite;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
		}

        [Test]
		public void Table_ItemModelsAreSet(){
			var totalResult = new CashgameTotalResult();
			var totalResults = new List<CashgameTotalResult>{totalResult, totalResult};
			_suite = new CashgameSuite {TotalResults = totalResults};
            var sut = GetSut();

			Assert.AreEqual(2, sut.ItemModels.Count);
            Assert.IsInstanceOf<CashgameLeaderboardTableItemModel>(sut.ItemModels[0]);
		}

		private CashgameLeaderboardTableModel GetSut(){
			return new CashgameLeaderboardTableModel(_homegame, _suite);
		}

	}

}