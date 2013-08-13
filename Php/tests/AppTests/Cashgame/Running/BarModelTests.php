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
			homegame = new Homegame();
			$runningGame = null;
		}

		function test_GameIsRunning_NoRunningGame_IsFalse(){
			$sut = getSut();

			assertFalse($sut.gameIsRunning);
		}

		function test_GameIsRunning_WithRunningGame_IsTrue(){
			setRunningGame();

			$sut = getSut();

			assertTrue($sut.gameIsRunning);
		}

		function test_GameUrl_WithRunningGame_IsSet(){
			setRunningGame();

			$sut = getSut();

			assertIsA($sut.gameUrl, 'app\Urls\RunningCashgameUrlModel');
		}

		function setRunningGame(){
			runningGame = new Cashgame();
		}

		function getSut(){
			return new BarModel(homegame, runningGame);
		}

	}

}