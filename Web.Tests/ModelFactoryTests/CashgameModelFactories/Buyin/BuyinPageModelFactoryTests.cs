using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Buyin;
using Web.Models.CashgameModels.Buyin;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Buyin{

	class BuyinPageModelFactoryTests : WebMockContainer {

		private Homegame _homegame;
		private Player _player;
		private BuyinPostModel _postModel;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_player = new Player();
			_postModel = null;
		}

		[Test]
		public void StackFieldEnabled_WithPlayerInGame_IsTrue(){
			_player.Id = 1;
			var cashgameResult = new CashgameResult {Player = _player};
		    var cashgame = new FakeCashgame(results: new List<CashgameResult>{cashgameResult});

			var sut = GetSut();
            var result = sut.Create(new User(), _homegame, _player, cashgame);

			Assert.IsTrue(result.StackFieldEnabled);
		}

		[Test]
		public void StackFieldEnabled_WithPlayerNotInGame_IsFalse(){
			_player.Id = 2;
		    var cashgame = new FakeCashgame();

			var sut = GetSut();
            var result = sut.Create(new User(), _homegame, _player, cashgame);

			Assert.IsFalse(result.StackFieldEnabled);
		}

		[Test]
		public void BuyinAmount_WithoutPostedValue_IsSetToDefaultBuyin(){
			_homegame.DefaultBuyin = 1;
            var cashgame = new FakeCashgame();

			var sut = GetSut();
            var result = sut.Create(new User(), _homegame, _player, cashgame);

			Assert.AreEqual(1, result.BuyinAmount);
		}

		[Test]
		public void BuyinAmount_WithPostedValue_IsSetToPostedValue(){
			_homegame.DefaultBuyin = 1;
		    _postModel = new BuyinPostModel {BuyinAmount = 2};
            var cashgame = new FakeCashgame();

			var sut = GetSut();
            var result = sut.Create(new User(), _homegame, _player, cashgame, _postModel);

			Assert.AreEqual(2, result.BuyinAmount);
		}
        
        private BuyinPageModelFactory GetSut(){
            return new BuyinPageModelFactory(Mocks.PagePropertiesFactoryMock.Object);
		}
		
	}

}