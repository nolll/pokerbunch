using System.Collections.Generic;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Listing;
using Web.Models.CashgameModels.Listing;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Listing{

	public class CashgameListingPageModelFactoryTests : WebMockContainer {

        [Test]
		public void ListTableModel_IsSet()
        {
            var homegame = new Homegame();
            var cashgames = new List<Cashgame>();
            Mocks.CashgameListingTableModelFactoryMock.Setup(o => o.Create(homegame, cashgames)).Returns(new CashgameListingTableModel());

            var sut = GetSut();
			var result = sut.Create(new User(), homegame, cashgames, null, null, null);

			Assert.IsInstanceOf<CashgameListingTableModel>(result.ListingTableModel);
		}

        private CashgameListingPageModelFactory GetSut()
        {
            return new CashgameListingPageModelFactory(
                Mocks.PagePropertiesFactoryMock.Object,
                Mocks.CashgameNavigationModelFactoryMock.Object,
                Mocks.CashgameListingTableModelFactoryMock.Object);
        }

	}

}