using System;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.Models.UrlModels;

namespace Web.Tests.ModelTests.CashgameModels.Running{

	class RunningCashgameTableItemModelTests : WebMockContainer {

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
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.AreEqual("a", result.Name);
		}

		[Test]
		public void PlayerUrl_IsSet()
		{
		    const string playerUrl = "a";
			var player = new Player();
			_result.Player = player;

		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameActionUrl(_homegame, _cashgame, player)).Returns(playerUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.AreEqual(playerUrl, result.PlayerUrl);
		}

		[Test]
		public void Buyin_IsSet(){
			_result.Buyin = 1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.AreEqual("$1", result.Buyin);
		}

		[Test]
		public void test_Stack_IsSet(){
			_result.Stack = 1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.AreEqual("$1", result.Stack);
		}

		[Test]
		public void Winnings_IsSet(){
			_result.Winnings = 1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.AreEqual("+$1", result.Winnings);
		}

		[Test]
		public void Time_IsSetToDifferenceBetweenNowAndLastCheckpoint(){
			SetLastCheckpointTime(DateTime.Parse("2010-01-01 01:00:00"));
            Mocks.TimeProviderMock.Setup(o => o.GetTime()).Returns(DateTime.Parse("2010-01-01 01:01:00"));

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.AreEqual("1 minute", result.Time);
		}

		[Test]
		public void WinningsClass_BuyinEqualToCashout_IsEmpty(){
			_result.Buyin = 100;
			_result.Stack = 100;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.AreEqual("", result.WinningsClass);
		}

		[Test]
		public void WinningsClass_WithPositiveResult_IsPositive(){
			_result.Winnings = 1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.AreEqual("pos-result", result.WinningsClass);
		}

		[Test]
		public void WinningsClass_WithNegativeResult_IsNegative(){
			_result.Winnings = -1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.AreEqual("neg-result", result.WinningsClass);
		}

		[Test]
		public void ManagerButtonsEnabled_WithoutManager_IsFalse(){
			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.IsFalse(result.ManagerButtonsEnabled);
		}

		[Test]
		public void ManagerButtonsEnabled_WithManager_IsTrue(){
			_isManager = true;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.IsTrue(result.ManagerButtonsEnabled);
		}

		[Test]
		public void BuyinUrl_WithManager_IsCorrectType()
		{
		    const string buyinUrl = "a";
			_isManager = true;
			var player = new Player();
			_result.Player = player;

		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameBuyinUrl(_homegame, player)).Returns(buyinUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.AreEqual(buyinUrl, result.BuyinUrl);
		}

		[Test]
		public void ReportUrl_WithManager_IsCorrectType(){
			_isManager = true;
			var player = new Player();
			_result.Player = player;

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.IsInstanceOf<CashgameReportUrlModel>(result.ReportUrl);
		}

		[Test]
		public void CashoutUrl_WithManager_IsCorrectType()
		{
		    const string cashoutUrl = "a";
            _isManager = true;
			var player = new Player();
			_result.Player = player;

		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameCashoutUrl(_homegame, player)).Returns(cashoutUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.AreEqual(cashoutUrl, result.CashoutUrl);
		}

		[Test]
		public void HasCheckedOut_ResultWithoutCashoutTime_ReturnsFalse(){
			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.IsFalse(result.HasCashedOut);
		}

		[Test]
		public void HasCheckedOut_ResultWithCashoutTime_ReturnsTrue(){
			_result.CashoutTime = new DateTime();

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, _result, _isManager);

			Assert.IsTrue(result.HasCashedOut);
		}

		private RunningCashgameTableItemModelFactory GetSut(){
            return new RunningCashgameTableItemModelFactory(
                Mocks.UrlProviderMock.Object,
                Mocks.TimeProviderMock.Object);
		}

		private void SetLastCheckpointTime(DateTime time){
			_result.LastReportTime = time;
		}

	}

}