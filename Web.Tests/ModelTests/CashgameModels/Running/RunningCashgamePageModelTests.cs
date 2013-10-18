using System;
using System.Collections.Generic;
using Core.Classes;
using Infrastructure.System;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories;
using Web.Models.CashgameModels.Running;
using Web.Models.UrlModels;

namespace Web.Tests.ModelTests.CashgameModels.Running{

	class RunningCashgamePageModelTests : WebMockContainer {

		private Homegame _homegame;
		private Cashgame _cashgame;
		private Player _player;
		private bool _isManager;

        [SetUp]
		public void SetUp()
        {
            _homegame = null;
            _cashgame = null;
            _player = null;
			_isManager = false;
		}

		[Test]
        public void StartTime_WithStartTime_IsSet(){
			SetupPlayerIsInGame();
			_cashgame.IsStarted = true;
			_cashgame.StartTime = DateTime.Parse("2010-01-01 01:00:00");

			var result = GetResult();

			Assert.AreEqual("01:00", result.StartTime);
		}

		[Test]
        public void StartTime_NoStartTime_IsNull(){
			SetupPlayerIsInGame();

            var result = GetResult();

            Assert.IsNull(result.StartTime);
		}

		[Test]
        public void ShowStartTime_WithStartTime_IsTrue(){
			SetupPlayerIsInGame();
			_cashgame.IsStarted = true;
			_cashgame.StartTime = DateTime.Parse("2010-01-01 01:00:00");

            var result = GetResult();

            Assert.IsTrue(result.ShowStartTime);
		}

		[Test]
        public void ShowStartTime_NoStartTime_IsFalse(){
			SetupPlayerIsInGame();
			_cashgame.StartTime = null;

            var result = GetResult();

            Assert.IsFalse(result.ShowStartTime);
		}

		[Test]
        public void Location_IsSet(){
			SetupPlayerIsInGame();
		    _cashgame.Location = "a";

            var result = GetResult();

            Assert.AreEqual("a", result.Location);
		}

		[Test]
        public void BuyinUrl_IsSet()
		{
		    const string buyinUrl = "a";
            SetupPlayerIsInGame();
		    Mocks.UrlProviderMock.Setup(o => o.GetCashgameBuyinUrl(_homegame, _player)).Returns(buyinUrl);

            var result = GetResult();

            Assert.AreEqual(buyinUrl, result.BuyinUrl);
		}

		[Test]
        public void ReportUrl_IsSet(){
			SetupPlayerIsInGame();

            var result = GetResult();

            Assert.IsInstanceOf<CashgameReportUrlModel>(result.ReportUrl);
		}

		[Test]
        public void CashoutUrl_IsSet(){
			SetupPlayerIsInGame();

            var result = GetResult();

            Assert.IsInstanceOf<CashgameCashoutUrlModel>(result.CashoutUrl);
		}

		[Test]
        public void BuyinButtonEnabled_IsTrue(){
			SetupPlayerIsInGame();

            var result = GetResult();

            Assert.IsTrue(result.BuyinButtonEnabled);
		}

		[Test]
        public void ReportButtonEnabled_WithPlayerNotInGame_IsFalse(){
			SetupPlayerIsNotInGame();

            var result = GetResult();

            Assert.IsFalse(result.ReportButtonEnabled);
		}

		[Test]
        public void ReportButtonEnabled_WithPlayerInGame_IsTrue(){
			SetupPlayerIsInGame();

            var result = GetResult();

            Assert.IsTrue(result.ReportButtonEnabled);
		}

		[Test]
        public void CashoutButtonEnabled_WithPlayerNotInGame_IsFalse(){
			SetupPlayerIsNotInGame();

            var result = GetResult();

            Assert.IsFalse(result.CashoutButtonEnabled);
		}

		[Test]
        public void CashoutButtonEnabled_WithPlayerInGame_IsTrue(){
			SetupPlayerIsInGame();

            var result = GetResult();

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
			SetupPlayerIsInGame();
			_cashgame.IsStarted = true;
            _cashgame.StartTime = new DateTime();

            var result = GetResult();

            Assert.IsTrue(result.ShowTable);
		}

		[Test]
        public void ShowTable_WithStartedGameButNoResults_IsFalse(){
			SetupPlayerIsInGame();
			_cashgame.StartTime = new DateTime();

            var result = GetResult();

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

        private void SetupHomegameAndRunningCashgameWithOnePlayer(){
			_homegame = new Homegame();
			_cashgame = new Cashgame {Status = GameStatus.Running};
        }

		private void SetupPlayerIsInGame(){
			SetupHomegameAndRunningCashgameWithOnePlayer();
			_player = new Player();
			var result = new CashgameResult {Player = _player};
		    _cashgame.Results = new List<CashgameResult>{result};
		}

		private void SetupPlayerIsNotInGame(){
			SetupHomegameAndRunningCashgameWithOnePlayer();
			_player = new Player();
		}

        private RunningCashgamePageModel GetResult()
        {
            return GetSut().Create(new User(), _homegame, _cashgame, _player, null, _isManager);
        }

		private RunningCashgamePageModelFactory GetSut(){
            return new RunningCashgamePageModelFactory(
                Mocks.PagePropertiesFactoryMock.Object,
                Mocks.RunningCashgameTableModelFactoryMock.Object,
                Mocks.UrlProviderMock.Object);
		}

	}

}