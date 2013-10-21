using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories;
using Web.Models.CashgameModels.Leaderboard;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories{

	public class CashgameLeaderboardTableModelFactoryTests : WebMockContainer {

        [Test]
		public void ActionLeaderboard_SetsTableModel(){
			var homegame = new Homegame();
			var suite = new CashgameSuite();

            var sut = GetSut();
			var result = sut.Create(new User(), homegame, suite, null, null, null);

            Assert.IsInstanceOf<CashgameLeaderboardTableModel>(result.TableModel);
		}

        private CashgameLeaderboardPageModelFactory GetSut()
        {
            return new CashgameLeaderboardPageModelFactory(
                Mocks.PagePropertiesFactoryMock.Object,
                Mocks.CashgameNavigationModelFactoryMock.Object);
        }

	}

}