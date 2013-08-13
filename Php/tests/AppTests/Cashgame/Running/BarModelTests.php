<?php
namespace tests\AppTests\Cashgame\Running{

	use app\Cashgame\Running\BarModel;
	use entities\Cashgame;
	use entities\GameStatus;
	use entities\Homegame;
	use tests\UnitTestCase;
	use tests\TestHelper;

	class BarModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;
		private $runningGame;

		function setUp(){
			$this->homegame = new Homegame();
			$runningGame = null;
		}

		function test_GameIsRunning_NoRunningGame_IsFalse(){
			$sut = $this->getSut();

			$this->assertFalse($sut->gameIsRunning);
		}

		function test_GameIsRunning_WithRunningGame_IsTrue(){
			$this->setRunningGame();

			$sut = $this->getSut();

			$this->assertTrue($sut->gameIsRunning);
		}

		function test_GameUrl_WithRunningGame_IsSet(){
			$this->setRunningGame();

			$sut = $this->getSut();

			$this->assertIsA($sut->gameUrl, 'app\Urls\RunningCashgameUrlModel');
		}

		function setRunningGame(){
			$this->runningGame = new Cashgame();
		}

		function getSut(){
			return new BarModel($this->homegame, $this->runningGame);
		}

	}

}