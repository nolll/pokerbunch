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
		public void Create_SetsTableModel(){
			var homegame = new FakeHomegame();
			var suite = new FakeCashgameSuite();
            const int year = 1;

            GetMock<ICashgameLeaderboardTableModelFactory>().Setup(o => o.Create(homegame, suite, year, LeaderboardSortOrder.winnings)).Returns(new CashgameLeaderboardTableModel());

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, suite, null, LeaderboardSortOrder.winnings, year);

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