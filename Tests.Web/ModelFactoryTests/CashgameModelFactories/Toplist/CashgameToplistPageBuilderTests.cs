using Application.Services;
using Core.Repositories;
using Core.Services.Interfaces;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Toplist;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Toplist;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Toplist{

	public class CashgameToplistPageBuilderTests : MockContainer {

        [Test]
		public void Create_SetsTableModel()
        {
            const string slug = "a";
			var homegame = new FakeHomegame();
			var suite = new FakeCashgameSuite();
            const int year = 1;

            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(slug)).Returns(homegame);
            GetMock<ICashgameService>().Setup(o => o.GetSuite(homegame, year)).Returns(suite);
            GetMock<ICashgameToplistTableModelFactory>().Setup(o => o.Create(homegame, suite, year, ToplistSortOrder.winnings)).Returns(new CashgameToplistTableModel());

            var sut = GetSut();
            var result = sut.Build(slug, null, year);

            Assert.IsInstanceOf<CashgameToplistTableModel>(result.TableModel);
		}

        private CashgameToplistPageBuilder GetSut()
        {
            return new CashgameToplistPageBuilder(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<ICashgameToplistTableModelFactory>().Object,
                GetMock<ICashgamePageNavigationModelFactory>().Object,
                GetMock<ICashgameYearNavigationModelFactory>().Object,
                GetMock<IHomegameRepository>().Object,
                GetMock<ICashgameRepository>().Object,
                GetMock<ICashgameService>().Object);
        }

	}

}