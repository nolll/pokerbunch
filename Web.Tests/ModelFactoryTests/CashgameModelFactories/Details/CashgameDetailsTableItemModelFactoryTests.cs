using System;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Details;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Details{

	public class CashgameDetailsTableItemModelFactoryTests : WebMockContainer {

		private Homegame _homegame;
		private Cashgame _cashgame;
		private CashgameResult _result;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_cashgame = new FakeCashgame(startTime: new DateTime());
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
        public void Buyin_IsSet()
		{
		    const string formattedBuyin = "a";
			_result.Buyin = 1;

            Mocks.GlobalizationMock.Setup(o => o.FormatCurrency(It.IsAny<CurrencySettings>(), _result.Buyin)).Returns(formattedBuyin);

            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual(formattedBuyin, result.Buyin);
		}

		[Test]
        public void Cashout_IsSet(){
            const string formattedStack = "a";
            _result.Stack = 1;

            Mocks.GlobalizationMock.Setup(o => o.FormatCurrency(It.IsAny<CurrencySettings>(), _result.Stack)).Returns(formattedStack);

            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual("a", result.Cashout);
		}

		[Test]
        public void Winnings_IsSet(){
            const string formattedWinnings = "a";
            _result.Winnings = 1;

            Mocks.GlobalizationMock.Setup(o => o.FormatResult(It.IsAny<CurrencySettings>(), _result.Winnings)).Returns(formattedWinnings);

            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual(formattedWinnings, result.Winnings);
		}

		[Test]
        public void WinningsClass_IsSet(){
		    const string resultClass = "a";
		    
            Mocks.ResultFormatterMock.Setup(o => o.GetWinningsCssClass(_result.Winnings)).Returns(resultClass);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual(resultClass, result.WinningsClass);
		}

		[Test]
        public void Winrate_WithDuration_IsSet()
		{
		    const string formattedWinrate = "a";
			_result.PlayedTime = 60;
			_result.Winnings = 1;

            Mocks.GlobalizationMock.Setup(o => o.FormatWinrate(It.IsAny<CurrencySettings>(), _result.Winnings)).Returns(formattedWinrate);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual(formattedWinrate, result.Winrate);
		}

		[Test]
        public void Winrate_WithoutDuration_IsEmpty(){
			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result);

			Assert.AreEqual("", result.Winrate);
		}

		private CashgameDetailsTableItemModelFactory GetSut(){
            return new CashgameDetailsTableItemModelFactory(
                Mocks.UrlProviderMock.Object,
                Mocks.ResultFormatterMock.Object,
                Mocks.GlobalizationMock.Object);
		}

	}

}