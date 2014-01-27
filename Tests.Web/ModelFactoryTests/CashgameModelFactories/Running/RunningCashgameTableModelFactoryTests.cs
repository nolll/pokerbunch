using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Repositories;
using Infrastructure.System;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.Models.CashgameModels.Running;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Running{

	public class RunningCashgameTableModelFactoryTests : MockContainer{

		private Homegame _homegame;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
		}

		[Test]
        public void StatusModels_CashgameWithOnePlayer_ContainsOneItem(){
			var results = new List<CashgameResult>{new FakeCashgameResult()};
            var cashgame = new FakeCashgame(results: results);

			var sut = GetSut();
		    var result = sut.Create(_homegame, cashgame, false);

			Assert.AreEqual(1, result.StatusModels.Count);
        }

		[Test]
        public void StatusModels_CashgameWithTwoPlayers_HasTwoItems(){
			var cashgame = new FakeCashgame(results: new List<CashgameResult>{new FakeCashgameResult(), new FakeCashgameResult()});

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

            GetMock<IGlobalization>().Setup(o => o.FormatCurrency(It.IsAny<CurrencySettings>(), turnover)).Returns(formatted);

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

            GetMock<IGlobalization>().Setup(o => o.FormatCurrency(It.IsAny<CurrencySettings>(), totalStacks)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, false);

            Assert.AreEqual(formatted, result.TotalStacks);
		}

		[Test]
        public void StatusModels_CashgameWithTwoPlayers_IsSortedByWinningsDescending()
		{
            const int playerId1 = 1;
            const int playerId2 = 2;
		    const string playerName1 = "a";
		    const string playerName2 = "b";
			var player1 = new FakePlayer(displayName: playerName1);
		    var result1 = new FakeCashgameResult(winnings: 1, playerId: playerId1);
		    var player2 = new FakePlayer(displayName: playerName2);
		    var result2 = new FakeCashgameResult(winnings: 2, playerId: playerId2);
            var cashgame = new FakeCashgame(startTime: new DateTime(), results: new List<CashgameResult>{result1, result2});

            GetMock<IRunningCashgameTableItemModelFactory>().Setup(o => o.Create(_homegame, cashgame, player1, result1, It.IsAny<bool>()))
                 .Returns(new RunningCashgameTableItemModel { Name = playerName1 });
            GetMock<IRunningCashgameTableItemModelFactory>().Setup(o => o.Create(_homegame, cashgame, player2, result2, It.IsAny<bool>()))
                 .Returns(new RunningCashgameTableItemModel { Name = playerName2 });
		    GetMock<IPlayerRepository>().Setup(o => o.GetById(playerId1)).Returns(player1);
            GetMock<IPlayerRepository>().Setup(o => o.GetById(playerId2)).Returns(player2);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame, false);

			var results = result.StatusModels;
			Assert.AreEqual("b", results[0].Name);
			Assert.AreEqual("a", results[1].Name);
		}

		private RunningCashgameTableModelFactory GetSut(){
            return new RunningCashgameTableModelFactory(
                GetMock<IRunningCashgameTableItemModelFactory>().Object,
                GetMock<IGlobalization>().Object,
                GetMock<IPlayerRepository>().Object);
		}

	}

}