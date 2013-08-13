namespace tests\AppTests\Player{

	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Player;
	use entities\Homegame;
	use tests\UnitTestCase;
	use app\Player\Facts\PlayerFactsModel;
	use tests\TestHelper;

	class PlayerFactsModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;
		private $player;

		function setUp(){
			$this->homegame = new Homegame();
		}

		function test_FilterCashames_ReturnsOnlyGamesWhereThePlayerParticipated(){
			$this->player = new Player();
			$this->player->setId(1);
			$cashgameResult = new CashgameResult();
			$cashgameResult->setPlayer($this->player);
			$cashgame1 = new Cashgame();
			$cashgame1->setResults(array($cashgameResult));
			$cashgame2 = new Cashgame();
			$cashgames = array($cashgame1, $cashgame2);

			$filteredCashgames = PlayerFactsModel::filterCashgames($cashgames, $this->player);

			$this->assertIdentical(1, count($filteredCashgames));
		}

		function test_GetWinnings_ReturnsPlayerWinnings(){
			$this->player = new Player();
			$this->player->setId(1);

			$cashgame1 = new Cashgame();
			$result1 = new CashgameResult();
			$result1->setPlayer($this->player);
			$result1->setWinnings(1);
			$cashgame1->setResults(array($result1));

			$cashgame2 = new Cashgame();
			$result2 = new CashgameResult();
			$result2->setPlayer($this->player);
			$result2->setWinnings(2);
			$cashgame2->setResults(array($result2));

			$cashgames = array($cashgame1, $cashgame2);

			$winnings = PlayerFactsModel::getWinnings($cashgames, $this->player);

			$this->assertIdentical(3, $winnings);
		}

		function test_GetBestResultAndGetWorstResult_ReturnsCorrectResults(){
			$this->player = new Player();
			$this->player->setId(1);
			$cashgame1 = new Cashgame();
			$worstResult = new CashgameResult();
			$worstResult->setWinnings(1);
			$worstResult->setPlayer($this->player);
			$cashgame1->setResults(array($worstResult));
			$cashgame2 = new Cashgame();
			$bestResult = new CashgameResult();
			$bestResult->setWinnings(2);
			$bestResult->setPlayer($this->player);
			$cashgame2->setResults(array($bestResult));
			$cashgames = array($cashgame1, $cashgame2);

			$worst = PlayerFactsModel::getWorstResult($cashgames, $this->player);
			$best = PlayerFactsModel::getBestResult($cashgames, $this->player);

			$this->assertIdentical(1, $worst);
			$this->assertIdentical(2, $best);
		}

		function test_GetMinutesPlayed_ReturnsPlayerMinutes(){
			$this->player = new Player();
			$cashgame1 = new Cashgame();
			$cashgame1->setDuration(1);
			$cashgame2 = new Cashgame();
			$cashgame2->setDuration(2);
			$cashgames = array($cashgame1, $cashgame2);

			$minutesPlayed = PlayerFactsModel::getMinutesPlayed($cashgames);

			$this->assertIdentical(3, $minutesPlayed);
		}

		function test_Facts_SetsFormattedWinnings(){
			$this->player = new Player();
			$this->player->setId(1);
			$cashgame1 = new Cashgame();
			$result1 = new CashgameResult();
			$result1->setPlayer($this->player);
			$result1->setWinnings(1);
			$cashgame1->setResults(array($result1));
			$cashgame2 = new Cashgame();
			$result2 = new CashgameResult();
			$result2->setPlayer($this->player);
			$result2->setWinnings(2);
			$cashgame2->setResults(array($result2));
			$cashgames = array($cashgame1, $cashgame2);

			$sut = new PlayerFactsModel($this->homegame, $cashgames, $this->player);

			$this->assertIdentical('+$3', $sut->winnings);
		}

		function test_Facts_SetsFormattedBestResult(){
			$this->player = new Player();
			$this->player->setId(1);
			$cashgame1 = new Cashgame();
			$result1 = new CashgameResult();
			$result1->setPlayer($this->player);
			$result1->setWinnings(1);
			$cashgame1->setResults(array($result1));
			$cashgame2 = new Cashgame();
			$result2 = new CashgameResult();
			$result2->setPlayer($this->player);
			$result2->setWinnings(2);
			$cashgame2->setResults(array($result2));
			$cashgames = array($cashgame1, $cashgame2);

			$sut = new PlayerFactsModel($this->homegame, $cashgames, $this->player);

			$this->assertIdentical('+$2', $sut->bestResult);
		}

		function test_Facts_SetsFormattedWorstResult(){
			$this->player = new Player();
			$this->player->setId(1);
			$cashgame1 = new Cashgame();
			$result1 = new CashgameResult();
			$result1->setPlayer($this->player);
			$result1->setWinnings(1);
			$cashgame1->setResults(array($result1));
			$cashgame2 = new Cashgame();
			$result2 = new CashgameResult();
			$result2->setPlayer($this->player);
			$result2->setWinnings(2);
			$cashgame2->setResults(array($result2));
			$cashgames = array($cashgame1, $cashgame2);

			$sut = new PlayerFactsModel($this->homegame, $cashgames, $this->player);

			$this->assertIdentical('+$1', $sut->worstResult);
		}

		function test_Facts_SetsFormattedTimePlayed(){
			$this->player = new Player();
			$this->player->setId(1);
			$cashgameResult1 = new CashgameResult();
			$cashgameResult1->setPlayer($this->player);
			$cashgame1 = new Cashgame();
			$cashgame1->setDuration(1);
			$cashgame1->setResults(array($cashgameResult1));
			$cashgameResult2 = new CashgameResult();
			$cashgameResult2->setPlayer($this->player);
			$cashgame2 = new Cashgame();
			$cashgame2->setDuration(2);
			$cashgame2->setResults(array($cashgameResult2));
			$cashgames = array($cashgame1, $cashgame2);

			$sut = new PlayerFactsModel($this->homegame, $cashgames, $this->player);

			$this->assertIdentical("3m", $sut->timePlayed);
		}

		function test_Facts_SetsNumberOfGamesPlayed(){
			$this->player = new Player();
			$this->player->setId(1);
			$cashgameResult1 = new CashgameResult();
			$cashgameResult1->setPlayer($this->player);
			$cashgame1 = new Cashgame();
			$cashgame1->setResults(array($cashgameResult1));
			$cashgameResult2 = new CashgameResult();
			$cashgameResult2->setPlayer($this->player);
			$cashgame2 = new Cashgame();
			$cashgame2->setResults(array($cashgameResult2));
			$cashgames = array($cashgame1, $cashgame2);

			$sut = new PlayerFactsModel($this->homegame, $cashgames, $this->player);

			$this->assertIdentical(2, $sut->gamesPlayed);
		}

	}

}