using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Matrix;

namespace Web.Tests.ModelTests.CashgameModels.Matrix{

	public class CashgameMatrixTableCellModelTests : WebMockContainer {

		private Cashgame _cashgame;

        [SetUp]
		public void SetUp(){
			_cashgame = new FakeCashgame();
		}

        [Test]
		public void ShowWinnings_WithResult_IsTrue()
        {
            var cashgameResult = new FakeCashgameResult();

            var sut = GetSut();
            var result = sut.Create(_cashgame, cashgameResult);

			Assert.IsTrue(result.ShowResult);
		}

		[Test]
		public void Buyin_WithResult_IsSet()
		{
		    const int buyin = 1;
            var cashgameResult = new FakeCashgameResult(buyin: buyin);

			var sut = GetSut();
            var result = sut.Create(_cashgame, cashgameResult);

			Assert.AreEqual(buyin, result.Buyin);
		}

		[Test]
		public void Cashout_WithResult_IsSet(){
            const int stack = 1;
            var cashgameResult = new FakeCashgameResult(stack: stack);

			var sut = GetSut();
            var result = sut.Create(_cashgame, cashgameResult);

			Assert.AreEqual(stack, result.Cashout);
		}

		[Test]
		public void Winnings_WithResult_IsSet(){
            const string expectedResult = "a";
            const int winnings = 1;
            var cashgameResult = new FakeCashgameResult(winnings: winnings);
            Mocks.ResultFormatterMock.Setup(o => o.FormatWinnings(winnings)).Returns(expectedResult);

			var sut = GetSut();
            var result = sut.Create(_cashgame, cashgameResult);

			Assert.AreEqual(expectedResult, result.Winnings);
		}

		[Test]
		public void ShowWinnings_WithoutResult_IsFalse(){
			var sut = GetSut();
            var result = sut.Create(_cashgame, null);

			Assert.IsFalse(result.ShowResult);
		}

		[Test]
		public void ShowTransactions_ResultWithBuyin_IsTrue(){
            const int buyin = 1;
            var cashgameResult = new FakeCashgameResult(buyin: buyin);

			var sut = GetSut();
            var result = sut.Create(_cashgame, cashgameResult);

			Assert.IsTrue(result.ShowTransactions);
		}

		[Test]
		public void ShowTransactions_ResultWithZeroBuyin_IsFalse(){
            var cashgameResult = new FakeCashgameResult();

			var sut = GetSut();
            var result = sut.Create(_cashgame, cashgameResult);

			Assert.IsFalse(result.ShowTransactions);
		}

		[Test]
		public void WinningsClass_IsSet(){
            const string resultClass = "a";
            const int winnings = 1;
            var cashgameResult = new FakeCashgameResult(winnings: winnings);
            Mocks.ResultFormatterMock.Setup(o => o.GetWinningsCssClass(winnings)).Returns(resultClass);

			var sut = GetSut();
            var result = sut.Create(_cashgame, cashgameResult);

			Assert.AreEqual(resultClass, result.ResultClass);
		}

		[Test]
		public void HasBestResult_PlayerWithBestResult_IsTrue(){
			var cashgameResult = new FakeCashgameResult();
		    var cashgame = new FakeCashgame(results: new List<CashgameResult> {cashgameResult});

			var sut = GetSut();
            var result = sut.Create(cashgame, cashgameResult);

            Assert.IsTrue(result.HasBestResult);
		}

		[Test]
		public void HasBestResult_PlayerWithoutBestResult_IsFalse(){
            var cashgameResult = new FakeCashgameResult();
		    
            var sut = GetSut();
            var result = sut.Create(_cashgame, cashgameResult);

            Assert.IsFalse(result.HasBestResult);
		}

		private CashgameMatrixTableCellModelFactory GetSut(){
			return new CashgameMatrixTableCellModelFactory(
                Mocks.ResultFormatterMock.Object);
		}

	}

}