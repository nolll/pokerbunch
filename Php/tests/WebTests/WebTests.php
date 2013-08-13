namespace tests\WebTests{

	use tests\TestHelper;
	importlib('/SimpleTest/web_tester.php');

	class WebTests extends \WebTestCase {

		private $baseUrl = 'http://pokerbunch.lan/';

		function test_StartPage_ReturnsAnyResponse(){
			$result = $this->get($this->baseUrl);
			$this->assertTrue($result);
		}

	}

}