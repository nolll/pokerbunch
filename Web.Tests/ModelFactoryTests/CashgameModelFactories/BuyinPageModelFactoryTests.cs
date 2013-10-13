using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories;
using Web.Models.CashgameModels.Buyin;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories{

	class BuyinPageModelFactoryTests : MockContainer {

		private Homegame _homegame;
		private Player _player;
		private Cashgame _cashgame;
		private BuyinPostModel _postModel;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_player = new Player();
			_cashgame = new Cashgame();
			_postModel = null;
		}

		[Test]
		public void StackFieldEnabled_WithPlayerInGame_IsTrue(){
			_player.Id = 1;
			var cashgameResult = new CashgameResult {Player = _player};
		    _cashgame.Results = new List<CashgameResult>{cashgameResult};

			var sut = GetSut();
            var result = sut.Create(new User(), _homegame, _player, _cashgame);

			Assert.IsTrue(result.StackFieldEnabled);
		}

		[Test]
		public void StackFieldEnabled_WithPlayerNotInGame_IsFalse(){
			_player.Id = 2;

			var sut = GetSut();
            var result = sut.Create(new User(), _homegame, _player, _cashgame);

			Assert.IsFalse(result.StackFieldEnabled);
		}

		[Test]
		public void BuyinAmount_WithoutPostedValue_IsSetToDefaultBuyin(){
			_homegame.DefaultBuyin = 1;

			var sut = GetSut();
            var result = sut.Create(new User(), _homegame, _player, _cashgame);

			Assert.AreEqual(1, result.BuyinAmount);
		}

		[Test]
		public void BuyinAmount_WithPostedValue_IsSetToPostedValue(){
			_homegame.DefaultBuyin = 1;
		    _postModel = new BuyinPostModel {BuyinAmount = 2};

			var sut = GetSut();
            var result = sut.Create(new User(), _homegame, _player, _cashgame, _postModel);

			Assert.AreEqual(2, result.BuyinAmount);
		}
        
        private BuyinPageModelFactory GetSut(){
            return new BuyinPageModelFactory(WebMocks.PagePropertiesFactoryMock.Object);
		}
		
	}

}