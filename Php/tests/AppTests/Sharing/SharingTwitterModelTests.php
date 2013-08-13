namespace tests\AppTests\Sharing{

	use Domain\Classes\User;
	use app\Sharing\Twitter\SharingTwitterModel;
	use tests\TestHelper;
	use integration\Sharing\TwitterCredentials;
	use tests\UnitTestCase;

	class SharingTwitterModelTests extends UnitTestCase {

		private $user;
		private $isSharing;
		private $credentials;

		function setUp(){
			parent::setUp();
			$this->user = new User();
			$this->isSharing = false;
			$this->credentials = null;
		}

		private function getSut(){
			return new SharingTwitterModel($this->user, $this->isSharing, $this->credentials);
		}

		private function getCredentials(){
			$c = new TwitterCredentials();
			$c->twitterName = 'nickName';
			return $c;
		}

		function test_ActionTwitter_IsSharingIsFalse(){
			$sut = $this->getSut();

			$this->assertFalse($sut->isSharing);
		}

		function test_ActionTwitter_WithSharing_IsSharingIsTrue(){
			$this->isSharing = true;

			$sut = $this->getSut();

			$this->assertTrue($sut->isSharing);
		}

		function test_ActionTwitter_WithSharingAndCredentials_TwitterNameSet(){
			$this->isSharing = true;
			$this->credentials = $this->getCredentials();

			$sut = $this->getSut();

			$this->assertIdentical("nickName", $sut->twitterName);
		}

		function test_ActionTwitter_PostUrlIsSet(){
			$sut = $this->getSut();

			$this->assertIsA($sut->postUrl, 'app\Urls\TwitterStartShareUrlModel');
		}

		function test_ActionTwitter_WithSharing_PostUrlIsSet(){
			$this->isSharing = true;

			$sut = $this->getSut();

			$this->assertIsA($sut->postUrl, 'app\Urls\TwitterStopShareUrlModel');
		}

	}

}