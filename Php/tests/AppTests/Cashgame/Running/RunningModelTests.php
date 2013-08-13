namespace tests\AppTests\Cashgame\Running{

	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Player;
	use tests\Fakes\TimerFake;
	use DateTime;
	use entities\Homegame;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\Cashgame\Running\RunningModel;
	use entities\GameStatus;
	use tests\TestHelper;

	class RunningModelTests extends UnitTestCase {

		private $homegame;
		/** @var Cashgame */
		private $cashgame;
		private $player;
		private $isManager;

		public function setUp(){
			isManager = false;
		}

		private function setupHomegameAndRunningCashgameWithOnePlayer(){
			homegame = new Homegame();
			cashgame = new Cashgame();
			cashgame.setStatus(GameStatus::running);
		}

		private function setupPlayerIsInGame(){
			setupHomegameAndRunningCashgameWithOnePlayer();
			$result = new CashgameResult();
			player = new Player();
			$result.setPlayer(player);
			cashgame.setResults(array($result));
		}

		private function setupPlayerIsNotInGame(){
			setupHomegameAndRunningCashgameWithOnePlayer();
			player = new Player();
		}

		function test_StartTime_WithStartTime_IsSet(){
			setupPlayerIsInGame();
			cashgame.setIsStarted(true);
			cashgame.setStartTime(new DateTime('2010-01-01 01:00:00'));

			$sut = getSut();

			assertEqual("01:00", $sut.startTime);
		}

		function test_StartTime_NoStartTime_IsNull(){
			setupPlayerIsInGame();

			$sut = getSut();

			assertNull($sut.startTime);
		}

		function test_ShowStartTime_WithStartTime_IsTrue(){
			setupPlayerIsInGame();
			cashgame.setIsStarted(true);
			cashgame.setStartTime(new DateTime('2010-01-01 01:00:00'));

			$sut = getSut();

			assertTrue($sut.showStartTime);
		}

		function test_ShowStartTime_NoStartTime_IsFalse(){
			setupPlayerIsInGame();
			cashgame.setStartTime(null);
			$sut = getSut();
			assertFalse($sut.showStartTime);
		}

		function test_Location_IsSet(){
			setupPlayerIsInGame();
			cashgame.setLocation('a');

			$sut = getSut();

			assertEqual('a', $sut.location);
		}

		function test_BuyinUrl_IsSet(){
			setupPlayerIsInGame();
			$sut = getSut();

			assertIsA($sut.buyinUrl, 'app\Urls\CashgameBuyinUrlModel');
		}

		function test_ReportUrl_IsSet(){
			setupPlayerIsInGame();
			$sut = getSut();

			assertIsA($sut.reportUrl, 'app\Urls\CashgameReportUrlModel');
		}

		function test_CashoutUrl_IsSet(){
			setupPlayerIsInGame();
			$sut = getSut();

			assertIsA($sut.cashoutUrl, 'app\Urls\CashgameCashoutUrlModel');
		}

		function test_BuyinButtonEnabled_IsTrue(){
			setupPlayerIsInGame();
			$sut = getSut();

			assertTrue($sut.buyinButtonEnabled);
		}

		function test_ReportButtonEnabled_WithPlayerNotInGame_IsFalse(){
			setupPlayerIsNotInGame();
			$sut = getSut();
			assertFalse($sut.reportButtonEnabled);
		}

		function test_ReportButtonEnabled_WithPlayerInGame_IsTrue(){
			setupPlayerIsInGame();
			$sut = getSut();
			assertTrue($sut.reportButtonEnabled);
		}

		function test_CashoutButtonEnabled_WithPlayerNotInGame_IsFalse(){
			setupPlayerIsNotInGame();
			$sut = getSut();
			assertFalse($sut.cashoutButtonEnabled);
		}

		function test_CashoutButtonEnabled_WithPlayerInGame_IsTrue(){
			setupPlayerIsInGame();
			$sut = getSut();
			assertTrue($sut.cashoutButtonEnabled);
		}

		/*
		function test_EndGameButtonEnabled_AtLeastOnePlayerIsStillInGame_IsFalse(){
			setupHomegameAndRunningCashgameWithOnePlayer();
			cashgame.setStarted();
			cashgame.setHasActivePlayers();
			$sut = getSut();
			assertFalse($sut.cashoutButtonEnabled);
		}

		function test_EndGameButtonEnabled_AllPlayersCashedOut_IsTrue(){
			setupHomegameAndRunningCashgameWithOnePlayer();
			cashgame.setStarted();
			$sut = getSut();
			assertTrue($sut.cashoutButtonEnabled);
		}
		*/

		function test_StatusTableModel_WithCreatedGame_IsCorrectType(){
			setupPlayerIsInGame();
			cashgame.setIsStarted(true);

			$sut = getSut();

			assertIsA($sut.statusTableModel, 'app\Cashgame\Running\StatusTableModel');
		}

		function test_ShowTable_WithStartedGame_IsTrue(){
			setupPlayerIsInGame();
			cashgame.setIsStarted(true);

			$sut = getSut();

			assertTrue($sut.showTable);
		}

		function test_ShowTable_WithStartedGameButNoResults_IsFalse(){
			setupPlayerIsInGame();
			cashgame.setStartTime();

			$sut = getSut();

			assertFalse($sut.showTable);
		}

		/*
		function test_ChartDataUrl_IsSet(){
			setupPlayerIsInGame();

			$sut = getModel();

			assertIsA($sut.chartDataUrl, 'app\Urls\CashgameDetailsChartJsonUrlModel');
		}
		*/

		private function getSut(){
			return new RunningModel(new User(), homegame, cashgame, player, null, isManager, new TimerFake());
		}

	}

}