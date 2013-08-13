<?php
namespace tests\AppTests\Cashgame\Action{
	use entities\Cashgame;
	use entities\Homegame;
	use entities\Player;
	use tests\UnitTestCase;
	use app\Cashgame\Action\CheckpointModel;
	use DateTime;
	use entities\Checkpoints\ReportCheckpoint;
	use tests\TestHelper;

	class CheckpointModelTests extends UnitTestCase {

		private $checkpoint;
		private $isManager;

		function setUp(){
			$this->checkpoint = new ReportCheckpoint(new DateTime('2010-01-01 01:00:00'), 200);
			$this->isManager = false;
		}

		function getSut(){
			$cashgame = new Cashgame();
			$cashgame->setStartTime(new DateTime());
			$player = new Player();
			return new CheckpointModel(new Homegame(), $cashgame, $player, $this->checkpoint, $this->isManager);
		}

		function test_Timestamp_IsSet(){
			$sut = $this->getSut();

			$this->assertIdentical($sut->timestamp, '01:00');
		}

		function test_Description_IsSet(){
			$sut = $this->getSut();

			$this->assertIdentical($sut->description, 'Report');
		}

		function test_Stack_IsSet(){
			$sut = $this->getSut();

			$this->assertIdentical($sut->stack, '$200');
		}

		function test_ShowLink_NormalUser_IsFalse(){
			$sut = $this->getSut();

			$this->assertFalse($sut->showLink);
		}

		function test_ShowLink_ManagerUser_IsTrue(){
			$this->isManager = true;

			$sut = $this->getSut();

			$this->assertTrue($sut->showLink);
		}

		function test_EditUrl_IsCorrectType(){
			$sut = $this->getSut();

			$this->assertIsA($sut->editUrl, 'app\Urls\CashgameCheckpointDeleteUrlModel');
		}

	}

}