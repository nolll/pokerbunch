using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Leaderboard;
using Web.Models.CashgameModels.Leaderboard;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Leaderboard{

	public class CashgameLeaderboardPageModelFactoryTests : WebMockContainer {

        [Test]
		public void ActionLeaderboard_SetsTableModel(){
			var homegame = new FakeHomegame();
			var suite = new CashgameSuite();

            Mocks.CashgameLeaderboardTableModelFactoryMock.Setup(o => o.Create(homegame, suite)).Returns(new CashgameLeaderboardTableModel());

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, suite, null, null, null);

            Assert.IsInstanceOf<CashgameLeaderboardTableModel>(result.TableModel);
		}

        private CashgameLeaderboardPageModelFactory GetSut()
        {
            return new CashgameLeaderboardPageModelFactory(
                Mocks.PagePropertiesFactoryMock.Object,
                Mocks.CashgameNavigationModelFactoryMock.Object,
                Mocks.CashgameLeaderboardTableModelFactoryMock.Object);
        }

	}

}