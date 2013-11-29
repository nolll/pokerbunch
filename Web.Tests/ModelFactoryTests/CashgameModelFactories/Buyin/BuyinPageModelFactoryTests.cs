using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Buyin;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Buyin;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Buyin{

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
            var player = new FakePlayer(playerId);
		    var homegame = new FakeHomegame();
			var cashgameResult = new FakeCashgameResult(player, playerId);
		    var cashgame = new FakeCashgame(results: new List<CashgameResult>{cashgameResult});

			var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, player, cashgame);

			Assert.IsTrue(result.StackFieldEnabled);
		}

		[Test]
		public void StackFieldEnabled_WithPlayerNotInGame_IsFalse(){
            var player = new FakePlayer(2);
            var homegame = new FakeHomegame();
		    var cashgame = new FakeCashgame();

			var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, player, cashgame);

			Assert.IsFalse(result.StackFieldEnabled);
		}

		[Test]
		public void BuyinAmount_WithoutPostedValue_IsSetToDefaultBuyin(){
            var player = new FakePlayer();
            var homegame = new FakeHomegame(defaultBuyin: 1);
            var cashgame = new FakeCashgame();

			var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, player, cashgame);

			Assert.AreEqual(1, result.BuyinAmount);
		}

		[Test]
		public void BuyinAmount_WithPostedValue_IsSetToPostedValue(){
            var player = new FakePlayer();
            var homegame = new FakeHomegame(defaultBuyin: 1);
		    _postModel = new BuyinPostModel {BuyinAmount = 2};
            var cashgame = new FakeCashgame();

			var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, player, cashgame, _postModel);

			Assert.AreEqual(2, result.BuyinAmount);
		}
        
        private BuyinPageModelFactory GetSut(){
            return new BuyinPageModelFactory(GetMock<IPagePropertiesFactory>().Object);
		}
		
	}

}