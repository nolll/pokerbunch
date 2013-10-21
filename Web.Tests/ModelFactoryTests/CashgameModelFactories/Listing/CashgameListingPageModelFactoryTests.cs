using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Listing;
using Web.Models.CashgameModels.Listing;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Listing{

	public class CashgameListingPageModelFactoryTests : WebMockContainer {

        [Test]
		public void ListTableModel_IsSet()
        {
            var sut = GetSut();
			var result = sut.Create(new User(), new Homegame(), new List<Cashgame>(), null, null, null);

			Assert.IsInstanceOf<CashgameListingTableModel>(result.ListingTableModel);
		}

        private CashgameListingPageModelFactory GetSut()
        {
            return new CashgameListingPageModelFactory(
                Mocks.PagePropertiesFactoryMock.Object,
                Mocks.CashgameNavigationModelFactoryMock.Object);
        }

	}

}