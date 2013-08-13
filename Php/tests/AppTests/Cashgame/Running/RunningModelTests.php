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
			$this->isManager = false;
		}

		private function setupHomegameAndRunningCashgameWithOnePlayer(){
			$this->homegame = new Homegame();
			$this->cashgame = new Cashgame();
			$this->cashgame->setStatus(GameStatus::running);
		}

		private function setupPlayerIsInGame(){
			$this->setupHomegameAndRunningCashgameWithOnePlayer();
			$result = new CashgameResult();
			$this->player = new Player();
			$result->setPlayer($this->player);
			$this->cashgame->setResults(array($result));
		}

		private function setupPlayerIsNotInGame(){
			$this->setupHomegameAndRunningCashgameWithOnePlayer();
			$this->player = new Player();
		}

		function test_StartTime_WithStartTime_IsSet(){
			$this->setupPlayerIsInGame();
			$this->cashgame->setIsStarted(true);
			$this->cashgame->setStartTime(new DateTime('2010-01-01 01:00:00'));

			$sut = $this->getSut();

			$this->assertEqual("01:00", $sut->startTime);
		}

		function test_StartTime_NoStartTime_IsNull(){
			$this->setupPlayerIsInGame();

			$sut = $this->getSut();

			$this->assertNull($sut->startTime);
		}

		function test_ShowStartTime_WithStartTime_IsTrue(){
			$this->setupPlayerIsInGame();
			$this->cashgame->setIsStarted(true);
			$this->cashgame->setStartTime(new DateTime('2010-01-01 01:00:00'));

			$sut = $this->getSut();

			$this->assertTrue($sut->showStartTime);
		}

		function test_ShowStartTime_NoStartTime_IsFalse(){
			$this->setupPlayerIsInGame();
			$this->cashgame->setStartTime(null);
			$sut = $this->getSut();
			$this->assertFalse($sut->showStartTime);
		}

		function test_Location_IsSet(){
			$this->setupPlayerIsInGame();
			$this->cashgame->setLocation('a');

			$sut = $this->getSut();

			$this->assertEqual('a', $sut->location);
		}

		function test_BuyinUrl_IsSet(){
			$this->setupPlayerIsInGame();
			$sut = $this->getSut();

			$this->assertIsA($sut->buyinUrl, 'app\Urls\CashgameBuyinUrlModel');
		}

		function test_ReportUrl_IsSet(){
			$this->setupPlayerIsInGame();
			$sut = $this->getSut();

			$this->assertIsA($sut->reportUrl, 'app\Urls\CashgameReportUrlModel');
		}

		function test_CashoutUrl_IsSet(){
			$this->setupPlayerIsInGame();
			$sut = $this->getSut();

			$this->assertIsA($sut->cashoutUrl, 'app\Urls\CashgameCashoutUrlModel');
		}

		function test_BuyinButtonEnabled_IsTrue(){
			$this->setupPlayerIsInGame();
			$sut = $this->getSut();

			$this->assertTrue($sut->buyinButtonEnabled);
		}

		function test_ReportButtonEnabled_WithPlayerNotInGame_IsFalse(){
			$this->setupPlayerIsNotInGame();
			$sut = $this->getSut();
			$this->assertFalse($sut->reportButtonEnabled);
		}

		function test_ReportButtonEnabled_WithPlayerInGame_IsTrue(){
			$this->setupPlayerIsInGame();
			$sut = $this->getSut();
			$this->assertTrue($sut->reportButtonEnabled);
		}

		function test_CashoutButtonEnabled_WithPlayerNotInGame_IsFalse(){
			$this->setupPlayerIsNotInGame();
			$sut = $this->getSut();
			$this->assertFalse($sut->cashoutButtonEnabled);
		}

		function test_CashoutButtonEnabled_WithPlayerInGame_IsTrue(){
			$this->setupPlayerIsInGame();
			$sut = $this->getSut();
			$this->assertTrue($sut->cashoutButtonEnabled);
		}

		/*
		function test_EndGameButtonEnabled_AtLeastOnePlayerIsStillInGame_IsFalse(){
			$this->setupHomegameAndRunningCashgameWithOnePlayer();
			$this->cashgame->setStarted();
			$this->cashgame->setHasActivePlayers();
			$sut = $this->getSut();
			$this->assertFalse($sut->cashoutButtonEnabled);
		}

		function test_EndGameButtonEnabled_AllPlayersCashedOut_IsTrue(){
			$this->setupHomegameAndRunningCashgameWithOnePlayer();
			$this->cashgame->setStarted();
			$sut = $this->getSut();
			$this->assertTrue($sut->cashoutButtonEnabled);
		}
		*/

		function test_StatusTableModel_WithCreatedGame_IsCorrectType(){
			$this->setupPlayerIsInGame();
			$this->cashgame->setIsStarted(true);

			$sut = $this->getSut();

			$this->assertIsA($sut->statusTableModel, 'app\Cashgame\Running\StatusTableModel');
		}

		function test_ShowTable_WithStartedGame_IsTrue(){
			$this->setupPlayerIsInGame();
			$this->cashgame->setIsStarted(true);

			$sut = $this->getSut();

			$this->assertTrue($sut->showTable);
		}

		function test_ShowTable_WithStartedGameButNoResults_IsFalse(){
			$this->setupPlayerIsInGame();
			$this->cashgame->setStartTime();

			$sut = $this->getSut();

			$this->assertFalse($sut->showTable);
		}

		/*
		function test_ChartDataUrl_IsSet(){
			$this->setupPlayerIsInGame();

			$sut = $this->getModel();

			$this->assertIsA($sut->chartDataUrl, 'app\Urls\CashgameDetailsChartJsonUrlModel');
		}
		*/

		private function getSut(){
			return new RunningModel(new User(), $this->homegame, $this->cashgame, $this->player, null, $this->isManager, new TimerFake());
		}

	}

}