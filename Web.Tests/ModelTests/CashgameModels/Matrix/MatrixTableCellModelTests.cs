using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Web.Models.CashgameModels.Matrix;

namespace Web.Tests.ModelTests.CashgameModels.Matrix{

	public class MatrixTableCellModelTests {

		private Cashgame _cashgame;
		private CashgameResult _result;

        [SetUp]
		public void SetUp(){
			_cashgame = new Cashgame();
			_result = new CashgameResult();
		}

        [Test]
		public void ShowWinnings_WithResult_IsTrue(){
			var sut = GetSut();

			Assert.IsTrue(sut.ShowResult);
		}

		[Test]
		public void Buyin_WithResult_IsSet(){
			_result.Buyin = 1;

			var sut = GetSut();

			Assert.AreEqual(1, sut.Buyin);
		}

		[Test]
		public void Cashout_WithResult_IsSet(){
			_result.Stack = 1;

			var sut = GetSut();

			Assert.AreEqual(1, sut.Cashout);
		}

		[Test]
		public void Winnings_WithResult_IsSet(){
			_result.Winnings = 1;

			var sut = GetSut();

			Assert.AreEqual("+1", sut.Winnings);
		}

		[Test]
		public void ShowWinnings_WithoutResult_IsFalse(){
			_result = null;

			var sut = GetSut();

			Assert.IsFalse(sut.ShowResult);
		}

		[Test]
		public void ShowTransactions_ResultWithBuyin_IsTrue(){
			_result.Buyin = 1;

			var sut = GetSut();

			Assert.IsTrue(sut.ShowTransactions);
		}

		[Test]
		public void ShowTransactions_ResultWithZeroBuyin_IsFalse(){
			_result.Buyin = 0;

			var sut = GetSut();

			Assert.IsFalse(sut.ShowTransactions);
		}

		[Test]
		public void WinningsClass_WithPositiveResult_IsPosResult(){
			_result.Winnings = 1;

			var sut = GetSut();

			Assert.AreEqual("pos-result", sut.ResultClass);
		}

		[Test]
		public void WinningsClass_WithNegativeResult_IsNegResult(){
			_result.Winnings = -1;

			var sut = GetSut();

			Assert.AreEqual("neg-result", sut.ResultClass);
		}

		[Test]
		public void HasBestResult_PlayerWithBestResult_IsTrue(){
			var cashgameResult = new CashgameResult();
			_cashgame.Results = new List<CashgameResult>{cashgameResult};

			var sut = GetSut();

			Assert.IsTrue(sut.HasBestResult);
		}

		[Test]
		public void HasBestResult_PlayerWithoutBestResult_IsFalse(){
			var sut = GetSut();

			Assert.IsFalse(sut.HasBestResult);
		}

		private MatrixTableCellModel GetSut(){
			return new MatrixTableCellModel(_cashgame, _result);
		}

	}

}