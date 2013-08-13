<?php
namespace tests\AppTests\Cashgame\Details{

	use DateTime;
	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Player;
	use entities\Homegame;
	use tests\UnitTestCase;
	use app\Cashgame\Details\ResultTable\ResultTableModel;
	use tests\TestHelper;

	class ResultTableModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;
		/** @var Cashgame */
		private $cashgame;

		function setUp(){
			$this->homegame = new Homegame();
			$this->cashgame = new Cashgame();
		}

		function test_ResultModels_CashgameWithOnePlayer_FirstItemIsCorrectType(){
			$this->cashgame = new Cashgame();
			$this->cashgame->setResults(array(new CashgameResult()));

			$sut = $this->getModel();

			$this->assertEqual(1, count($sut->resultModels));
			$this->assertIsA($sut->resultModels[0], 'app\Cashgame\Details\ResultTableItem\ResultTableItemModel');
		}

		function test_ResultModels_CashgameWithTwoPlayers_HasTwoItems(){
			$this->cashgame = new Cashgame();
			$this->cashgame->setResults(array(new CashgameResult(), new CashgameResult()));

			$sut = $this->getModel();

			$this->assertEqual(2, count($sut->resultModels));
		}

		function test_ResultModels_CashgameWithTwoPlayers_IsSortedByWinningsDescending(){
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

			$sut = $this->getModel();

			$results = $sut->resultModels;
			$this->assertIdentical('b', $result1Name = $results[0]->name);
			$this->assertIdentical('a', $result1Name = $results[1]->name);
		}

		function getModel(){
			return new ResultTableModel($this->homegame, $this->cashgame);
		}

	}

}