using System;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Running;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Running{

	class RunningCashgameTableItemModelFactoryTests : WebMockContainer {

		private Homegame _homegame;
		private Cashgame _cashgame;
		private Boolean _isManager;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
			_cashgame = new FakeCashgame(startTime: new DateTime());
            _isManager = false;
		}

        [Test]
		public void Name_IsSets(){
			var player = new FakePlayer(displayName: "a");
            var cashgameResult = new FakeCashgameResult(player);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, cashgameResult, _isManager);

			Assert.AreEqual("a", result.Name);
		}

		[Test]
		public void PlayerUrl_IsSet()
		{
		    const string playerUrl = "a";
			var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult(player);

		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameActionUrl(_homegame, _cashgame, player)).Returns(playerUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, cashgameResult, _isManager);

			Assert.AreEqual(playerUrl, result.PlayerUrl);
		}

		[Test]
		public void Buyin_IsSet()
		{
		    const string formatted = "a";
			const int buyin = 1;
            var cashgameResult = new FakeCashgameResult(buyin: buyin);

            Mocks.GlobalizationMock.Setup(o => o.FormatCurrency(It.IsAny<CurrencySettings>(), buyin)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, cashgameResult, _isManager);

			Assert.AreEqual(formatted, result.Buyin);
		}

		[Test]
		public void test_Stack_IsSet()
        {
            const string formatted = "a";
            const int stack = 1;
            var cashgameResult = new FakeCashgameResult(stack: stack);

            Mocks.GlobalizationMock.Setup(o => o.FormatCurrency(It.IsAny<CurrencySettings>(), stack)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, cashgameResult, _isManager);

			Assert.AreEqual(formatted, result.Stack);
		}

		[Test]
		public void Winnings_IsSet()
		{
		    const string formatted = "a";
		    const int winnings = 1;
            var cashgameResult = new FakeCashgameResult(winnings: winnings);

            Mocks.GlobalizationMock.Setup(o => o.FormatResult(It.IsAny<CurrencySettings>(), winnings)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, cashgameResult, _isManager);

			Assert.AreEqual(formatted, result.Winnings);
		}

		[Test]
		public void Time_IsSetToDifferenceBetweenNowAndLastCheckpoint()
		{
		    const string formatted = "a";
		    var expectedTimespan = new TimeSpan(0, 1, 0);
            var cashgameResult = new FakeCashgameResult(lastReportTime: DateTime.Parse("2010-01-01 01:00:00"));
            Mocks.TimeProviderMock.Setup(o => o.GetTime()).Returns(DateTime.Parse("2010-01-01 01:01:00"));
            Mocks.GlobalizationMock.Setup(o => o.FormatTimespan(expectedTimespan)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, cashgameResult, _isManager);

			Assert.AreEqual(formatted, result.Time);
		}

		[Test]
		public void WinningsClass_IsSet(){
            const string resultClass = "a";
		    const int winnings = 1;
            var cashgameResult = new FakeCashgameResult(winnings: winnings);
            Mocks.ResultFormatterMock.Setup(o => o.GetWinningsCssClass(winnings)).Returns(resultClass);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, cashgameResult, _isManager);

			Assert.AreEqual(resultClass, result.WinningsClass);
		}

		[Test]
		public void ManagerButtonsEnabled_WithoutManager_IsFalse(){
            var cashgameResult = new FakeCashgameResult();
            
            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, cashgameResult, _isManager);

			Assert.IsFalse(result.ManagerButtonsEnabled);
		}

		[Test]
		public void ManagerButtonsEnabled_WithManager_IsTrue(){
			_isManager = true;
            var cashgameResult = new FakeCashgameResult();

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, cashgameResult, _isManager);

			Assert.IsTrue(result.ManagerButtonsEnabled);
		}

		[Test]
		public void BuyinUrl_WithManager_IsCorrectType()
		{
		    const string buyinUrl = "a";
			_isManager = true;
			var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult(player);

		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameBuyinUrl(_homegame, player)).Returns(buyinUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, cashgameResult, _isManager);

			Assert.AreEqual(buyinUrl, result.BuyinUrl);
		}

		[Test]
		public void ReportUrl_WithManager_IsCorrectType()
		{
		    const string reportUrl = "a";
			_isManager = true;
			var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult(player);

		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameReportUrl(_homegame, player)).Returns(reportUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, cashgameResult, _isManager);

			Assert.AreEqual(reportUrl, result.ReportUrl);
		}

		[Test]
		public void CashoutUrl_WithManager_IsCorrectType()
		{
		    const string cashoutUrl = "a";
            _isManager = true;
			var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult(player);

		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameCashoutUrl(_homegame, player)).Returns(cashoutUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, cashgameResult, _isManager);

			Assert.AreEqual(cashoutUrl, result.CashoutUrl);
		}

		[Test]
		public void HasCheckedOut_ResultWithoutCashoutTime_ReturnsFalse(){
            var cashgameResult = new FakeCashgameResult();

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, cashgameResult, _isManager);

			Assert.IsFalse(result.HasCashedOut);
		}

		[Test]
		public void HasCheckedOut_ResultWithCashoutTime_ReturnsTrue(){
            var cashgameResult = new FakeCashgameResult(cashoutTime: new DateTime());

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, cashgameResult, _isManager);

			Assert.IsTrue(result.HasCashedOut);
		}

		private RunningCashgameTableItemModelFactory GetSut(){
            return new RunningCashgameTableItemModelFactory(
                Mocks.UrlProviderMock.Object,
                Mocks.TimeProviderMock.Object,
                Mocks.ResultFormatterMock.Object,
                Mocks.GlobalizationMock.Object);
		}

	}

}