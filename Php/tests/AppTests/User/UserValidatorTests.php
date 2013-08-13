<?php
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
			$user->setUserName('a');
			$user->setDisplayName('b');
			$user->setEmail('valid@email.com');
			return $user;
		}

		function test_IsValid_WithValidValues_ReturnsTrue(){
			$user = $this->getValidUser();
			$validator = $this->getValidator($user);

			$this->assertTrue($validator->isValid());
		}

		function test_IsValid_WithEmptyUserName_ReturnsFalse(){
			$user = $this->getValidUser();
			$user->setUserName('');
			$validator = $this->getValidator($user);

			$this->assertFalse($validator->isValid());
		}

		function test_IsValid_WithEmptyDisplayName_ReturnsFalse(){
			$user = $this->getValidUser();
			$user->setDisplayName('');
			$validator = $this->getValidator($user);

			$this->assertFalse($validator->isValid());
		}

		function test_IsValid_WithEmptyEmail_ReturnsFalse(){
			$user = $this->getValidUser();
			$user->setEmail('');
			$validator = $this->getValidator($user);

			$this->assertFalse($validator->isValid());
		}

		function test_ValidateEmail_WithInvalidEmail_ReturnsFalse(){
			$user = $this->getValidUser();
			$user->setEmail('invalidemail');
			$validator = $this->getValidator($user);

			$this->assertFalse($validator->isValid());
		}

		function getValidator(User $user){
			return $this->getValidatorFactory()->getEditUserValidator($user);
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