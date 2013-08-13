<?php
namespace tests{

	class WebTestSuite extends PokerBunchTestSuite {

		public function addTestCases(){
			$this->addWebTests();
		}

		private function addWebTests(){
			$this->addTestCase('tests\WebTests\WebTests');
		}

	}

}