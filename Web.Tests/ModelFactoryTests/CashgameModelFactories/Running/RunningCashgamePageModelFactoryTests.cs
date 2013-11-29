using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.ModelFactories.PageBaseModelFactories;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Running{

	class RunningCashgamePageModelFactoryTests : MockContainer {

		[Test]
        public void StartTime_WithStartTime_IsSet()
		{
		    const string formatted = "a";
		    const bool isManager = false;
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult();
		    var startTime = DateTime.Parse("2010-01-01 01:00:00");
            var cashgame = new FakeCashgame(status: GameStatus.Running, isStarted: true, startTime: startTime, results: new List<CashgameResult> { cashgameResult });

            GetMock<IGlobalization>().Setup(o => o.FormatTime(It.IsAny<DateTime>())).Returns(formatted);

		    var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgame, player, isManager);

			Assert.AreEqual(formatted, result.StartTime);
		}

		[Test]
        public void StartTime_NoStartTime_IsNull()
		{
		    const bool isManager = false;
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult();
            var cashgame = new FakeCashgame(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult });

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgame, player, isManager);

            Assert.IsNull(result.StartTime);
		}

		[Test]
        public void ShowStartTime_WithStartTime_IsTrue()
        {
            const bool isManager = false;
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult();
            var cashgame = new FakeCashgame(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult }, isStarted: true, startTime: DateTime.Parse("2010-01-01 01:00:00"));

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgame, player, isManager);

            Assert.IsTrue(result.ShowStartTime);
		}

		[Test]
        public void ShowStartTime_NoStartTime_IsFalse(){
            const bool isManager = false;
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult();
            var cashgame = new FakeCashgame(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult });

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgame, player, isManager);

            Assert.IsFalse(result.ShowStartTime);
		}

		[Test]
        public void Location_IsSet(){
            const bool isManager = false;
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult();
            var cashgame = new FakeCashgame(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult }, location: "a");

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgame, player, isManager);

            Assert.AreEqual("a", result.Location);
		}

		[Test]
        public void BuyinUrl_IsSet()
		{
            const bool isManager = false;
		    const string buyinUrl = "a";
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult();
            var cashgame = new FakeCashgame(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult });
		    GetMock<IUrlProvider>().Setup(o => o.GetCashgameBuyinUrl(homegame, player)).Returns(buyinUrl);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgame, player, isManager);

            Assert.AreEqual(buyinUrl, result.BuyinUrl);
		}

		[Test]
        public void ReportUrl_IsSet(){
            const bool isManager = false;
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult();
            var cashgame = new FakeCashgame(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult });
		    const string reportUrl = "a";
		    GetMock<IUrlProvider>().Setup(o => o.GetCashgameReportUrl(homegame, player)).Returns(reportUrl);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgame, player, isManager);

            Assert.AreEqual(reportUrl, result.ReportUrl);
		}

		[Test]
        public void CashoutUrl_IsSet()
		{
            const bool isManager = false;
		    const string cashoutUrl = "a";
		    GetMock<IUrlProvider>().Setup(o => o.GetCashgameCashoutUrl(It.IsAny<Homegame>(), It.IsAny<Player>())).Returns(cashoutUrl);
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult();
            var cashgame = new FakeCashgame(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult });
            
            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgame, player, isManager);

            Assert.AreEqual(cashoutUrl, result.CashoutUrl);
		}

		[Test]
        public void BuyinButtonEnabled_IsTrue(){
            const bool isManager = false;
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult();
            var cashgame = new FakeCashgame(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult });

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgame, player, isManager);

            Assert.IsTrue(result.BuyinButtonEnabled);
		}

		[Test]
        public void ReportButtonEnabled_WithPlayerNotInGame_IsFalse(){
            const bool isManager = false;
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            var cashgame = new FakeCashgame(status: GameStatus.Running);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgame, player, isManager);

            Assert.IsFalse(result.ReportButtonEnabled);
		}

		[Test]
        public void ReportButtonEnabled_WithPlayerInGame_IsTrue(){
            const bool isManager = false;
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult();
            var cashgame = new FakeCashgame(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult });

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgame, player, isManager);

            Assert.IsTrue(result.ReportButtonEnabled);
		}

		[Test]
        public void CashoutButtonEnabled_WithPlayerNotInGame_IsFalse(){
            const bool isManager = false;
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            var cashgame = new FakeCashgame(status: GameStatus.Running);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgame, player, isManager);

            Assert.IsFalse(result.CashoutButtonEnabled);
		}

		[Test]
        public void CashoutButtonEnabled_WithPlayerInGame_IsTrue(){
            const bool isManager = false;
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult();
            var cashgameResults = new List<CashgameResult> { cashgameResult };
            var cashgame = new FakeCashgame(status: GameStatus.Running, results: cashgameResults);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgame, player, isManager);

            Assert.IsTrue(result.CashoutButtonEnabled);
		}

        //todo: Fix old tests
		/*
		[Test]
        public void EndGameButtonEnabled_AtLeastOnePlayerIsStillInGame_IsFalse(){
			setupHomegameAndRunningCashgameWithOnePlayer();
			cashgame.setStarted();
			cashgame.setHasActivePlayers();
			sut = getSut();
			assertFalse(sut.cashoutButtonEnabled);
		}

        [Test]
        public void 
		function test_EndGameButtonEnabled_AllPlayersCashedOut_IsTrue(){
			setupHomegameAndRunningCashgameWithOnePlayer();
			cashgame.setStarted();
			sut = getSut();
			assertTrue(sut.cashoutButtonEnabled);
		}
		*/

		[Test]
        public void ShowTable_WithStartedGame_IsTrue(){
            const bool isManager = false;
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            var cashgameResults = new FakeCashgameResult();
            var cashgame = new FakeCashgame(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResults }, startTime: new DateTime(), isStarted: true);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgame, player, isManager);

            Assert.IsTrue(result.ShowTable);
		}

		[Test]
        public void ShowTable_WithStartedGameButNoResults_IsFalse(){
            const bool isManager = false;
            var homegame = new FakeHomegame();
            var player = new FakePlayer();
            var cashgameResult = new FakeCashgameResult();
            var cashgame = new FakeCashgame(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult }, startTime: new DateTime());

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), homegame, cashgame, player, isManager);

			Assert.IsFalse(result.ShowTable);
		}

        //todo: fix old test
		/*
		function test_ChartDataUrl_IsSet(){
			setupPlayerIsInGame();

			sut = getModel();

			assertIsA(sut.chartDataUrl, 'app\Urls\CashgameDetailsChartJsonUrlModel');
		}
		*/

		private RunningCashgamePageModelFactory GetSut(){
            return new RunningCashgamePageModelFactory(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<IRunningCashgameTableModelFactory>().Object,
                GetMock<IUrlProvider>().Object,
                GetMock<IGlobalization>().Object);
		}

	}

}