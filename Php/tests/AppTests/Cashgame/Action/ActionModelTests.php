<?php
namespace tests\AppTests\Cashgame\Action{

	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Player;
	use entities\Homegame;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use entities\Role;
	use entities\Checkpoints\ReportCheckpoint;
	use app\Cashgame\Action\ActionModel;
	use DateTime;
	use tests\TestHelper;

	class ActionModelTests extends UnitTestCase {

		private $homegame;
		/** @var Player */
		private $player;
		/** @var Cashgame */
		private $cashgame;
		/** @var CashgameResult */
		private $result;

		function setUp(){
			$this->homegame = new Homegame();
			$this->player = new Player();
			$this->cashgame = new Cashgame();
			$this->result = new CashgameResult();
			$this->result->setPlayer($this->player);
		}

		function getSut(){
			return new ActionModel(new User(), $this->homegame, $this->cashgame, $this->player, $this->result, Role::$player);
		}

		function test_Heading_IsSet(){
			$this->player->setDisplayName('a');
			$this->cashgame->setStartTime(new DateTime("2010-01-01 01:00:00"));

			$sut = $this->getSut();

			$this->assertEqual($sut->heading, 'Cashgame Jan 1 2010, a');
		}

		function test_Checkpoints_WithOneCheckpoint_HasOneCheckpoint(){
			$timestamp = new DateTime("2010-01-01 01:00:00");
			$stack = 1;
			$checkpoint = new ReportCheckpoint($timestamp, $stack);
			$this->result->setCheckpoints(array($checkpoint));

			$sut = $this->getSut();

			$checkpoints = $sut->checkpoints;
			$this->assertIdentical(count($checkpoints), 1);
		}

		function test_ChartDataUrl_IsSet(){
			$sut = $this->getSut();

			$this->assertIsA($sut->chartDataUrl, 'app\Urls\CashgameActionChartJsonUrlModel');
		}

	}

}