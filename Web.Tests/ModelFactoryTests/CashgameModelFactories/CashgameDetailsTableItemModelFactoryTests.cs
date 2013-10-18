using System;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories;
using Web.Models.UrlModels;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories{

	public class CashgameDetailsTableItemModelFactoryTests : WebMockContainer {

		private Homegame _homegame;
		private Cashgame _cashgame;
		private CashgameResult _result;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_cashgame = new Cashgame {StartTime = new DateTime()};
            _result = new CashgameResult();
		}

		[Test]
        public void Name_IsSet(){
			var player = new Player {DisplayName = "a"};
		    _result.Player = player;
			
            var sut = GetSut();
		    var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual("a", result.Name);
		}

		[Test]
        public void PlayerUrl_IsSet()
		{
		    const string playerUrl = "a";
            var player = new Player();
		    _result.Player = player;

		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameActionUrl(_homegame, _cashgame, player)).Returns(playerUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual(playerUrl, result.PlayerUrl);
		}

		[Test]
        public void Buyin_IsSet(){
			_result.Buyin = 1;
			
            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual("$1", result.Buyin);
		}

		[Test]
        public void Cashout_IsSet(){
			_result.Stack = 1;
			
            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual("$1", result.Cashout);
		}

		[Test]
        public void Winnings_IsSet(){
			_result.Winnings = 1;
			
            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual("+$1", result.Winnings);
		}

		[Test]
        public void WinningsClass_BuyinEqualToCashout_IsEmpty(){
			_result.Buyin = 100;
			_result.Stack = 100;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual("", result.WinningsClass);
		}

		[Test]
        public void WinningsClass_ResultIsPositive_IsPositive(){
			_result.Winnings = 1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual("pos-result", result.WinningsClass);
		}

		[Test]
        public void WinningsClass_BuyinBiggerThanCashout_IsNegative(){
			_result.Winnings = -1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual("neg-result", result.WinningsClass);
		}

		[Test]
        public void Winrate_WithDuration_IsSet(){
			_result.PlayedTime = 60;
			_result.Winnings = 1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual("$1/h", result.Winrate);
		}

		[Test]
        public void Winrate_WithoutDuration_IsEmpty(){
			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual("", result.Winrate);
		}

		private CashgameDetailsTableItemModelFactory GetSut(){
            return new CashgameDetailsTableItemModelFactory(Mocks.UrlProviderMock.Object);
		}

	}

}