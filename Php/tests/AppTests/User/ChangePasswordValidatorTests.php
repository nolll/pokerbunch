namespace tests\AppTests\User{

	use app\User\UserValidatorFactory;
	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\ClassNames;
	use app\User\UserValidatorFactoryImpl;

	class ChangePasswordValidatorTests extends UnitTestCase {

		function test_IsValid_WithValidValues_ReturnsTrue(){
			$password = "password";
			$validator = $this->getValidator($password, $password);

			$this->assertTrue($validator->isValid());
		}

		function test_IsValid_WithInvalidPassword_ReturnsFalse(){
			$password = "";
			$validator = $this->getValidator($password, $password);

			$this->assertFalse($validator->isValid());
		}

		function test_IsValid_WithDifferentPasswords_ReturnsFalse(){
			$password = "password";
			$repeatPassword = "anotherpassword";
			$validator = $this->getValidator($password, $repeatPassword);

			$this->assertFalse($validator->isValid());
		}

		function getValidator($password, $repeatPassword){
			return $this->getValidatorFactory()->getChangePasswordValidator($password, $repeatPassword);
		}

		/**
		 * @return UserValidatorFactory;
		 */
		function getValidatorFactory(){
			$userStorage = $this->getUserStorage();
			return new UserValidatorFactoryImpl($userStorage);
		}

		function getUserStorage(){
			return TestHelper::getFake(ClassNames::$UserStorage);
		}

	}

}