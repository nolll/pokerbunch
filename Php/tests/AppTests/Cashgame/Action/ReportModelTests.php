<?php
namespace tests\AppTests\Cashgame\Action{

	use entities\Cashgame;
	use entities\Homegame;
	use Domain\Classes\User;
	use entities\Player;
	use tests\UnitTestCase;
	use app\Cashgame\Action\ReportModel;
	use tests\TestHelper;

	class ReportModelTests extends UnitTestCase {

		private $homegame;
		/** @var Player */
		private $player;
		/** @var Cashgame */

		function setUp(){
			$this->homegame = new Homegame();
			$this->player = new Player();
		}

		function getSut(){
			$runningGame = new Cashgame();
			return new ReportModel(new User(), $this->homegame, $this->player, null, $runningGame);
		}

		function test_ReportUrl_IsCorrectType(){
			$sut = $this->getSut();

			$this->assertIsA($sut->reportUrl, 'app\Urls\CashgameReportUrlModel');
		}

	}

}