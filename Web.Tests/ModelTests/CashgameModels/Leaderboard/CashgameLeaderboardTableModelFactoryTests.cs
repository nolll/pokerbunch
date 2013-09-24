using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories;
using Web.Models.CashgameModels.Leaderboard;

namespace Web.Tests.ModelTests.CashgameModels.Leaderboard{

	public class CashgameLeaderboardTableModelFactoryTests : MockContainer {

        [Test]
		public void ActionLeaderboard_SetsTableModel(){
			var homegame = new Homegame();
			var suite = new CashgameSuite();

            var sut = new CashgameLeaderboardPageModelFactory(PagePropertiesFactoryMock.Object);
			var result = sut.Create(new User(), homegame, suite, null, null, null);

            Assert.IsInstanceOf<CashgameLeaderboardTableModel>(result.TableModel);
		}

	}

}