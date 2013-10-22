using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Matrix;

namespace Web.Tests.ModelTests.CashgameModels.Matrix{

	public class CashgameMatrixTableCellModelTests : WebMockContainer {

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
            var result = sut.Create(_cashgame, _result);

			Assert.IsTrue(result.ShowResult);
		}

		[Test]
		public void Buyin_WithResult_IsSet(){
			_result.Buyin = 1;

			var sut = GetSut();
            var result = sut.Create(_cashgame, _result);

			Assert.AreEqual(1, result.Buyin);
		}

		[Test]
		public void Cashout_WithResult_IsSet(){
			_result.Stack = 1;

			var sut = GetSut();
            var result = sut.Create(_cashgame, _result);

			Assert.AreEqual(1, result.Cashout);
		}

		[Test]
		public void Winnings_WithResult_IsSet(){
            const string expectedResult = "a";
            Mocks.ResultFormatterMock.Setup(o => o.FormatWinnings(_result.Winnings)).Returns(expectedResult);

			var sut = GetSut();
            var result = sut.Create(_cashgame, _result);

			Assert.AreEqual(expectedResult, result.Winnings);
		}

		[Test]
		public void ShowWinnings_WithoutResult_IsFalse(){
			_result = null;

			var sut = GetSut();
            var result = sut.Create(_cashgame, _result);

			Assert.IsFalse(result.ShowResult);
		}

		[Test]
		public void ShowTransactions_ResultWithBuyin_IsTrue(){
			_result.Buyin = 1;

			var sut = GetSut();
            var result = sut.Create(_cashgame, _result);

			Assert.IsTrue(result.ShowTransactions);
		}

		[Test]
		public void ShowTransactions_ResultWithZeroBuyin_IsFalse(){
			_result.Buyin = 0;

			var sut = GetSut();
            var result = sut.Create(_cashgame, _result);

			Assert.IsFalse(result.ShowTransactions);
		}

		[Test]
		public void WinningsClass_IsSet(){
            const string resultClass = "a";
            Mocks.ResultFormatterMock.Setup(o => o.GetWinningsCssClass(_result.Winnings)).Returns(resultClass);

			var sut = GetSut();
            var result = sut.Create(_cashgame, _result);

			Assert.AreEqual(resultClass, result.ResultClass);
		}

		[Test]
		public void HasBestResult_PlayerWithBestResult_IsTrue(){
			var cashgameResult = new CashgameResult();
			_cashgame.Results = new List<CashgameResult>{cashgameResult};

			var sut = GetSut();
            var result = sut.Create(_cashgame, _result);

            Assert.IsTrue(result.HasBestResult);
		}

		[Test]
		public void HasBestResult_PlayerWithoutBestResult_IsFalse(){
			var sut = GetSut();
            var result = sut.Create(_cashgame, _result);

            Assert.IsFalse(result.HasBestResult);
		}

		private CashgameMatrixTableCellModelFactory GetSut(){
			return new CashgameMatrixTableCellModelFactory(
                Mocks.ResultFormatterMock.Object);
		}

	}

}