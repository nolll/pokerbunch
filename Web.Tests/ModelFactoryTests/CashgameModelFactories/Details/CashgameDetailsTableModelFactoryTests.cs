using System;
using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.Models.CashgameModels.Details;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Details{

	public class CashgameDetailsTableModelFactoryTests : WebMockContainer {

		private Homegame _homegame;
		private Cashgame _cashgame;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_cashgame = new Cashgame();
		}

        [Test]
		public void ResultModels_CashgameWithOnePlayer_ContainsOneItem(){
			_cashgame = new Cashgame {Results = new List<CashgameResult> {new CashgameResult()}};

            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame);

			Assert.AreEqual(1, result.ResultModels.Count);
		}

		[Test]
		public void ResultModels_CashgameWithTwoPlayers_HasTwoItems(){
			_cashgame = new Cashgame {Results = new List<CashgameResult> {new CashgameResult(), new CashgameResult()}};

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame);

            Assert.AreEqual(2, result.ResultModels.Count);
		}

		[Test]
		public void ResultModels_CashgameWithTwoPlayers_IsSortedByWinningsDescending()
		{
		    const string displayName1 = "a";
            const string displayName2 = "b";
            _cashgame.StartTime = new DateTime();
			var player1 = new Player {DisplayName = "a"};
		    var result1 = new CashgameResult {Player = player1, Winnings = 1};
		    var player2 = new Player {DisplayName = "b"};
		    var result2 = new CashgameResult {Player = player2, Winnings = 2};
		    _cashgame.Results = new List<CashgameResult>{result1, result2};

		    Mocks.CashgameDetailsTableItemModelFactoryMock.Setup(o => o.Create(_homegame, _cashgame, result1)).Returns(new CashgameDetailsTableItemModel{ Name = displayName1 });
            Mocks.CashgameDetailsTableItemModelFactoryMock.Setup(o => o.Create(_homegame, _cashgame, result2)).Returns(new CashgameDetailsTableItemModel { Name = displayName2 });

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame);

            Assert.AreEqual("b", result.ResultModels[0].Name);
            Assert.AreEqual("a", result.ResultModels[1].Name);
		}

		private CashgameDetailsTableModelFactory GetSut()
		{
		    return new CashgameDetailsTableModelFactory(Mocks.CashgameDetailsTableItemModelFactoryMock.Object);
		}

	}

}