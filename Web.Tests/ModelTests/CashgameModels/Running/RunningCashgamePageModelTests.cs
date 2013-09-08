using System;
using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.Models.CashgameModels.Running;
using Web.Models.UrlModels;

namespace Web.Tests.ModelTests.CashgameModels.Running{

	class RunningCashgamePageModelTests : MockContainer {

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

			var sut = GetSut();

			Assert.AreEqual("01:00", sut.StartTime);
		}

		[Test]
        public void StartTime_NoStartTime_IsNull(){
			SetupPlayerIsInGame();

			var sut = GetSut();

			Assert.IsNull(sut.StartTime);
		}

		[Test]
        public void ShowStartTime_WithStartTime_IsTrue(){
			SetupPlayerIsInGame();
			_cashgame.IsStarted = true;
			_cashgame.StartTime = DateTime.Parse("2010-01-01 01:00:00");

			var sut = GetSut();

			Assert.IsTrue(sut.ShowStartTime);
		}

		[Test]
        public void ShowStartTime_NoStartTime_IsFalse(){
			SetupPlayerIsInGame();
			_cashgame.StartTime = null;
			
            var sut = GetSut();
			Assert.IsFalse(sut.ShowStartTime);
		}

		[Test]
        public void Location_IsSet(){
			SetupPlayerIsInGame();
		    _cashgame.Location = "a";

			var sut = GetSut();

			Assert.AreEqual("a", sut.Location);
		}

		[Test]
        public void BuyinUrl_IsSet(){
			SetupPlayerIsInGame();

			var sut = GetSut();

			Assert.IsInstanceOf<CashgameBuyinUrlModel>(sut.BuyinUrl);
		}

		[Test]
        public void ReportUrl_IsSet(){
			SetupPlayerIsInGame();
			
            var sut = GetSut();

			Assert.IsInstanceOf<CashgameReportUrlModel>(sut.ReportUrl);
		}

		[Test]
        public void CashoutUrl_IsSet(){
			SetupPlayerIsInGame();
			var sut = GetSut();

			Assert.IsInstanceOf<CashgameCashoutUrlModel>(sut.CashoutUrl);
		}

		[Test]
        public void BuyinButtonEnabled_IsTrue(){
			SetupPlayerIsInGame();
			
            var sut = GetSut();

			Assert.IsTrue(sut.BuyinButtonEnabled);
		}

		[Test]
        public void ReportButtonEnabled_WithPlayerNotInGame_IsFalse(){
			SetupPlayerIsNotInGame();
			
            var sut = GetSut();
			
            Assert.IsFalse(sut.ReportButtonEnabled);
		}

		[Test]
        public void ReportButtonEnabled_WithPlayerInGame_IsTrue(){
			SetupPlayerIsInGame();
			
            var sut = GetSut();
			
            Assert.IsTrue(sut.ReportButtonEnabled);
		}

		[Test]
        public void CashoutButtonEnabled_WithPlayerNotInGame_IsFalse(){
			SetupPlayerIsNotInGame();
			
            var sut = GetSut();
			
            Assert.IsFalse(sut.CashoutButtonEnabled);
		}

		[Test]
        public void CashoutButtonEnabled_WithPlayerInGame_IsTrue(){
			SetupPlayerIsInGame();
			
            var sut = GetSut();
			
            Assert.IsTrue(sut.CashoutButtonEnabled);
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
        public void StatusTableModel_WithCreatedGame_IsCorrectType(){
			SetupPlayerIsInGame();
			_cashgame.IsStarted = true;
            _cashgame.StartTime = new DateTime();

			var sut = GetSut();

            Assert.IsInstanceOf<RunningCashgameTableModel>(sut.RunningCashgameTableModel);
		}

		[Test]
        public void ShowTable_WithStartedGame_IsTrue(){
			SetupPlayerIsInGame();
			_cashgame.IsStarted = true;
            _cashgame.StartTime = new DateTime();

			var sut = GetSut();

			Assert.IsTrue(sut.ShowTable);
		}

		[Test]
        public void ShowTable_WithStartedGameButNoResults_IsFalse(){
			SetupPlayerIsInGame();
			_cashgame.StartTime = new DateTime();

			var sut = GetSut();

			Assert.IsFalse(sut.ShowTable);
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

		private RunningCashgamePageModel GetSut(){
			return new RunningCashgamePageModel(new User(), _homegame, _cashgame, _player, null, _isManager, TimeProviderMock.Object);
		}

	}

}