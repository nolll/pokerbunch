<?php
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
			$this->isManager = false;
			$this->homegame = new Homegame();
			$this->cashgame = new Cashgame();
			$this->player = new Player();
		}

		function test_Heading_IsSet(){
			$this->cashgame->setStartTime(new DateTime('2010-01-01 01:00:00'));

			$sut = $this->getSut();

			$this->assertEqual("Cashgame Jan 1 2010", $sut->heading);
		}

		function test_Duration_IsSet(){
			$this->cashgame->setDuration(1);

			$sut = $this->getSut();

			$this->assertEqual("1m", $sut->duration);
		}

		function test_Duration_WithDurationLargerThanZero_IsEnabled(){
			$this->cashgame->setDuration(1);

			$sut = $this->getSut();

			$this->assertTrue($sut->durationEnabled);
		}

		function test_StartTime_WithRunningGame_IsSet(){
			$this->cashgame->setStartTime(new DateTime('2010-01-01 01:00:00'));
			$this->cashgame->setStatus(GameStatus::running);

			$sut = $this->getSut();

			$this->assertEqual("01:00", $sut->startTime);
		}

		function test_EndTime_WithFinishedGame_IsSet(){
			$this->cashgame->setEndTime(new DateTime('2010-01-01 01:00:00'));
			$this->cashgame->setStatus(GameStatus::finished);

			$sut = $this->getSut();

			$this->assertEqual("01:00", $sut->endTime);
		}

		function test_Location_IsSet(){
			$this->cashgame->setLocation('a');

			$sut = $this->getSut();

			$this->assertEqual('a', $sut->location);
		}

		function test_EditUrl_IsCorrectType(){
			$sut = $this->getSut();

			$this->assertIsA($sut->editUrl, 'app\Urls\CashgameEditUrlModel');
		}

		function test_CheckpointsUrl_IsCorrectType(){
			$sut = $this->getSut();

			$this->assertIsA($sut->checkpointsUrl, 'app\Urls\CashgameActionUrlModel');
		}

		function test_ShowStartTime_WithCreatedGame_IsFalse(){
			$this->cashgame->setStatus(GameStatus::created);

			$sut = $this->getSut();

			$this->assertFalse($sut->showStartTime);
		}

		function test_ShowEndTime_WithCreatedGame_IsFalse(){
			$this->cashgame->setStatus(GameStatus::created);

			$sut = $this->getSut();

			$this->assertFalse($sut->showEndTime);
		}

		function test_EnableCheckpointsButton_WithPlayerNotInGame_IsFalse(){
			$sut = $this->getSut();

			$this->assertFalse($sut->enableCheckpointsButton);
		}

		function test_EnableCheckpointsButton_WithPlayerInGame_IsTrue(){
			$this->player->setId(1);

			$cashgameResult = new CashgameResult();
			$cashgameResult->setPlayer($this->player);

			$this->cashgame->setResults(array($cashgameResult));

			$sut = $this->getSut();

			$this->assertTrue($sut->enableCheckpointsButton);
		}

		function test_EnableCheckpointsButton_WithFinishedGame_IsFalse(){
			$this->cashgame->setStatus(GameStatus::finished);

			$sut = $this->getSut();

			$this->assertFalse($sut->enableCheckpointsButton);
		}

		function test_ShowEndTime_WithRunningGame_IsFalse(){
			$this->cashgame->setStatus(GameStatus::running);

			$sut = $this->getSut();

			$this->assertFalse($sut->showEndTime);
		}

		function test_EnableEdit_WithPlayerRights_IsFalse(){
			$sut = $this->getSut();

			$this->assertFalse($sut->enableEdit);
		}

		function test_EnableEdit_WithManagerRights_IsTrue(){
			$this->isManager = true;

			$sut = $this->getSut();

			$this->assertTrue($sut->enableEdit);
		}

		function test_Status_IsNotNull(){
			$sut = $this->getSut();

			$this->assertNotNull($sut->status);
		}

		function test_ResultTableModel_WithCreatedGame_IsCorrectType(){
			$sut = $this->getSut();

			$this->assertIsA($sut->resultTableModel, 'app\Cashgame\Details\ResultTable\ResultTableModel');
		}

		function test_ChartDataUrl_IsSet(){
			$sut = $this->getSut();

			$this->assertIsA($sut->chartDataUrl, 'app\Urls\CashgameDetailsChartJsonUrlModel');
		}

		private function getSut(){
			return new DetailsModel(new User(), $this->homegame, $this->cashgame, $this->player, null, $this->isManager);
		}

	}

}