<?php
namespace tests\AppTests\User{

	use app\User\ChangePassword\ChangePasswordController;
	use app\User\SaltGenerator;
	use core\ClassNames;
	use core\Validation\ValidatorFake;
	use tests\TestHelper;
	use core\Validation\Validator;
	use tests\UnitTestCase;

	class ChangePasswordControllerTests extends UnitTestCase {

		/** @var ChangePasswordController */
		private $sut;
		private $userContext;
		private $userStorage;
		private $userValidatorFactory;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			$this->userValidatorFactory = TestHelper::getFake(ClassNames::$UserValidatorFactory);
			$encryption = TestHelper::getFake(ClassNames::$Encryption);
			$saltGenerator = new SaltGenerator();
			$request = TestHelper::getFake(ClassNames::$Request);
			$this->sut = new ChangePasswordController($this->userContext, $this->userStorage, $this->userValidatorFactory, $encryption, $saltGenerator, $request);
		}

		function test_ActionChange_NotAuthorized_ThrowsException(){
			$this->userContext->throwOn('requireUser');
			$this->expectException();

			$this->sut->action_change();
		}

		function test_ActionChangePost_NotAuthorized_ThrowsException(){
			$this->userContext->throwOn('requireUser');
			$this->expectException();

			$this->sut->action_change_post();
		}

		function test_ActionChangePost_WithEqualValidPasswords_CallsSetEncryptedPasswordAndSetSalt(){
			TestHelper::setupUser($this->userContext);
			$this->setupValidValidator();

			$this->userStorage->expectOnce("setEncryptedPassword");
			$this->userStorage->expectOnce("setSalt");

			$this->sut->action_change_post();
		}

		function test_ActionChangePost_WithEqualValidPasswords_RedirectsToConfirmation(){
			TestHelper::setupUser($this->userContext);
			$this->setupValidValidator();

			$urlModel = $this->sut->action_change_post();

			$this->assertIsA($urlModel, 'app\Urls\ChangePasswordConfirmationUrlModel');
		}

		function test_ActionChangePost_WithDifferentValidPasswords_DoesntUpdateUser(){
			TestHelper::setupUser($this->userContext);
			$this->setupInvalidValidator();

			$this->userStorage->expectNever("updateUser");

			$this->sut->action_change_post();
		}

		function setupValidValidator(){
			$validator = new ValidatorFake(true);
			$this->setupValidator($validator);
		}

		function setupInvalidValidator(){
			$validator = new ValidatorFake(false);
			$this->setupValidator($validator);
		}

		function setupValidator(Validator $validator){
			$this->userValidatorFactory->returns("getChangePasswordValidator", $validator);
		}

	}

}