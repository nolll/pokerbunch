namespace tests\AppTests\User{

	use app\User\UserValidatorFactory;
	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\ClassNames;
	use app\User\UserValidatorFactoryImpl;
	use Domain\Classes\User;

	class UserValidatorTests extends UnitTestCase {

		private function getValidUser(){
			$user = new User();
			$user.setUserName('a');
			$user.setDisplayName('b');
			$user.setEmail('valid@email.com');
			return $user;
		}

		function test_IsValid_WithValidValues_ReturnsTrue(){
			$user = getValidUser();
			$validator = getValidator($user);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithEmptyUserName_ReturnsFalse(){
			$user = getValidUser();
			$user.setUserName('');
			$validator = getValidator($user);

			assertFalse($validator.isValid());
		}

		function test_IsValid_WithEmptyDisplayName_ReturnsFalse(){
			$user = getValidUser();
			$user.setDisplayName('');
			$validator = getValidator($user);

			assertFalse($validator.isValid());
		}

		function test_IsValid_WithEmptyEmail_ReturnsFalse(){
			$user = getValidUser();
			$user.setEmail('');
			$validator = getValidator($user);

			assertFalse($validator.isValid());
		}

		function test_ValidateEmail_WithInvalidEmail_ReturnsFalse(){
			$user = getValidUser();
			$user.setEmail('invalidemail');
			$validator = getValidator($user);

			assertFalse($validator.isValid());
		}

		function getValidator(User $user){
			return getValidatorFactory().getEditUserValidator($user);
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