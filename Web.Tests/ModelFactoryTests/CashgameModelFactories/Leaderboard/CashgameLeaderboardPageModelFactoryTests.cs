using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Leaderboard;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Leaderboard;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Leaderboard{

	public class CashgameLeaderboardPageModelFactoryTests : MockContainer {

        [Test]
		public void ActionLeaderboard_SetsTableModel(){
			var homegame = new FakeHomegame();
			var suite = new CashgameSuite();

            GetMock<ICashgameLeaderboardTableModelFactory>().Setup(o => o.Create(homegame, suite)).Returns(new CashgameLeaderboardTableModel());

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, suite, null, null);

            Assert.IsInstanceOf<CashgameLeaderboardTableModel>(result.TableModel);
		}

        private CashgameLeaderboardPageModelFactory GetSut()
        {
            return new CashgameLeaderboardPageModelFactory(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<ICashgameNavigationModelFactory>().Object,
                GetMock<ICashgameLeaderboardTableModelFactory>().Object);
        }

	}

}