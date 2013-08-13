namespace tests\AppTests\Cashgame\Facts{

	use app\Cashgame\Facts\CashgameFactsModel;
	use entities\CashgameResult;
	use entities\CashgameSuite;
	use entities\CashgameTotalResult;
	use entities\Player;
	use entities\Homegame;
	use entities\GameStatus;
	use Domain\Classes\User;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class CashgameFactsModelTests extends UnitTestCase {

		/** @var CashgameSuite */
		private $suite;

		public function setUp(){
			suite = new CashgameSuite();
		}

		function test_GameCount_SuiteHasGameCount_IsSet(){
			suite.setGameCount(1);
			$sut = getSut();

			assertEqual(1, $sut.gameCount);
		}

		function test_TotalGameTime_SuiteHasTotalGameTime_IsSet(){
			suite.setTotalGametime(1);
			$sut = getSut();

			assertEqual(1, $sut.totalGameTime);
		}

		function test_BestResultAmount_SuiteHasBestResult_IsSet(){
			$result = new CashgameResult();
			$result.setWinnings(1);
			suite.setBestResult($result);
			$sut = getSut();

			assertEqual('+$1', $sut.bestResultAmount);
		}

		function test_BestResultName_SuiteHasBestResult_IsSet(){
			$player = new Player();
			$player.setDisplayName('a');
			$result = new CashgameResult();
			$result.setPlayer($player);
			suite.setBestResult($result);
			$sut = getSut();

			assertEqual('a', $sut.bestResultName);
		}

		function test_WorstResultAmount_SuiteHasWorstResult_IsSet(){
			$result = new CashgameResult();
			$result.setWinnings(1);
			suite.setWorstResult($result);
			$sut = getSut();

			assertIdentical('+$1', $sut.worstResultAmount);
		}

		function test_WorstResultName_SuiteHasWorstResult_IsSet(){
			$player = new Player();
			$player.setDisplayName('a');
			$result = new CashgameResult();
			$result.setPlayer($player);
			suite.setWorstResult($result);
			$sut = getSut();

			assertEqual('a', $sut.worstResultName);
		}

		function test_MostTimeDuration_SuiteHasBestResult_IsSet(){
			$result = new CashgameTotalResult();
			$result.setTimePlayed(1);
			suite.setMostTimeResult($result);
			$sut = getSut();

			assertIdentical('1m', $sut.mostTimeDuration);
		}

		function test_MostTimeName_SuiteHasBestResult_IsSet(){
			$player = new Player();
			$player.setDisplayName('a');
			$result = new CashgameTotalResult();
			$result.setPlayer($player);
			suite.setMostTimeResult($result);
			$sut = getSut();

			assertEqual('a', $sut.mostTimeName);
		}

		private function getSut(){
			return new CashgameFactsModel(new User(), new Homegame(), suite);
		}

	}

}