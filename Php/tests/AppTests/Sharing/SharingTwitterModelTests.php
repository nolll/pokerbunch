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
			user = new User();
			isSharing = false;
			credentials = null;
		}

		private function getSut(){
			return new SharingTwitterModel(user, isSharing, credentials);
		}

		private function getCredentials(){
			$c = new TwitterCredentials();
			$c.twitterName = 'nickName';
			return $c;
		}

		function test_ActionTwitter_IsSharingIsFalse(){
			$sut = getSut();

			assertFalse($sut.isSharing);
		}

		function test_ActionTwitter_WithSharing_IsSharingIsTrue(){
			isSharing = true;

			$sut = getSut();

			assertTrue($sut.isSharing);
		}

		function test_ActionTwitter_WithSharingAndCredentials_TwitterNameSet(){
			isSharing = true;
			credentials = getCredentials();

			$sut = getSut();

			assertIdentical("nickName", $sut.twitterName);
		}

		function test_ActionTwitter_PostUrlIsSet(){
			$sut = getSut();

			assertIsA($sut.postUrl, 'app\Urls\TwitterStartShareUrlModel');
		}

		function test_ActionTwitter_WithSharing_PostUrlIsSet(){
			isSharing = true;

			$sut = getSut();

			assertIsA($sut.postUrl, 'app\Urls\TwitterStopShareUrlModel');
		}

	}

}