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
			$this->cashgames = array();
			$this->players = array();
			$this->cashgameTotalResultFactory = TestHelper::getFake(ClassNames::$CashgameTotalResultFactory);
		}

		function test_GetTotalResults_ReturnsCorrectTotalsSortedOnWinningsDescending(){
			$this->setUpTwoGamesWithOneWinningAndOneLosingPlayer();

			$sut = $this->getSut();
			$result = $sut->getTotalResults();

			$this->assertEqual(2, count($result));
			$this->assertEqual(3, $result[0]->getWinnings());
			$this->assertEqual(-3, $result[1]->getWinnings());
		}

		function test_GetCashgames_ReturnsTheCashgames(){
			$cashgame = new Cashgame();
			$cashgame->setId(1);
			$this->cashgames[] = $cashgame;

			$sut = $this->getSut();
			$result = $sut->getCashgames();

			$this->assertEqual(1, count($result));
			$this->assertEqual(1, $result[0]->getId());
		}

		function test_GetGameCount_ReturnsCorrectCount(){
			$cashgame = new Cashgame();
			$this->cashgames[] = $cashgame;

			$sut = $this->getSut();
			$result = $sut->getGameCount();

			$this->assertEqual(1, $result);
		}

		function test_GetBestTotalResult_ReturnsTheTotalResultWithTheHighestWinnings(){
			$this->setUpTwoGamesWithOneWinningAndOneLosingPlayer();

			$sut = $this->getSut();
			$result = $sut->getBestTotalResult();

			$this->assertEqual(3, $result->getWinnings());
		}

		function test_GetBestResult_ReturnsTheResultWithTheHighestWinnings(){
			$this->setUpTwoGamesWithOneWinningAndOneLosingPlayer();

			$sut = $this->getSut();
			$result = $sut->getBestResult();

			$this->assertEqual(2, $result->getWinnings());
		}

		function test_GetWorstResult_ReturnsTheResultWithTheLowestWinnings(){
			$this->setUpTwoGamesWithOneWinningAndOneLosingPlayer();

			$sut = $this->getSut();
			$result = $sut->getWorstResult();

			$this->assertEqual(-2, $result->getWinnings());
		}

		function test_GetMostTime_ReturnsTheResultWithTheMostTimePlayed(){
			$this->setUpTwoGamesWithOneWinningAndOneLosingPlayer();

			$sut = $this->getSut();
			$result = $sut->getMostTimeResult();

			$this->assertEqual(4, $result->getTimePlayed());
		}

		function test_GetTotalGameTime_ReturnsTheSumOfTheGameDurations(){
			$cashgame1 = new Cashgame();
			$cashgame1->setDuration(1);

			$cashgame2 = new Cashgame();
			$cashgame2->setDuration(2);

			$this->cashgames = array($cashgame1, $cashgame2);

			$sut = $this->getSut();
			$result = $sut->getTotalGametime();

			$this->assertEqual(3, $result);
		}

		private function setUpTwoGamesWithOneWinningAndOneLosingPlayer(){
			$player1 = new Player();
			$player1->setId(1);
			$player2 = new Player();
			$player2->setId(2);

			$cashgame1result1 = new CashgameResult();
			$cashgame1result1->setWinnings(-1);
			$cashgame1result1->setPlayedTime(1);
			$cashgame1result1->setPlayer($player1);
			$cashgame1result2 = new CashgameResult();
			$cashgame1result2->setWinnings(1);
			$cashgame1result2->setPlayedTime(2);
			$cashgame1result2->setPlayer($player2);
			$cashgame1 = new Cashgame();
			$cashgame1->setResults(array($cashgame1result1, $cashgame1result2));

			$cashgame2result1 = new CashgameResult();
			$cashgame2result1->setWinnings(-2);
			$cashgame2result1->setPlayedTime(1);
			$cashgame2result1->setPlayer($player1);
			$cashgame2result2 = new CashgameResult();
			$cashgame2result2->setWinnings(2);
			$cashgame2result2->setPlayedTime(2);
			$cashgame2result2->setPlayer($player2);
			$cashgame2 = new Cashgame();
			$cashgame2->setResults(array($cashgame2result1, $cashgame2result2));

			$this->cashgames = array($cashgame1, $cashgame2);
			$this->players = array($player1, $player2);

			$totalResult1 = new CashgameTotalResult();
			$totalResult1->setPlayer($player1);
			$totalResult1->setWinnings(-3);
			$totalResult1->setTimePlayed(2);

			$totalResult2 = new CashgameTotalResult();
			$totalResult2->setPlayer($player2);
			$totalResult2->setWinnings(3);
			$totalResult1->setTimePlayed(4);

			$this->cashgameTotalResultFactory->returns('create', $totalResult1, array($player1, array($cashgame1result1, $cashgame2result1)));
			$this->cashgameTotalResultFactory->returns('create', $totalResult2, array($player2, array($cashgame1result2, $cashgame2result2)));
		}

		private function getSut(){
			$cashgameSuiteFactory = new CashgameSuiteFactoryImpl($this->cashgameTotalResultFactory);
			return $cashgameSuiteFactory->create($this->cashgames, $this->players);
		}

	}

}