using System;
using System.Collections.Generic;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Listing;
using Web.Models.CashgameModels.Listing;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Listing{

	public class CashgameListingTableModelFactoryTests : MockContainer {

		private Homegame _homegame;
		private List<Cashgame> _cashgames;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
			_cashgames = new List<Cashgame>();
		}

		[Test]
		public void Table_WithOneCashgame_OneItemIsCorrectType(){
            Mocks.CashgameListingTableItemModelFactoryMock.Setup(o => o.Create(_homegame, It.IsAny<Cashgame>(), It.IsAny<bool>())).Returns(new CashgameListingTableItemModel());

			_cashgames = GetCashgames();

			var sut = GetSut();
		    var result = sut.Create(_homegame, _cashgames);

            Assert.IsInstanceOf<CashgameListingTableItemModel>(result.ListItemModels[0]);
			Assert.AreEqual(3, result.ListItemModels.Count);
		}

		private CashgameListingTableModelFactory GetSut(){
			return new CashgameListingTableModelFactory(
                Mocks.CashgameListingTableItemModelFactoryMock.Object);
		}

		private List<Cashgame> GetCashgames(){
            var cashgame1 = new FakeCashgame(status: GameStatus.Finished, startTime: DateTime.Parse("2010-01-01 01:00:00"));
            var cashgame2 = new FakeCashgame(status: GameStatus.Published, startTime: DateTime.Parse("2010-01-02 01:00:00"));
            var cashgame3 = new FakeCashgame(status: GameStatus.Published, startTime: DateTime.Parse("2011-01-01 01:00:00"));
		    return new List<Cashgame>{cashgame1, cashgame2, cashgame3};
		}

	}

}