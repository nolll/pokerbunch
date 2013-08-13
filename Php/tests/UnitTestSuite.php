namespace tests{

	class UnitTestSuite extends PokerBunchTestSuite {

		public function addTestCases(){
			addAppTests();
			addCoreTests();
			addEntityTests();
			addFactoryTests();
			addRepositoryTests();
			addStorageTests();
		}

		private function addAppTests(){
			addTestCaseNamespace('tests\AppTests');
		}

		private function addCoreTests(){
			addTestCaseNamespace('tests\CoreTests');
		}

		private function addEntityTests(){
			addTestCaseNamespace('tests\EntityTests');
		}

		private function addFactoryTests(){
			addTestCaseNamespace('tests\FactoryTests');
		}

		private function addRepositoryTests(){
			addTestCaseNamespace('tests\RepositoryTests');
		}

		private function addStorageTests(){
			addTestCaseNamespace('tests\StorageTests');
		}

	}

}