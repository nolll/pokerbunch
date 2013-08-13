namespace tests\AppTests\User{

	use app\User\UserValidatorFactory;
	use tests\TestHelper;
	use tests\UnitTestCase;
	use core\ClassNames;
	use app\User\UserValidatorFactoryImpl;
	use Infrastructure\Data\Interfaces\UserStorage;
	use Domain\Classes\User;

	class UniqueUserValidatorTests extends UnitTestCase {

		private function getValidUser(){
			$user = new User();
			$user->setUserName('a');
			$user->setDisplayName('b');
			$user->setEmail('valid@email.com');
			return $user;
		}

		function test_IsValid_WithNonExistingUserNameAndNonExistingEmail_ReturnsTrue(){
			$user = $this->getValidUser();
			$userStorage = $this->getUserStorage();
			$userStorage->returns("getUserByName", null);
			$userStorage->returns("getUserByEmail", null);
			$validator = $this->getValidator($userStorage, $user);

			$this->assertTrue($validator->isValid());
		}

		function test_IsValid_WithExistingUserName_ReturnsFalse(){
			$user = $this->getValidUser();
			$userStorage = $this->getUserStorage();
			$existingUser = new User();
			$userStorage->returns("getUserByName", $existingUser);
			$userStorage->returns("getUserByEmail", null);
			$validator = $this->getValidator($userStorage, $user);

			$this->assertFalse($validator->isValid());
		}

		function test_IsValid_WithExistingEmail_ReturnsFalse(){
			$user = $this->getValidUser();
			$userStorage = $this->getUserStorage();
			$existingUser = new User();
			$userStorage->returns("getUserByName", null);
			$userStorage->returns("getUserByEmail", $existingUser);
			$validator = $this->getValidator($userStorage, $user);

			$this->assertFalse($validator->isValid());
		}

		function getValidator(UserStorage $userStorage, User $user){
			return $this->getValidatorFactory($userStorage)->getAddUserValidator($user);
		}

		/**
		 * @param UserStorage $userStorage
		 * @return UserValidatorFactory;
		 */
		function getValidatorFactory(UserStorage $userStorage){
			return new UserValidatorFactoryImpl($userStorage);
		}

		function getUserStorage(){
			return TestHelper::getFake(ClassNames::$UserStorage);
		}

	}

}