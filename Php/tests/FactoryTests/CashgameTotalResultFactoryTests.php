namespace tests\FactoryTests{
	use entities\CashgameResult;
	use entities\CashgameTotalResultFactoryImpl;
	use entities\Player;
	use tests\UnitTestCase;
	use tests\TestHelper;

	class CashgameTotalResultFactoryTests extends UnitTestCase {

		public function test_GetWinnings_WithTwoResults_ReturnsSumOfWinnings(){
			$sut = $this->getSutWithTwoResults();

			$actual = $sut->getWinnings();

			$this->assertEqual(2, $actual);
		}

		private function getSutWithTwoResults(){
			$player = new Player();
			$factory = new CashgameTotalResultFactoryImpl();
			$result = $this->getResult();
			$results = array($result, $result);
			return $factory->create($player, $results);
		}

		private function getResult(){
			$player = new Player();
			$result = new CashgameResult();
			$result->setPlayer($player);
			$result->setWinnings(1);
			return $result;
		}

	}

}