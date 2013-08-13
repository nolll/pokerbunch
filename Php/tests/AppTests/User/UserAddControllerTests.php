<?php
namespace tests\AppTests\User{

	use app\User\Add\UserAddController;
	use app\User\SaltGenerator;
	use core\Validation\ValidatorFake;
	use Domain\Classes\User;
	use core\ClassNames;
	use tests\TestHelper;
	use core\Validation\Validator;
	use tests\UnitTestCase;

	class UserAddControllerTests extends UnitTestCase {

		/** @var UserAddController */
		private $sut;
		private $userContext;
		private $userStorage;
		private $registrationConfirmationSender;
		private $passwordGenerator;
		private $userValidatorFactory;
		private $userFactory;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			$this->registrationConfirmationSender = TestHelper::getFake(ClassNames::$RegistrationConfirmationSender);
			$this->passwordGenerator = TestHelper::getFake(ClassNames::$PasswordGenerator);
			$this->userValidatorFactory = TestHelper::getFake(ClassNames::$UserValidatorFactory);
			$encryption = TestHelper::getFake(ClassNames::$Encryption);
			$this->userFactory = TestHelper::getFake(ClassNames::$UserFactory);
			$saltGenerator = new SaltGenerator();
			$request = TestHelper::getFake(ClassNames::$Request);
			$this->sut = new UserAddController($this->userContext, $this->userStorage, $this->registrationConfirmationSender, $this->passwordGenerator, $this->userValidatorFactory, $encryption, $this->userFactory, $saltGenerator, $request);
		}

		function test_ActionAddPost_WithValidParameters_CallsAddUser(){
			$this->setupValidValidator();
			$this->userFactory->returns("createUser", new User());
			$this->userStorage->expectOnce("addUser");

			$this->sut->action_add_post();
		}

		function test_ActionAddPost_WithValidParameters_GeneratesPassword(){
			$this->setupValidValidator();
			$this->userFactory->returns("createUser", new User());
			$this->passwordGenerator->expectOnce("createPassword");

			$this->sut->action_add_post();
		}

		function test_ActionAddPost_WithValidParameters_SendsEmail(){
			$this->setupValidValidator();
			$this->userFactory->returns("createUser", new User());

			$this->registrationConfirmationSender->expectOnce("send", array("*", "*"));

			$this->sut->action_add_post();
		}

		function test_ActionAddPost_WithValidParameters_RedirectsToConfirmation(){
			$this->setupValidValidator();
			$this->userFactory->returns("createUser", new User());

			$urlModel = $this->sut->action_add_post();

			$this->assertIsA($urlModel, 'app\Urls\UserAddConfirmationUrlModel');
		}

		function test_ActionAddPost_WithInvalidParameters_DoesntCallAddUser(){
			TestHelper::setupNullUser($this->userContext);
			$this->setupInvalidValidator();
			$this->userFactory->returns("createUser", new User());
			$this->userStorage->expectNever("addUser");

			$this->sut->action_add_post();
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
			$this->userValidatorFactory->returns("getAddUserValidator", $validator);
		}

	}

}