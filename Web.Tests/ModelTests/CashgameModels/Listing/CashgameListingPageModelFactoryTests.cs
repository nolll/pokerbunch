using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories;
using Web.Models.CashgameModels.Listing;

namespace Web.Tests.ModelTests.CashgameModels.Listing{

	public class CashgameListingPageModelFactoryTests : MockContainer {

        [Test]
		public void ListTableModel_IsSet()
        {
            var sut = new CashgameListingPageModelFactory(PagePropertiesFactoryMock.Object);
			var result = sut.Create(new User(), new Homegame(), new List<Cashgame>(), null, null, null);

			Assert.IsInstanceOf<CashgameListingTableModel>(result.ListingTableModel);
		}

	}

}