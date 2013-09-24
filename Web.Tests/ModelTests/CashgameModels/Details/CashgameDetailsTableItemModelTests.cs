using System;
using Core.Classes;
using NUnit.Framework;
using Web.Models.CashgameModels.Details;
using Web.Models.UrlModels;

namespace Web.Tests.ModelTests.CashgameModels.Details{

	public class CashgameDetailsTableItemModelTests {

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

			Assert.AreEqual("a", sut.Name);
		}

		[Test]
        public void PlayerUrl_IsCorrectType(){
			_result.Player = new Player();

			var sut = GetSut();

			Assert.IsInstanceOf<CashgameActionUrlModel>(sut.PlayerUrl);
		}

		[Test]
        public void Buyin_IsSet(){
			_result.Buyin = 1;
			
            var sut = GetSut();

			Assert.AreEqual("$1", sut.Buyin);
		}

		[Test]
        public void Cashout_IsSet(){
			_result.Stack = 1;
			
            var sut = GetSut();

			Assert.AreEqual("$1", sut.Cashout);
		}

		[Test]
        public void Winnings_IsSet(){
			_result.Winnings = 1;
			
            var sut = GetSut();

			Assert.AreEqual("+$1", sut.Winnings);
		}

		[Test]
        public void WinningsClass_BuyinEqualToCashout_IsEmpty(){
			_result.Buyin = 100;
			_result.Stack = 100;

			var sut = GetSut();

			Assert.AreEqual("", sut.WinningsClass);
		}

		[Test]
        public void WinningsClass_ResultIsPositive_IsPositive(){
			_result.Winnings = 1;

			var sut = GetSut();

			Assert.AreEqual("pos-result", sut.WinningsClass);
		}

		[Test]
        public void WinningsClass_BuyinBiggerThanCashout_IsNegative(){
			_result.Winnings = -1;

			var sut = GetSut();

			Assert.AreEqual("neg-result", sut.WinningsClass);
		}

		[Test]
        public void Winrate_WithDuration_IsSet(){
			_result.PlayedTime = 60;
			_result.Winnings = 1;

			var sut = GetSut();

			Assert.AreEqual("$1/h", sut.Winrate);
		}

		[Test]
        public void Winrate_WithoutDuration_IsEmpty(){
			var sut = GetSut();

			Assert.AreEqual("", sut.Winrate);
		}

		private CashgameDetailsTableItemModel GetSut(){
			return new CashgameDetailsTableItemModel(_homegame, _cashgame, _result);
		}

	}

}