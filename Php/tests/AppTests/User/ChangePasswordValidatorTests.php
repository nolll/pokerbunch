namespace tests\AppTests\User{

	use app\User\UserValidatorFactory;
	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\ClassNames;
	use app\User\UserValidatorFactoryImpl;

	class ChangePasswordValidatorTests extends UnitTestCase {

		function test_IsValid_WithValidValues_ReturnsTrue(){
			$password = "password";
			$validator = getValidator($password, $password);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithInvalidPassword_ReturnsFalse(){
			$password = "";
			$validator = getValidator($password, $password);

			assertFalse($validator.isValid());
		}

		function test_IsValid_WithDifferentPasswords_ReturnsFalse(){
			$password = "password";
			$repeatPassword = "anotherpassword";
			$validator = getValidator($password, $repeatPassword);

			assertFalse($validator.isValid());
		}

		function getValidator($password, $repeatPassword){
			return getValidatorFactory().getChangePasswordValidator($password, $repeatPassword);
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