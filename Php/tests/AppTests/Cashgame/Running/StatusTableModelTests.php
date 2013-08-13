namespace tests\AppTests\Cashgame\Running{

	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Player;
	use tests\Fakes\TimerFake;
	use DateTime;
	use entities\Homegame;
	use tests\UnitTestCase;
	use app\Cashgame\Running\StatusTableModel;
	use tests\TestHelper;

	class StatusTableModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;
		/** @var Cashgame */
		private $cashgame;

		function setUp(){
			homegame = new Homegame();
			cashgame = new Cashgame();
		}

		function test_StatusModels_CashgameWithOnePlayer_FirstItemIsCorrectType(){
			cashgame.setResults(array(new CashgameResult()));

			$sut = getSut();

			assertEqual(1, count($sut.statusModels));
			assertIsA($sut.statusModels[0], 'app\Cashgame\Running\StatusItemModel');
		}

		function test_StatusModels_CashgameWithTwoPlayers_HasTwoItems(){
			cashgame.setResults(array(new CashgameResult(), new CashgameResult()));

			$sut = getSut();

			assertEqual(2, count($sut.statusModels));
		}

		function test_TotalBuyin_CashgameWithTwoPlayers_IsSumOfBuyins(){
			cashgame.setTurnOver(1);

			$sut = getSut();

			assertEqual("$1", $sut.totalBuyin);
		}

		function test_TotalStacks_CashgameWithTwoPlayers_IsSumOfCurrentStacks(){
			cashgame.setTotalStacks(1);

			$sut = getSut();

			assertEqual("$1", $sut.totalStacks);
		}

		function test_StatusModels_CashgameWithTwoPlayers_IsSortedByWinningsDescending(){
			cashgame.setStartTime(new DateTime());
			$player1 = new Player();
			$player1.setDisplayName('a');
			$result1 = new CashgameResult();
			$result1.setPlayer($player1);
			$result1.setWinnings(1);
			$player2 = new Player();
			$player2.setDisplayName('b');
			$result2 = new CashgameResult();
			$result2.setPlayer($player2);
			$result2.setWinnings(2);
			cashgame.setResults(array($result1, $result2));

			$sut = getSut();

			$results = $sut.statusModels;
			assertIdentical('b', $results[0].name);
			assertIdentical('a', $results[1].name);
		}

		function getSut(){
			return new StatusTableModel(homegame, cashgame, false, new TimerFake());
		}

	}

}