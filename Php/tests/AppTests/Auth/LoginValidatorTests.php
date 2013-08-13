namespace tests\AppTests\Auth{

	use app\User\UserValidatorFactory;
	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\ClassNames;
	use app\User\UserValidatorFactoryImpl;
	use Domain\Classes\User;

	class LoginValidatorTests extends UnitTestCase {

		function test_IsValid_WithUser_ReturnsTrue(){
			$user = new User();
			$validator = getValidator($user);

			assertTrue($validator.isValid());
		}

		function test_IsValid_WithNullUser_ReturnsFalse(){
			$validator = getValidator(null);

			assertFalse($validator.isValid());
		}

		function getValidator(User $user = null){
			return getValidatorFactory().getLoginValidator($user);
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