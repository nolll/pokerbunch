using System;
using Application.Services;
using Core.Entities;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.Models.UrlModels;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Running{

	class RunningCashgameTableItemModelFactoryTests : MockContainer {

		private Homegame _homegame;
		private Cashgame _cashgame;
		private Boolean _isManager;

        [SetUp]
		public void SetUp(){
			_homegame = new HomegameInTest();
			_cashgame = new CashgameInTest(startTime: new DateTime());
            _isManager = false;
		}

        [Test]
		public void Name_IsSets(){
			var player = new PlayerInTest(displayName: "a");
            var cashgameResult = new CashgameResultInTest();

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult, _isManager);

			Assert.AreEqual("a", result.Name);
		}

		[Test]
		public void PlayerUrl_IsSet()
		{
			var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();


			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult, _isManager);

			Assert.IsInstanceOf<CashgameActionUrl>(result.PlayerUrl);
		}

		[Test]
		public void Buyin_IsSet()
		{
		    const string formatted = "a";
			const int buyin = 1;
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest(buyin: buyin);

            GetMock<IGlobalization>().Setup(o => o.FormatCurrency(It.IsAny<Currency>(), buyin)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult, _isManager);

			Assert.AreEqual(formatted, result.Buyin);
		}

		[Test]
		public void Stack_IsSet()
        {
            const string formatted = "a";
            const int stack = 1;
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest(stack: stack);

            GetMock<IGlobalization>().Setup(o => o.FormatCurrency(It.IsAny<Currency>(), stack)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult, _isManager);

			Assert.AreEqual(formatted, result.Stack);
		}

		[Test]
		public void Winnings_IsSet()
		{
		    const string formatted = "a";
		    const int winnings = 1;
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest(winnings: winnings);

            GetMock<IGlobalization>().Setup(o => o.FormatResult(It.IsAny<Currency>(), winnings)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult, _isManager);

			Assert.AreEqual(formatted, result.Winnings);
		}

		[Test]
		public void Time_IsSetToDifferenceBetweenNowAndLastCheckpoint()
		{
		    const string formatted = "a";
            var player = new PlayerInTest();
		    var expectedTimespan = new TimeSpan(0, 1, 0);
            var cashgameResult = new CashgameResultInTest(lastReportTime: DateTime.Parse("2010-01-01 01:00:00"));
            GetMock<ITimeProvider>().Setup(o => o.GetTime()).Returns(DateTime.Parse("2010-01-01 01:01:00"));
            GetMock<IGlobalization>().Setup(o => o.FormatTimespan(expectedTimespan)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult, _isManager);

			Assert.AreEqual(formatted, result.Time);
		}

		[Test]
		public void WinningsClass_IsSet(){
            const string resultClass = "pos-result";
		    const int winnings = 1;
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest(winnings: winnings);

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult, _isManager);

			Assert.AreEqual(resultClass, result.WinningsClass);
		}

		[Test]
		public void ManagerButtonsEnabled_WithoutManager_IsFalse(){
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();
            
            var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult, _isManager);

			Assert.IsFalse(result.ManagerButtonsEnabled);
		}

		[Test]
		public void ManagerButtonsEnabled_WithManager_IsTrue(){
			_isManager = true;
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult, _isManager);

			Assert.IsTrue(result.ManagerButtonsEnabled);
		}

		[Test]
		public void BuyinUrl_WithManager_IsCorrectType()
		{
			_isManager = true;
			var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult, _isManager);

			Assert.IsInstanceOf<CashgameBuyinUrl>(result.BuyinUrl);
		}

		[Test]
		public void ReportUrl_WithManager_IsCorrectType()
		{
			_isManager = true;
			var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult, _isManager);

			Assert.IsInstanceOf<CashgameReportUrl>(result.ReportUrl);
		}

		[Test]
		public void CashoutUrl_WithManager_IsCorrectType()
		{
            _isManager = true;
			var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult, _isManager);

			Assert.IsInstanceOf<CashgameCashoutUrl>(result.CashoutUrl);
		}

		[Test]
		public void HasCheckedOut_ResultWithoutCashoutTime_ReturnsFalse(){
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult, _isManager);

			Assert.IsFalse(result.HasCashedOut);
		}

		[Test]
		public void HasCheckedOut_ResultWithCashoutTime_ReturnsTrue(){
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest(cashoutTime: new DateTime());

			var sut = GetSut();
            var result = sut.Create(_homegame, _cashgame, player, cashgameResult, _isManager);

			Assert.IsTrue(result.HasCashedOut);
		}

		private RunningCashgameTableItemModelFactory GetSut(){
            return new RunningCashgameTableItemModelFactory(
                GetMock<ITimeProvider>().Object,
                GetMock<IGlobalization>().Object);
		}

	}

}