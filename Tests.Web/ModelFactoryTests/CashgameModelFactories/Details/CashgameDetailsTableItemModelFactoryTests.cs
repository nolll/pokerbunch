using System;
using Application.Services;
using Application.Urls;
using Core.Entities;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.Models.UrlModels;
using Web.Services;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Details{

	public class CashgameDetailsTableItemModelFactoryTests : MockContainer {

		private Homegame _homegame;
		private Cashgame _cashgame;
		
        [SetUp]
		public void SetUp(){
			_homegame = new HomegameInTest();
			_cashgame = new CashgameInTest(startTime: new DateTime());
        }

		[Test]
        public void Name_IsSet(){
			var player = new PlayerInTest(displayName: "a");
            var cashgameResult = new CashgameResultInTest();
		    
            var sut = GetSut();
		    var result = sut.Create(_homegame, _cashgame, player, cashgameResult);

			Assert.AreEqual("a", result.Name);
		}

		[Test]
        public void PlayerUrl_IsSet()
		{
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult);

			Assert.IsInstanceOf<CashgameActionUrl>(result.PlayerUrl);
		}

		[Test]
        public void Buyin_IsSet()
		{
		    const string formattedBuyin = "a";
		    const int buyin = 1;
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest(buyin: buyin);

            GetMock<IGlobalization>().Setup(o => o.FormatCurrency(It.IsAny<Currency>(), buyin)).Returns(formattedBuyin);

            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult);

			Assert.AreEqual(formattedBuyin, result.Buyin);
		}

		[Test]
        public void Cashout_IsSet(){
            const string formattedStack = "a";
            const int stack = 1;
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest(stack: stack);

            GetMock<IGlobalization>().Setup(o => o.FormatCurrency(It.IsAny<Currency>(), stack)).Returns(formattedStack);

            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult);

			Assert.AreEqual("a", result.Cashout);
		}

		[Test]
        public void Winnings_IsSet(){
            const string formattedWinnings = "a";
            const int winnings = 1;
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest(winnings: winnings);

            GetMock<IGlobalization>().Setup(o => o.FormatResult(It.IsAny<Currency>(), winnings)).Returns(formattedWinnings);

            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult);

			Assert.AreEqual(formattedWinnings, result.Winnings);
		}

		[Test]
        public void WinningsClass_IsSet(){
		    const string resultClass = "pos-result";
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest(winnings: 1);
		    
			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult);

			Assert.AreEqual(resultClass, result.WinningsClass);
		}

		[Test]
        public void Winrate_WithDuration_IsSet()
		{
		    const string formattedWinrate = "a";
		    const int winnings = 1;
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest(winnings: winnings, playedTime: 60);

            GetMock<IGlobalization>().Setup(o => o.FormatWinrate(It.IsAny<Currency>(), winnings)).Returns(formattedWinrate);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult);

			Assert.AreEqual(formattedWinrate, result.Winrate);
		}

		[Test]
        public void Winrate_WithoutDuration_IsEmpty(){
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult);

			Assert.AreEqual("", result.Winrate);
		}

		private CashgameDetailsTableItemModelFactory GetSut(){
            return new CashgameDetailsTableItemModelFactory(GetMock<IGlobalization>().Object);
		}

	}

}