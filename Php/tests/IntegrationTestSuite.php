namespace tests{

	class IntegrationTestSuite extends PokerBunchTestSuite {

		public function addTestCases(){
			$this->addIntegrationTests();
		}

		private function addIntegrationTests(){
			$this->addTestCase('tests\IntegrationTests\GravatarTests');

			$this->addTestCase('tests\IntegrationTests\CashgameStorageTests');
			$this->addTestCase('tests\IntegrationTests\HomegameStorageTests');
			$this->addTestCase('tests\IntegrationTests\UserStorageTests');
		}

	}

}