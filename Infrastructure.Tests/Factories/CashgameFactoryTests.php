namespace tests\FactoryTests{

	use DateTime;
	use entities\CashgameFactoryImpl;
	use entities\CashgameResult;
	use entities\GameStatus;
	use entities\Player;
	use tests\UnitTestCase;

	class CashgameFactoryTests extends UnitTestCase {

		private $earliestBuyinTime;
		private $earliestCashoutTime;
		private $latestBuyinTime;
		private $latestCashoutTime;

		private $results;
		private $location;
		private $status;
		private $id;

		function __construct(){
			$this->earliestBuyinTime = new DateTime("2010-01-01 01:00:00");
			$this->earliestCashoutTime = new DateTime("2010-01-01 03:00:00");
			$this->latestBuyinTime = new DateTime("2010-01-01 02:00:00");
			$this->latestCashoutTime = new DateTime("2010-01-01 04:00:00");

			$this->results = array();
			$result1 = new CashgameResult();
			$result1->setPlayer(new Player());
			$result1->setBuyinTime($this->earliestBuyinTime);
			$result1->setCashoutTime($this->earliestCashoutTime);
			$this->results[] = $result1;
			$result2 = new CashgameResult();
			$result2->setPlayer(new Player());
			$result2->setBuyinTime($this->latestBuyinTime);
			$result2->setCashoutTime($this->latestCashoutTime);
			$this->results[] = $result2;

			$this->location = 'a';
			$this->status = GameStatus::running;
			$this->id = 1;
		}

		function test_Get_CashgamePropertiesAreSet(){
			$sut = $this->getSut();
			$result = $sut->create($this->location, $this->status, $this->id, $this->results);

			$this->assertIdentical($this->location, $result->getLocation());
			$this->assertIdentical($this->status, $result->getStatus());
			$this->assertIdentical($this->id, $result->getId());
			$this->assertIdentical($this->earliestBuyinTime, $result->getStartTime());
			$this->assertIdentical($this->latestCashoutTime, $result->getEndTime());
			$this->assertIdentical(180, $result->getDuration());
		}

		private function getSut(){
			return new CashgameFactoryImpl();
		}

	}

}