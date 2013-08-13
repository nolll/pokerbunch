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
			$validator = $this->getValidator($user);

			$this->assertTrue($validator->isValid());
		}

		function test_IsValid_WithNullUser_ReturnsFalse(){
			$validator = $this->getValidator(null);

			$this->assertFalse($validator->isValid());
		}

		function getValidator(User $user = null){
			return $this->getValidatorFactory()->getLoginValidator($user);
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