using System;
using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.Models.CashgameModels.Details;

namespace Web.Tests.ModelTests.CashgameModels.Details{

	public class CashgameDetailsTableModelTests : WebMockContainer {

		private Homegame _homegame;
		private Cashgame _cashgame;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_cashgame = new Cashgame();
		}

        [Test]
		public void ResultModels_CashgameWithOnePlayer_FirstItemIsCorrectType(){
			_cashgame = new Cashgame {Results = new List<CashgameResult> {new CashgameResult()}};

            var sut = GetSut();

			Assert.AreEqual(1, sut.ResultModels.Count);
			Assert.IsInstanceOf<CashgameDetailsTableItemModel>(sut.ResultModels[0]);
		}

		[Test]
		public void ResultModels_CashgameWithTwoPlayers_HasTwoItems(){
			_cashgame = new Cashgame {Results = new List<CashgameResult> {new CashgameResult(), new CashgameResult()}};

			var sut = GetSut();

			Assert.AreEqual(2, sut.ResultModels.Count);
		}

		[Test]
		public void ResultModels_CashgameWithTwoPlayers_IsSortedByWinningsDescending(){
			_cashgame.StartTime = new DateTime();
			var player1 = new Player {DisplayName = "a"};
		    var result1 = new CashgameResult {Player = player1, Winnings = 1};
		    var player2 = new Player {DisplayName = "b"};
		    var result2 = new CashgameResult {Player = player2, Winnings = 2};
		    _cashgame.Results = new List<CashgameResult>{result1, result2};

			var sut = GetSut();

			Assert.AreEqual("b", sut.ResultModels[0].Name);
            Assert.AreEqual("a", sut.ResultModels[1].Name);
		}

		private CashgameDetailsTableModel GetSut(){
			return new CashgameDetailsTableModel(_homegame, _cashgame);
		}

	}

}