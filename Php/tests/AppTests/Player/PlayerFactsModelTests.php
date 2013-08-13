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
			homegame = new Homegame();
		}

		function test_FilterCashames_ReturnsOnlyGamesWhereThePlayerParticipated(){
			player = new Player();
			player.setId(1);
			$cashgameResult = new CashgameResult();
			$cashgameResult.setPlayer(player);
			$cashgame1 = new Cashgame();
			$cashgame1.setResults(array($cashgameResult));
			$cashgame2 = new Cashgame();
			$cashgames = array($cashgame1, $cashgame2);

			$filteredCashgames = PlayerFactsModel::filterCashgames($cashgames, player);

			assertIdentical(1, count($filteredCashgames));
		}

		function test_GetWinnings_ReturnsPlayerWinnings(){
			player = new Player();
			player.setId(1);

			$cashgame1 = new Cashgame();
			$result1 = new CashgameResult();
			$result1.setPlayer(player);
			$result1.setWinnings(1);
			$cashgame1.setResults(array($result1));

			$cashgame2 = new Cashgame();
			$result2 = new CashgameResult();
			$result2.setPlayer(player);
			$result2.setWinnings(2);
			$cashgame2.setResults(array($result2));

			$cashgames = array($cashgame1, $cashgame2);

			$winnings = PlayerFactsModel::getWinnings($cashgames, player);

			assertIdentical(3, $winnings);
		}

		function test_GetBestResultAndGetWorstResult_ReturnsCorrectResults(){
			player = new Player();
			player.setId(1);
			$cashgame1 = new Cashgame();
			$worstResult = new CashgameResult();
			$worstResult.setWinnings(1);
			$worstResult.setPlayer(player);
			$cashgame1.setResults(array($worstResult));
			$cashgame2 = new Cashgame();
			$bestResult = new CashgameResult();
			$bestResult.setWinnings(2);
			$bestResult.setPlayer(player);
			$cashgame2.setResults(array($bestResult));
			$cashgames = array($cashgame1, $cashgame2);

			$worst = PlayerFactsModel::getWorstResult($cashgames, player);
			$best = PlayerFactsModel::getBestResult($cashgames, player);

			assertIdentical(1, $worst);
			assertIdentical(2, $best);
		}

		function test_GetMinutesPlayed_ReturnsPlayerMinutes(){
			player = new Player();
			$cashgame1 = new Cashgame();
			$cashgame1.setDuration(1);
			$cashgame2 = new Cashgame();
			$cashgame2.setDuration(2);
			$cashgames = array($cashgame1, $cashgame2);

			$minutesPlayed = PlayerFactsModel::getMinutesPlayed($cashgames);

			assertIdentical(3, $minutesPlayed);
		}

		function test_Facts_SetsFormattedWinnings(){
			player = new Player();
			player.setId(1);
			$cashgame1 = new Cashgame();
			$result1 = new CashgameResult();
			$result1.setPlayer(player);
			$result1.setWinnings(1);
			$cashgame1.setResults(array($result1));
			$cashgame2 = new Cashgame();
			$result2 = new CashgameResult();
			$result2.setPlayer(player);
			$result2.setWinnings(2);
			$cashgame2.setResults(array($result2));
			$cashgames = array($cashgame1, $cashgame2);

			$sut = new PlayerFactsModel(homegame, $cashgames, player);

			assertIdentical('+$3', $sut.winnings);
		}

		function test_Facts_SetsFormattedBestResult(){
			player = new Player();
			player.setId(1);
			$cashgame1 = new Cashgame();
			$result1 = new CashgameResult();
			$result1.setPlayer(player);
			$result1.setWinnings(1);
			$cashgame1.setResults(array($result1));
			$cashgame2 = new Cashgame();
			$result2 = new CashgameResult();
			$result2.setPlayer(player);
			$result2.setWinnings(2);
			$cashgame2.setResults(array($result2));
			$cashgames = array($cashgame1, $cashgame2);

			$sut = new PlayerFactsModel(homegame, $cashgames, player);

			assertIdentical('+$2', $sut.bestResult);
		}

		function test_Facts_SetsFormattedWorstResult(){
			player = new Player();
			player.setId(1);
			$cashgame1 = new Cashgame();
			$result1 = new CashgameResult();
			$result1.setPlayer(player);
			$result1.setWinnings(1);
			$cashgame1.setResults(array($result1));
			$cashgame2 = new Cashgame();
			$result2 = new CashgameResult();
			$result2.setPlayer(player);
			$result2.setWinnings(2);
			$cashgame2.setResults(array($result2));
			$cashgames = array($cashgame1, $cashgame2);

			$sut = new PlayerFactsModel(homegame, $cashgames, player);

			assertIdentical('+$1', $sut.worstResult);
		}

		function test_Facts_SetsFormattedTimePlayed(){
			player = new Player();
			player.setId(1);
			$cashgameResult1 = new CashgameResult();
			$cashgameResult1.setPlayer(player);
			$cashgame1 = new Cashgame();
			$cashgame1.setDuration(1);
			$cashgame1.setResults(array($cashgameResult1));
			$cashgameResult2 = new CashgameResult();
			$cashgameResult2.setPlayer(player);
			$cashgame2 = new Cashgame();
			$cashgame2.setDuration(2);
			$cashgame2.setResults(array($cashgameResult2));
			$cashgames = array($cashgame1, $cashgame2);

			$sut = new PlayerFactsModel(homegame, $cashgames, player);

			assertIdentical("3m", $sut.timePlayed);
		}

		function test_Facts_SetsNumberOfGamesPlayed(){
			player = new Player();
			player.setId(1);
			$cashgameResult1 = new CashgameResult();
			$cashgameResult1.setPlayer(player);
			$cashgame1 = new Cashgame();
			$cashgame1.setResults(array($cashgameResult1));
			$cashgameResult2 = new CashgameResult();
			$cashgameResult2.setPlayer(player);
			$cashgame2 = new Cashgame();
			$cashgame2.setResults(array($cashgameResult2));
			$cashgames = array($cashgame1, $cashgame2);

			$sut = new PlayerFactsModel(homegame, $cashgames, player);

			assertIdentical(2, $sut.gamesPlayed);
		}

	}

}