<?php
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
			$this->homegame = new Homegame();
			$this->cashgame = new Cashgame();
		}

		function test_StatusModels_CashgameWithOnePlayer_FirstItemIsCorrectType(){
			$this->cashgame->setResults(array(new CashgameResult()));

			$sut = $this->getSut();

			$this->assertEqual(1, count($sut->statusModels));
			$this->assertIsA($sut->statusModels[0], 'app\Cashgame\Running\StatusItemModel');
		}

		function test_StatusModels_CashgameWithTwoPlayers_HasTwoItems(){
			$this->cashgame->setResults(array(new CashgameResult(), new CashgameResult()));

			$sut = $this->getSut();

			$this->assertEqual(2, count($sut->statusModels));
		}

		function test_TotalBuyin_CashgameWithTwoPlayers_IsSumOfBuyins(){
			$this->cashgame->setTurnOver(1);

			$sut = $this->getSut();

			$this->assertEqual("$1", $sut->totalBuyin);
		}

		function test_TotalStacks_CashgameWithTwoPlayers_IsSumOfCurrentStacks(){
			$this->cashgame->setTotalStacks(1);

			$sut = $this->getSut();

			$this->assertEqual("$1", $sut->totalStacks);
		}

		function test_StatusModels_CashgameWithTwoPlayers_IsSortedByWinningsDescending(){
			$this->cashgame->setStartTime(new DateTime());
			$player1 = new Player();
			$player1->setDisplayName('a');
			$result1 = new CashgameResult();
			$result1->setPlayer($player1);
			$result1->setWinnings(1);
			$player2 = new Player();
			$player2->setDisplayName('b');
			$result2 = new CashgameResult();
			$result2->setPlayer($player2);
			$result2->setWinnings(2);
			$this->cashgame->setResults(array($result1, $result2));

			$sut = $this->getSut();

			$results = $sut->statusModels;
			$this->assertIdentical('b', $results[0]->name);
			$this->assertIdentical('a', $results[1]->name);
		}

		function getSut(){
			return new StatusTableModel($this->homegame, $this->cashgame, false, new TimerFake());
		}

	}

}