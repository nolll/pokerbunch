using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.Models.CashgameModels.Details;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Details{

	public class CashgameDetailsTableModelFactoryTests : MockContainer {

		private Homegame _homegame;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
		}

        [Test]
		public void ResultModels_CashgameWithOnePlayer_ContainsOneItem(){
			var cashgame = new FakeCashgame(results: new List<CashgameResult> {new FakeCashgameResult()});

            var sut = GetSut();
            var result = sut.Create(_homegame, cashgame);

			Assert.AreEqual(1, result.ResultModels.Count);
		}

		[Test]
		public void ResultModels_CashgameWithTwoPlayers_HasTwoItems(){
			var cashgame = new FakeCashgame(results: new List<CashgameResult> {new FakeCashgameResult(), new FakeCashgameResult()});

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame);

            Assert.AreEqual(2, result.ResultModels.Count);
		}

		[Test]
		public void ResultModels_CashgameWithTwoPlayers_IsSortedByWinningsDescending()
		{
		    const int playerId1 = 1;
		    const int playerId2 = 2;
		    const string displayName1 = "a";
            const string displayName2 = "b";
			var player1 = new FakePlayer(displayName: "a");
		    var result1 = new FakeCashgameResult(winnings: 1, playerId: playerId1);
		    var player2 = new FakePlayer(displayName: "b");
            var result2 = new FakeCashgameResult(winnings: 2, playerId: playerId2);
		    var cashgame = new FakeCashgame(startTime: new DateTime(), results: new List<CashgameResult> {result1, result2});

		    GetMock<ICashgameDetailsTableItemModelFactory>().Setup(o => o.Create(_homegame, cashgame, player1, result1)).Returns(new CashgameDetailsTableItemModel{ Name = displayName1 });
            GetMock<ICashgameDetailsTableItemModelFactory>().Setup(o => o.Create(_homegame, cashgame, player2, result2)).Returns(new CashgameDetailsTableItemModel { Name = displayName2 });
		    GetMock<IPlayerRepository>().Setup(o => o.GetById(playerId1)).Returns(player1);
            GetMock<IPlayerRepository>().Setup(o => o.GetById(playerId2)).Returns(player2);

			var sut = GetSut();
            var result = sut.Create(_homegame, cashgame);

            Assert.AreEqual("b", result.ResultModels[0].Name);
            Assert.AreEqual("a", result.ResultModels[1].Name);
		}

		private CashgameDetailsTableModelFactory GetSut()
		{
		    return new CashgameDetailsTableModelFactory(
                GetMock<ICashgameDetailsTableItemModelFactory>().Object,
                GetMock<IPlayerRepository>().Object);
		}

	}

}