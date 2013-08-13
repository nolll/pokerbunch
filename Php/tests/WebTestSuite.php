namespace tests{

	class WebTestSuite extends PokerBunchTestSuite {

		public function addTestCases(){
			addWebTests();
		}

		private function addWebTests(){
			addTestCase('tests\WebTests\WebTests');
		}

	}

}