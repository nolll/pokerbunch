using System;
using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.Models.CashgameModels.Running;

namespace tests{

	public class StatusTableModelTests : MockContainer{

		private Homegame _homegame;
		private Cashgame _cashgame;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_cashgame = new Cashgame();
		}

		[Test]
        public void StatusModels_CashgameWithOnePlayer_FirstItemIsCorrectType(){
			_cashgame.Results = new List<CashgameResult>{new CashgameResult()};

			var sut = GetSut();

			Assert.AreEqual(1, sut.StatusModels.Count);
		    Assert.IsInstanceOf<StatusItemModel>(sut.StatusModels[0]);
        }

		[Test]
        public void StatusModels_CashgameWithTwoPlayers_HasTwoItems(){
			_cashgame.Results = new List<CashgameResult>{new CashgameResult(), new CashgameResult()};

			var sut = GetSut();

			Assert.AreEqual(2, sut.StatusModels.Count);
		}

		[Test]
        public void TotalBuyin_CashgameWithTwoPlayers_IsSumOfBuyins(){
			_cashgame.Turnover = 1;

			var sut = GetSut();

			Assert.AreEqual("$1", sut.TotalBuyin);
		}

		[Test]
        public void TotalStacks_CashgameWithTwoPlayers_IsSumOfCurrentStacks(){
			_cashgame.TotalStacks = 1;

			var sut = GetSut();

			Assert.AreEqual("$1", sut.TotalStacks);
		}

		[Test]
        public void StatusModels_CashgameWithTwoPlayers_IsSortedByWinningsDescending(){
			_cashgame.StartTime = new DateTime();
			var player1 = new Player {DisplayName = "a"};
		    var result1 = new CashgameResult {Player = player1, Winnings = 1};
		    var player2 = new Player {DisplayName = "b"};
		    var result2 = new CashgameResult {Player = player2, Winnings = 2};
		    _cashgame.Results = new List<CashgameResult>{result1, result2};

			var sut = GetSut();

			var results = sut.StatusModels;
			Assert.AreEqual("b", results[0].Name);
			Assert.AreEqual("a", results[1].Name);
		}

		private StatusTableModel GetSut(){
			return new StatusTableModel(_homegame, _cashgame, false, TimeProviderMock.Object);
		}

	}

}