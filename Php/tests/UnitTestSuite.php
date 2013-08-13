namespace tests{

	class UnitTestSuite extends PokerBunchTestSuite {

		public function addTestCases(){
			$this->addAppTests();
			$this->addCoreTests();
			$this->addEntityTests();
			$this->addFactoryTests();
			$this->addRepositoryTests();
			$this->addStorageTests();
		}

		private function addAppTests(){
			$this->addTestCaseNamespace('tests\AppTests');
		}

		private function addCoreTests(){
			$this->addTestCaseNamespace('tests\CoreTests');
		}

		private function addEntityTests(){
			$this->addTestCaseNamespace('tests\EntityTests');
		}

		private function addFactoryTests(){
			$this->addTestCaseNamespace('tests\FactoryTests');
		}

		private function addRepositoryTests(){
			$this->addTestCaseNamespace('tests\RepositoryTests');
		}

		private function addStorageTests(){
			$this->addTestCaseNamespace('tests\StorageTests');
		}

	}

}