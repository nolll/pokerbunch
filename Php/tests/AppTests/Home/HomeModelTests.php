namespace tests\AppTests\Home{

	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\Home\HomeModel;
	use tests\TestHelper;

	class HomeModelTests extends UnitTestCase {

		function test_IsLoggedIn_WithUser_IsTrue(){
			$user = new User();

			$sut = new HomeModel($user);

			assertTrue($sut.isLoggedIn);
		}

		function test_IsLoggedIn_WithoutUser_IsFalse(){
			$sut = new HomeModel(null);

			assertFalse($sut.isLoggedIn);
		}

		function test_LoginUrl_WithUser_IsSet(){
			$user = new User();

			$sut = new HomeModel($user);

			assertIsA($sut.loginUrl, 'app\Urls\AuthLoginUrlModel');
		}

		function test_AddHomegameUrl_WithUser_IsSet(){
			$user = new User();

			$sut = new HomeModel($user);

			assertIsA($sut.addHomegameUrl, 'app\Urls\HomegameAddUrlModel');
		}

		function test_RegisterUrl_WithUrl_IsSet(){
			$user = new User();

			$sut = new HomeModel($user);

			assertIsA($sut.registerUrl, 'app\Urls\UserAddUrlModel');
		}

		function test_AdminNav_WithUser_IsSet(){
			$user = new User();

			$sut = new HomeModel($user);

			assertIsA($sut.adminNav, 'app\Admin\AdminNavModel');
		}

	}

}