namespace tests\AppTests\User{

	use app\User\UserValidatorFactory;
	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\ClassNames;
	use app\User\UserValidatorFactoryImpl;

	class ForgotPasswordValidatorTests extends UnitTestCase {

		function test_IsValid_WithValidEmail_ReturnsTrue(){
			$email = "test@example.com";
			$validator = getValidator($email);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithEmptyEmail_ReturnsFalse(){
			$email = "";
			$validator = getValidator($email);

			assertFalse($validator.isValid());
		}

		function test_IsValid_WithInvalidEmail_ReturnsFalse(){
			$email = "not-an-email";
			$validator = getValidator($email);

			assertFalse($validator.isValid());
		}

		function getValidator($email){
			return getValidatorFactory().getForgotPasswordValidator($email);
		}

		/**
		 * @return UserValidatorFactory;
		 */
		function getValidatorFactory(){
			$userStorage = getUserStorage();
			return new UserValidatorFactoryImpl($userStorage);
		}

		function getUserStorage(){
			return TestHelper::getFake(ClassNames::$UserStorage);
		}

	}

}