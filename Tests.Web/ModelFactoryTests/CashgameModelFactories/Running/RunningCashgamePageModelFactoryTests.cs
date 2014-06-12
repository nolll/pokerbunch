using System;
using System.Collections.Generic;
using Application.Services;
using Core.Entities;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UrlModels;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Running
{

    class RunningCashgamePageModelFactoryTests : MockContainer
    {

        [Test]
        public void StartTime_WithStartTime_IsSet()
        {
            const string formatted = "a";
            const bool isManager = false;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();
            var startTime = DateTime.Parse("2010-01-01 01:00:00");
            var cashgame = new CashgameInTest(status: GameStatus.Running, isStarted: true, startTime: startTime, results: new List<CashgameResult> { cashgameResult });

            GetMock<IGlobalization>().Setup(o => o.FormatTime(It.IsAny<DateTime>())).Returns(formatted);

            var sut = GetSut();
            var result = sut.Create(homegame, cashgame, player, isManager);

            Assert.AreEqual(formatted, result.StartTime);
        }

        [Test]
        public void StartTime_NoStartTime_IsNull()
        {
            const bool isManager = false;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();
            var cashgame = new CashgameInTest(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult });

            var sut = GetSut();
            var result = sut.Create(homegame, cashgame, player, isManager);

            Assert.IsNull(result.StartTime);
        }

        [Test]
        public void ShowStartTime_WithStartTime_IsTrue()
        {
            const bool isManager = false;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();
            var cashgame = new CashgameInTest(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult }, isStarted: true, startTime: DateTime.Parse("2010-01-01 01:00:00"));

            var sut = GetSut();
            var result = sut.Create(homegame, cashgame, player, isManager);

            Assert.IsTrue(result.ShowStartTime);
        }

        [Test]
        public void ShowStartTime_NoStartTime_IsFalse()
        {
            const bool isManager = false;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();
            var cashgame = new CashgameInTest(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult });

            var sut = GetSut();
            var result = sut.Create(homegame, cashgame, player, isManager);

            Assert.IsFalse(result.ShowStartTime);
        }

        [Test]
        public void Location_IsSet()
        {
            const bool isManager = false;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();
            var cashgame = new CashgameInTest(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult }, location: "a");

            var sut = GetSut();
            var result = sut.Create(homegame, cashgame, player, isManager);

            Assert.AreEqual("a", result.Location);
        }

        [Test]
        public void BuyinUrl_IsSet()
        {
            const bool isManager = false;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();
            var cashgame = new CashgameInTest(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult });

            var sut = GetSut();
            var result = sut.Create(homegame, cashgame, player, isManager);

            Assert.IsInstanceOf<CashgameBuyinUrl>(result.BuyinUrl);
        }

        [Test]
        public void ReportUrl_IsSet()
        {
            const bool isManager = false;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();
            var cashgame = new CashgameInTest(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult });
            
            var sut = GetSut();
            var result = sut.Create(homegame, cashgame, player, isManager);

            Assert.IsInstanceOf<CashgameReportUrl>(result.ReportUrl);
        }

        [Test]
        public void CashoutUrl_IsSet()
        {
            const bool isManager = false;

            var homegame = new HomegameInTest();
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();
            var cashgame = new CashgameInTest(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult });

            var sut = GetSut();
            var result = sut.Create(homegame, cashgame, player, isManager);

            Assert.IsInstanceOf<CashgameCashoutUrl>(result.CashoutUrl);
        }

        [Test]
        public void BuyinButtonEnabled_IsTrue()
        {
            const bool isManager = false;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();
            var cashgame = new CashgameInTest(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult });

            var sut = GetSut();
            var result = sut.Create(homegame, cashgame, player, isManager);

            Assert.IsTrue(result.BuyinButtonEnabled);
        }

        [Test]
        public void ReportButtonEnabled_WithPlayerNotInGame_IsFalse()
        {
            const bool isManager = false;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest();
            var cashgame = new CashgameInTest(status: GameStatus.Running);

            var sut = GetSut();
            var result = sut.Create(homegame, cashgame, player, isManager);

            Assert.IsFalse(result.ReportButtonEnabled);
        }

        [Test]
        public void ReportButtonEnabled_WithPlayerInGame_IsTrue()
        {
            const bool isManager = false;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();
            var cashgame = new CashgameInTest(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult });

            var sut = GetSut();
            var result = sut.Create(homegame, cashgame, player, isManager);

            Assert.IsTrue(result.ReportButtonEnabled);
        }

        [Test]
        public void CashoutButtonEnabled_WithPlayerNotInGame_IsFalse()
        {
            const bool isManager = false;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest();
            var cashgame = new CashgameInTest(status: GameStatus.Running);

            var sut = GetSut();
            var result = sut.Create(homegame, cashgame, player, isManager);

            Assert.IsFalse(result.CashoutButtonEnabled);
        }

        [Test]
        public void CashoutButtonEnabled_WithPlayerInGame_IsTrue()
        {
            const bool isManager = false;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();
            var cashgameResults = new List<CashgameResult> { cashgameResult };
            var cashgame = new CashgameInTest(status: GameStatus.Running, results: cashgameResults);

            var sut = GetSut();
            var result = sut.Create(homegame, cashgame, player, isManager);

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
        public void ShowTable_WithStartedGame_IsTrue()
        {
            const bool isManager = false;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest();
            var cashgameResults = new CashgameResultInTest();
            var cashgame = new CashgameInTest(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResults }, startTime: new DateTime(), isStarted: true);

            var sut = GetSut();
            var result = sut.Create(homegame, cashgame, player, isManager);

            Assert.IsTrue(result.ShowTable);
        }

        [Test]
        public void ShowTable_WithStartedGameButNoResults_IsFalse()
        {
            const bool isManager = false;
            var homegame = new HomegameInTest();
            var player = new PlayerInTest();
            var cashgameResult = new CashgameResultInTest();
            var cashgame = new CashgameInTest(status: GameStatus.Running, results: new List<CashgameResult> { cashgameResult }, startTime: new DateTime());

            var sut = GetSut();
            var result = sut.Create(homegame, cashgame, player, isManager);

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

        private RunningCashgamePageModelFactory GetSut()
        {
            return new RunningCashgamePageModelFactory(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<IRunningCashgameTableModelFactory>().Object,
                GetMock<IGlobalization>().Object);
        }

    }

}