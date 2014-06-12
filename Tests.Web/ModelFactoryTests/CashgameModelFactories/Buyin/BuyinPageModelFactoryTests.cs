using System.Collections.Generic;
using Core.Entities;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Buyin;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Buyin;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Buyin{

	class BuyinPageModelFactoryTests : MockContainer {

		private BuyinPostModel _postModel;

        [SetUp]
		public void SetUp(){
			_postModel = null;
		}

		[Test]
		public void StackFieldEnabled_WithPlayerInGame_IsTrue()
		{
		    const int playerId = 1;
            var player = new PlayerInTest(playerId);
		    var homegame = new HomegameInTest();
			var cashgameResult = new CashgameResultInTest(playerId);
		    var cashgame = new CashgameInTest(results: new List<CashgameResult>{cashgameResult});

			var sut = GetSut();
            var result = sut.Create(homegame, player, cashgame, null);

			Assert.IsTrue(result.StackFieldEnabled);
		}

		[Test]
		public void StackFieldEnabled_WithPlayerNotInGame_IsFalse(){
            var player = new PlayerInTest(2);
            var homegame = new HomegameInTest();
		    var cashgame = new CashgameInTest();

			var sut = GetSut();
            var result = sut.Create(homegame, player, cashgame, null);

			Assert.IsFalse(result.StackFieldEnabled);
		}

		[Test]
		public void BuyinAmount_WithoutPostedValue_IsSetToDefaultBuyin(){
            var player = new PlayerInTest();
            var homegame = new HomegameInTest(defaultBuyin: 1);
            var cashgame = new CashgameInTest();

			var sut = GetSut();
            var result = sut.Create(homegame, player, cashgame, null);

			Assert.AreEqual(1, result.BuyinAmount);
		}

		[Test]
		public void BuyinAmount_WithPostedValue_IsSetToPostedValue(){
            var player = new PlayerInTest();
            var homegame = new HomegameInTest(defaultBuyin: 1);
		    _postModel = new BuyinPostModel {BuyinAmount = 2};
            var cashgame = new CashgameInTest();

			var sut = GetSut();
            var result = sut.Create(homegame, player, cashgame, _postModel);

			Assert.AreEqual(2, result.BuyinAmount);
		}
        
        private BuyinPageModelFactory GetSut(){
            return new BuyinPageModelFactory(GetMock<IPagePropertiesFactory>().Object);
		}
		
	}

}