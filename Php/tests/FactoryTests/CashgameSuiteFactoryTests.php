namespace tests\FactoryTests{

	use core\ClassNames;
	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\CashgameSuiteFactoryImpl;
	use entities\CashgameTotalResult;
	use entities\Player;
	use entities\GameStatus;
	use tests\UnitTestCase;
	use tests\TestHelper;

	class CashgameSuiteFactoryTests extends UnitTestCase {

		private $cashgames;
		private $players;

		private $cashgameTotalResultFactory;

		function setUp(){
			cashgames = array();
			players = array();
			cashgameTotalResultFactory = TestHelper::getFake(ClassNames::$CashgameTotalResultFactory);
		}

		function test_GetTotalResults_ReturnsCorrectTotalsSortedOnWinningsDescending(){
			setUpTwoGamesWithOneWinningAndOneLosingPlayer();

			$sut = getSut();
			$result = $sut.getTotalResults();

			assertEqual(2, count($result));
			assertEqual(3, $result[0].getWinnings());
			assertEqual(-3, $result[1].getWinnings());
		}

		function test_GetCashgames_ReturnsTheCashgames(){
			$cashgame = new Cashgame();
			$cashgame.setId(1);
			cashgames[] = $cashgame;

			$sut = getSut();
			$result = $sut.getCashgames();

			assertEqual(1, count($result));
			assertEqual(1, $result[0].getId());
		}

		function test_GetGameCount_ReturnsCorrectCount(){
			$cashgame = new Cashgame();
			cashgames[] = $cashgame;

			$sut = getSut();
			$result = $sut.getGameCount();

			assertEqual(1, $result);
		}

		function test_GetBestTotalResult_ReturnsTheTotalResultWithTheHighestWinnings(){
			setUpTwoGamesWithOneWinningAndOneLosingPlayer();

			$sut = getSut();
			$result = $sut.getBestTotalResult();

			assertEqual(3, $result.getWinnings());
		}

		function test_GetBestResult_ReturnsTheResultWithTheHighestWinnings(){
			setUpTwoGamesWithOneWinningAndOneLosingPlayer();

			$sut = getSut();
			$result = $sut.getBestResult();

			assertEqual(2, $result.getWinnings());
		}

		function test_GetWorstResult_ReturnsTheResultWithTheLowestWinnings(){
			setUpTwoGamesWithOneWinningAndOneLosingPlayer();

			$sut = getSut();
			$result = $sut.getWorstResult();

			assertEqual(-2, $result.getWinnings());
		}

		function test_GetMostTime_ReturnsTheResultWithTheMostTimePlayed(){
			setUpTwoGamesWithOneWinningAndOneLosingPlayer();

			$sut = getSut();
			$result = $sut.getMostTimeResult();

			assertEqual(4, $result.getTimePlayed());
		}

		function test_GetTotalGameTime_ReturnsTheSumOfTheGameDurations(){
			$cashgame1 = new Cashgame();
			$cashgame1.setDuration(1);

			$cashgame2 = new Cashgame();
			$cashgame2.setDuration(2);

			cashgames = array($cashgame1, $cashgame2);

			$sut = getSut();
			$result = $sut.getTotalGametime();

			assertEqual(3, $result);
		}

		private function setUpTwoGamesWithOneWinningAndOneLosingPlayer(){
			$player1 = new Player();
			$player1.setId(1);
			$player2 = new Player();
			$player2.setId(2);

			$cashgame1result1 = new CashgameResult();
			$cashgame1result1.setWinnings(-1);
			$cashgame1result1.setPlayedTime(1);
			$cashgame1result1.setPlayer($player1);
			$cashgame1result2 = new CashgameResult();
			$cashgame1result2.setWinnings(1);
			$cashgame1result2.setPlayedTime(2);
			$cashgame1result2.setPlayer($player2);
			$cashgame1 = new Cashgame();
			$cashgame1.setResults(array($cashgame1result1, $cashgame1result2));

			$cashgame2result1 = new CashgameResult();
			$cashgame2result1.setWinnings(-2);
			$cashgame2result1.setPlayedTime(1);
			$cashgame2result1.setPlayer($player1);
			$cashgame2result2 = new CashgameResult();
			$cashgame2result2.setWinnings(2);
			$cashgame2result2.setPlayedTime(2);
			$cashgame2result2.setPlayer($player2);
			$cashgame2 = new Cashgame();
			$cashgame2.setResults(array($cashgame2result1, $cashgame2result2));

			cashgames = array($cashgame1, $cashgame2);
			players = array($player1, $player2);

			$totalResult1 = new CashgameTotalResult();
			$totalResult1.setPlayer($player1);
			$totalResult1.setWinnings(-3);
			$totalResult1.setTimePlayed(2);

			$totalResult2 = new CashgameTotalResult();
			$totalResult2.setPlayer($player2);
			$totalResult2.setWinnings(3);
			$totalResult1.setTimePlayed(4);

			cashgameTotalResultFactory.returns('create', $totalResult1, array($player1, array($cashgame1result1, $cashgame2result1)));
			cashgameTotalResultFactory.returns('create', $totalResult2, array($player2, array($cashgame1result2, $cashgame2result2)));
		}

		private function getSut(){
			$cashgameSuiteFactory = new CashgameSuiteFactoryImpl(cashgameTotalResultFactory);
			return $cashgameSuiteFactory.create(cashgames, players);
		}

	}

}