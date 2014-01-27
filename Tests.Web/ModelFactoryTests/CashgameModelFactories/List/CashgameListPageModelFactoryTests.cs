using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.List;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.List;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.List{

	public class CashgameListPageModelFactoryTests : MockContainer {

        [Test]
		public void ListTableModel_IsSet()
        {
            var homegame = new FakeHomegame();
            var cashgames = new List<Cashgame>();
            GetMock<ICashgameListTableModelFactory>().Setup(o => o.Create(homegame, cashgames, ListSortOrder.date, null)).Returns(new CashgameListTableModel());

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgames, null, ListSortOrder.date, null);

			Assert.IsInstanceOf<CashgameListTableModel>(result.ListTableModel);
		}

        private CashgameListPageModelFactory GetSut()
        {
            return new CashgameListPageModelFactory(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<ICashgameListTableModelFactory>().Object,
                GetMock<ICashgamePageNavigationModelFactory>().Object,
                GetMock<ICashgameYearNavigationModelFactory>().Object);
        }

	}

}