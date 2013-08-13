namespace tests{

	class IntegrationTestSuite extends PokerBunchTestSuite {

		public function addTestCases(){
			addIntegrationTests();
		}

		private function addIntegrationTests(){
			addTestCase('tests\IntegrationTests\GravatarTests');

			addTestCase('tests\IntegrationTests\CashgameStorageTests');
			addTestCase('tests\IntegrationTests\HomegameStorageTests');
			addTestCase('tests\IntegrationTests\UserStorageTests');
		}

	}

}