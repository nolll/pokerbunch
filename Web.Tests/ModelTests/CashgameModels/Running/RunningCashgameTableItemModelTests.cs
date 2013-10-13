using System;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.Models.CashgameModels.Running;
using Web.Models.UrlModels;

namespace Web.Tests.ModelTests.CashgameModels.Running{

	class RunningCashgameTableItemModelTests : MockContainer {

		private Homegame _homegame;
		private Cashgame _cashgame;
		private CashgameResult _result;
		private Boolean _isManager;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_cashgame = new Cashgame {StartTime = new DateTime()};
            _result = new CashgameResult();
			_isManager = false;
		}

        [Test]
		public void Name_IsSets(){
			var player = new Player {DisplayName = "a"};
            _result.Player = player;

			var sut = GetSut();

			Assert.AreEqual("a", sut.Name);
		}

		[Test]
		public void PlayerUrl_IsSet(){
			var player = new Player();
			_result.Player = player;

			var sut = GetSut();

			Assert.IsInstanceOf<CashgameActionUrlModel>(sut.PlayerUrl);
		}

		[Test]
		public void Buyin_IsSet(){
			_result.Buyin = 1;

			var sut = GetSut();

			Assert.AreEqual("$1", sut.Buyin);
		}

		[Test]
		public void test_Stack_IsSet(){
			_result.Stack = 1;

			var sut = GetSut();

			Assert.AreEqual("$1", sut.Stack);
		}

		[Test]
		public void Winnings_IsSet(){
			_result.Winnings = 1;

			var sut = GetSut();

			Assert.AreEqual("+$1", sut.Winnings);
		}

		[Test]
		public void Time_IsSetToDifferenceBetweenNowAndLastCheckpoint(){
			SetLastCheckpointTime(DateTime.Parse("2010-01-01 01:00:00"));
            WebMocks.TimeProviderMock.Setup(o => o.GetTime()).Returns(DateTime.Parse("2010-01-01 01:01:00"));
			var sut = GetSut();

			Assert.AreEqual("1 minute", sut.Time);
		}

		[Test]
		public void WinningsClass_BuyinEqualToCashout_IsEmpty(){
			_result.Buyin = 100;
			_result.Stack = 100;

			var sut = GetSut();

			Assert.AreEqual("", sut.WinningsClass);
		}

		[Test]
		public void WinningsClass_WithPositiveResult_IsPositive(){
			_result.Winnings = 1;

			var sut = GetSut();

			Assert.AreEqual("pos-result", sut.WinningsClass);
		}

		[Test]
		public void WinningsClass_WithNegativeResult_IsNegative(){
			_result.Winnings = -1;

			var sut = GetSut();

			Assert.AreEqual("neg-result", sut.WinningsClass);
		}

		[Test]
		public void ManagerButtonsEnabled_WithoutManager_IsFalse(){
			var sut = GetSut();

			Assert.IsFalse(sut.ManagerButtonsEnabled);
		}

		[Test]
		public void ManagerButtonsEnabled_WithManager_IsTrue(){
			_isManager = true;

			var sut = GetSut();

			Assert.IsTrue(sut.ManagerButtonsEnabled);
		}

		[Test]
		public void BuyinUrl_WithManager_IsCorrectType(){
			_isManager = true;
			var player = new Player();
			_result.Player = player;

			var sut = GetSut();

			Assert.IsInstanceOf<CashgameBuyinUrlModel>(sut.BuyinUrl);
		}

		[Test]
		public void ReportUrl_WithManager_IsCorrectType(){
			_isManager = true;
			var player = new Player();
			_result.Player = player;

			var sut = GetSut();

			Assert.IsInstanceOf<CashgameReportUrlModel>(sut.ReportUrl);
		}

		[Test]
		public void CashoutUrl_WithManager_IsCorrectType(){
			_isManager = true;
			var player = new Player();
			_result.Player = player;

			var sut = GetSut();

			Assert.IsInstanceOf<CashgameCashoutUrlModel>(sut.CashoutUrl);
		}

		[Test]
		public void HasCheckedOut_ResultWithoutCashoutTime_ReturnsFalse(){
			var sut = GetSut();

			Assert.IsFalse(sut.HasCashedOut);
		}

		[Test]
		public void HasCheckedOut_ResultWithCashoutTime_ReturnsTrue(){
			_result.CashoutTime = new DateTime();
			var sut = GetSut();

			Assert.IsTrue(sut.HasCashedOut);
		}

		private RunningCashgameTableItemModel GetSut(){
            return new RunningCashgameTableItemModel(_homegame, _cashgame, _result, _isManager, WebMocks.TimeProviderMock.Object);
		}

		private void SetLastCheckpointTime(DateTime time){
			_result.LastReportTime = time;
		}

	}

}