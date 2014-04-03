using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Toplist;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Toplist;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Toplist{

	public class CashgameToplistPageModelFactoryTests : MockContainer {

        [Test]
		public void Create_SetsTableModel(){
			var homegame = new FakeHomegame();
			var suite = new FakeCashgameSuite();
            const int year = 1;

            GetMock<ICashgameToplistTableModelFactory>().Setup(o => o.Create(homegame, suite, year, ToplistSortOrder.winnings)).Returns(new CashgameToplistTableModel());

            var sut = GetSut();
            var result = sut.Create(homegame, suite, null, ToplistSortOrder.winnings, year);

            Assert.IsInstanceOf<CashgameToplistTableModel>(result.TableModel);
		}

        private CashgameToplistPageModelFactory GetSut()
        {
            return new CashgameToplistPageModelFactory(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<ICashgameToplistTableModelFactory>().Object,
                GetMock<ICashgamePageNavigationModelFactory>().Object,
                GetMock<ICashgameYearNavigationModelFactory>().Object);
        }

	}

}