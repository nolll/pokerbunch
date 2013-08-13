namespace tests\AppTests\Cashgame\Details{

	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Homegame;
	use entities\Player;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use DateTime;
	use app\Cashgame\Details\DetailsModel;
	use entities\GameStatus;
	use tests\TestHelper;

	class DetailsModelTests extends UnitTestCase {

		private $homegame;
		/** @var Cashgame */
		private $cashgame;
		/** @var Player */
		private $player;
		private $isManager;

		function setUp(){
			isManager = false;
			homegame = new Homegame();
			cashgame = new Cashgame();
			player = new Player();
		}

		function test_Heading_IsSet(){
			cashgame.setStartTime(new DateTime('2010-01-01 01:00:00'));

			$sut = getSut();

			assertEqual("Cashgame Jan 1 2010", $sut.heading);
		}

		function test_Duration_IsSet(){
			cashgame.setDuration(1);

			$sut = getSut();

			assertEqual("1m", $sut.duration);
		}

		function test_Duration_WithDurationLargerThanZero_IsEnabled(){
			cashgame.setDuration(1);

			$sut = getSut();

			assertTrue($sut.durationEnabled);
		}

		function test_StartTime_WithRunningGame_IsSet(){
			cashgame.setStartTime(new DateTime('2010-01-01 01:00:00'));
			cashgame.setStatus(GameStatus::running);

			$sut = getSut();

			assertEqual("01:00", $sut.startTime);
		}

		function test_EndTime_WithFinishedGame_IsSet(){
			cashgame.setEndTime(new DateTime('2010-01-01 01:00:00'));
			cashgame.setStatus(GameStatus::finished);

			$sut = getSut();

			assertEqual("01:00", $sut.endTime);
		}

		function test_Location_IsSet(){
			cashgame.setLocation('a');

			$sut = getSut();

			assertEqual('a', $sut.location);
		}

		function test_EditUrl_IsCorrectType(){
			$sut = getSut();

			assertIsA($sut.editUrl, 'app\Urls\CashgameEditUrlModel');
		}

		function test_CheckpointsUrl_IsCorrectType(){
			$sut = getSut();

			assertIsA($sut.checkpointsUrl, 'app\Urls\CashgameActionUrlModel');
		}

		function test_ShowStartTime_WithCreatedGame_IsFalse(){
			cashgame.setStatus(GameStatus::created);

			$sut = getSut();

			assertFalse($sut.showStartTime);
		}

		function test_ShowEndTime_WithCreatedGame_IsFalse(){
			cashgame.setStatus(GameStatus::created);

			$sut = getSut();

			assertFalse($sut.showEndTime);
		}

		function test_EnableCheckpointsButton_WithPlayerNotInGame_IsFalse(){
			$sut = getSut();

			assertFalse($sut.enableCheckpointsButton);
		}

		function test_EnableCheckpointsButton_WithPlayerInGame_IsTrue(){
			player.setId(1);

			$cashgameResult = new CashgameResult();
			$cashgameResult.setPlayer(player);

			cashgame.setResults(array($cashgameResult));

			$sut = getSut();

			assertTrue($sut.enableCheckpointsButton);
		}

		function test_EnableCheckpointsButton_WithFinishedGame_IsFalse(){
			cashgame.setStatus(GameStatus::finished);

			$sut = getSut();

			assertFalse($sut.enableCheckpointsButton);
		}

		function test_ShowEndTime_WithRunningGame_IsFalse(){
			cashgame.setStatus(GameStatus::running);

			$sut = getSut();

			assertFalse($sut.showEndTime);
		}

		function test_EnableEdit_WithPlayerRights_IsFalse(){
			$sut = getSut();

			assertFalse($sut.enableEdit);
		}

		function test_EnableEdit_WithManagerRights_IsTrue(){
			isManager = true;

			$sut = getSut();

			assertTrue($sut.enableEdit);
		}

		function test_Status_IsNotNull(){
			$sut = getSut();

			assertNotNull($sut.status);
		}

		function test_ResultTableModel_WithCreatedGame_IsCorrectType(){
			$sut = getSut();

			assertIsA($sut.resultTableModel, 'app\Cashgame\Details\ResultTable\ResultTableModel');
		}

		function test_ChartDataUrl_IsSet(){
			$sut = getSut();

			assertIsA($sut.chartDataUrl, 'app\Urls\CashgameDetailsChartJsonUrlModel');
		}

		private function getSut(){
			return new DetailsModel(new User(), homegame, cashgame, player, null, isManager);
		}

	}

}