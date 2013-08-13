namespace tests\AppTests\Auth{
	use tests\UnitTestCase;
	use app\Auth\AuthLoginModel;

	class AuthLoginModelTests extends UnitTestCase {

		function test_ReturnUrl_NoReturnUrl_IsSetToRoot(){
			$sut = new AuthLoginModel(null, 'anyname');

			$this->assertIdentical("/", $sut->returnUrl);
		}

		function test_ReturnUrl_WithReturnUrl_IsSet(){
			$sut = new AuthLoginModel('return-url', 'anyname');

			$this->assertIdentical("return-url", $sut->returnUrl);
		}

		function test_AddUserUrl_IsSet(){
			$sut = new AuthLoginModel('anyurl', 'anyname');

			$this->assertIsA($sut->addUserUrl, 'app\Urls\UserAddUrlModel');
		}

		function test_ForgotPasswordUrl_IsSet(){
			$sut = new AuthLoginModel('anyurl', 'anyname');

			$this->assertIsA($sut->forgotPasswordUrl, 'app\Urls\ForgotPasswordUrlModel');
		}

		function test_LoginName_IsSet(){
			$sut = new AuthLoginModel('anyurl', 'login-name');

			$this->assertIdentical($sut->loginName, 'login-name');
		}

	}

}