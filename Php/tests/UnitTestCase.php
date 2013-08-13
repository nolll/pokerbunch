namespace tests{

	use core\ClassNames;
	use SimpleMock;

	importlib('/SimpleTest/unit_tester.php');

	class UnitTestCase extends \UnitTestCase {

		/** @var SimpleMock */
		protected $homegameRepositoryMock;
		/** @var SimpleMock */
		protected $playerRepositoryMock;
		/** @var SimpleMock */
		protected $cashgameRepositoryMock;

		protected function setupMocks(){
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->playerRepositoryMock = $this->getFakePlayerRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
		}

		protected function getFakeHomegameRepository(){
			return TestHelper::getFake(ClassNames::$HomegameRepository);
		}

		protected function getFakeCashgameRepository(){
			return TestHelper::getFake(ClassNames::$CashgameRepository);
		}

		protected function getFakePlayerRepository(){
			return TestHelper::getFake(ClassNames::$PlayerRepository);
		}

	}

}