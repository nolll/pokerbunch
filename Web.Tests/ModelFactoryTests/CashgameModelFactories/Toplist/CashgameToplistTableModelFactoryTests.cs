using System.Collections.Generic;
using Core.Classes;
using Core.Repositories;
using Core.Services;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Toplist;
using Web.Models.CashgameModels.Toplist;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Toplist{

	public class CashgameToplistTableModelFactoryTests : MockContainer {

		private Homegame _homegame;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
		}

        [Test]
		public void Table_ItemModelsAreSet(){
			var totalResult = new FakeCashgameTotalResult();
			var totalResults = new List<CashgameTotalResult>{totalResult, totalResult};
            var suite = new FakeCashgameSuite(totalResults: totalResults);
            const int year = 0;

            var sut = GetSut();
            var result = sut.Create(_homegame, suite, year, ToplistSortOrder.winnings);

			Assert.AreEqual(2, result.ItemModels.Count);
		}

        private CashgameToplistTableModelFactory GetSut()
        {
			return new CashgameToplistTableModelFactory(
                GetMock<ICashgameToplistTableItemModelFactory>().Object,
                GetMock<IPlayerRepository>().Object,
                GetMock<IUrlProvider>().Object);
		}

	}

}