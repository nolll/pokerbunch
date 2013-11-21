using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Listing;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Listing;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Listing{

	public class CashgameListingPageModelFactoryTests : MockContainer {

        [Test]
		public void ListTableModel_IsSet()
        {
            var homegame = new FakeHomegame();
            var cashgames = new List<Cashgame>();
            GetMock<ICashgameListingTableModelFactory>().Setup(o => o.Create(homegame, cashgames)).Returns(new CashgameListingTableModel());

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgames, null, null);

			Assert.IsInstanceOf<CashgameListingTableModel>(result.ListingTableModel);
		}

        private CashgameListingPageModelFactory GetSut()
        {
            return new CashgameListingPageModelFactory(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<ICashgameNavigationModelFactory>().Object,
                GetMock<ICashgameListingTableModelFactory>().Object);
        }

	}

}