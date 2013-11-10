using System;
using System.Collections.Generic;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.Models.CashgameModels.Running;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Running{

	public class RunningCashgameTableModelFactoryTests : WebMockContainer{

		private Homegame _homegame;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
		}

		[Test]
        public void StatusModels_CashgameWithOnePlayer_ContainsOneItem(){
			var results = new List<CashgameResult>{new CashgameResult()};
            var cashgame = new FakeCashgame(results: results);

			var sut = GetSut();
		    var result = sut.Create(_homegame, cashgame, false);

			Assert.AreEqual(1, result.StatusModels.Count);
        }

		[Test]
        public void StatusModels_CashgameWithTwoPlayers_HasTwoItems(){
			var cashgame = new FakeCashgame(results: new List<CashgameResult>{new CashgameResult(), new CashgameResult()});

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, false);

			Assert.AreEqual(2, result.StatusModels.Count);
		}

		[Test]
        public void TotalBuyin_CashgameWithTwoPlayers_IsSumOfBuyins()
		{
		    const string formatted = "a";
		    const int turnover = 1;
			var cashgame = new FakeCashgame(turnover: 1);

            Mocks.GlobalizationMock.Setup(o => o.FormatCurrency(It.IsAny<CurrencySettings>(), turnover)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, false);

			Assert.AreEqual(formatted, result.TotalBuyin);
		}

		[Test]
        public void TotalStacks_CashgameWithTwoPlayers_IsSumOfCurrentStacks()
		{
		    const string formatted = "a";
		    const int totalStacks = 1;
            var cashgame = new FakeCashgame(totalStacks: totalStacks);

            Mocks.GlobalizationMock.Setup(o => o.FormatCurrency(It.IsAny<CurrencySettings>(), totalStacks)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, false);

            Assert.AreEqual(formatted, result.TotalStacks);
		}

		[Test]
        public void StatusModels_CashgameWithTwoPlayers_IsSortedByWinningsDescending()
		{
		    const string playerName1 = "a";
		    const string playerName2 = "b";
			var player1 = new Player {DisplayName = playerName1};
		    var result1 = new CashgameResult {Player = player1, Winnings = 1};
		    var player2 = new Player {DisplayName = playerName2};
		    var result2 = new CashgameResult {Player = player2, Winnings = 2};
            var cashgame = new FakeCashgame(startTime: new DateTime(), results: new List<CashgameResult>{result1, result2});

            Mocks.RunningCashgameTableItemModelFactoryMock.Setup(o => o.Create(_homegame, cashgame, result1, It.IsAny<bool>()))
                 .Returns(new RunningCashgameTableItemModel { Name = playerName1 });
            Mocks.RunningCashgameTableItemModelFactoryMock.Setup(o => o.Create(_homegame, cashgame, result2, It.IsAny<bool>()))
                 .Returns(new RunningCashgameTableItemModel { Name = playerName2 });

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, false);

			var results = result.StatusModels;
			Assert.AreEqual("b", results[0].Name);
			Assert.AreEqual("a", results[1].Name);
		}

		private RunningCashgameTableModelFactory GetSut(){
            return new RunningCashgameTableModelFactory(
                Mocks.RunningCashgameTableItemModelFactoryMock.Object,
                Mocks.GlobalizationMock.Object);
		}

	}

}