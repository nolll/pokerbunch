<?php
namespace tests\AppTests\Sharing{

	use Domain\Classes\User;
	use app\Sharing\Index\SharingIndexModel;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class SharingIndexModelTests extends UnitTestCase {

		function setUp(){
			parent::setUp();
		}

		function test_ActionIndex_ShareToTwitterUrlIsSet(){
			$user = new User();

			$sut = new SharingIndexModel($user, false);

			$this->assertIsA($sut->shareToTwitterSettingsUrl, 'app\Urls\TwitterSettingsUrlModel');
		}

		function test_ActionIndex_UserIsNotSharingToTwitter_IsSharingToTwitterIsFalse(){
			$user = new User();

			$sut = new SharingIndexModel($user, false);

			$this->assertFalse($sut->isSharingToTwitter);
		}

		function test_ActionIndex_UserIsSharingToTwitter_IsSharingToTwitterIsTrue(){
			$user = new User();

			$sut = new SharingIndexModel($user, true);

			$this->assertTrue($sut->isSharingToTwitter);
		}

	}

}