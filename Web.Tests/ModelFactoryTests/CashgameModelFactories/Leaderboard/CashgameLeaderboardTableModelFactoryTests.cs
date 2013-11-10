using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Leaderboard;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Leaderboard{

	public class CashgameLeaderboardTableModelFactoryTests : WebMockContainer {

		private Homegame _homegame;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
		}

        [Test]
		public void Table_ItemModelsAreSet(){
			var totalResult = new CashgameTotalResult();
			var totalResults = new List<CashgameTotalResult>{totalResult, totalResult};
			var suite = new CashgameSuite {TotalResults = totalResults};
            var sut = GetSut();
            var result = sut.Create(_homegame, suite);

			Assert.AreEqual(2, result.ItemModels.Count);
		}

        private CashgameLeaderboardTableModelFactory GetSut()
        {
			return new CashgameLeaderboardTableModelFactory(
                Mocks.CashgameLeaderboardTableItemModelFactoryMock.Object);
		}

	}

}