namespace tests\AppTests\User{

	use app\User\ForgotPassword\ForgotPasswordController;
	use app\User\SaltGenerator;
	use core\ClassNames;
	use core\Validation\ValidatorFake;
	use tests\TestHelper;
	use core\Validation\Validator;
	use Domain\Classes\User;
	use tests\UnitTestCase;

	class ForgotPasswordControllerTests extends UnitTestCase {

		/** @var ForgotPasswordController */
		private $sut;
		private $userContext;
		private $userStorage;
		private $passwordSender;
		private $passwordGenerator;
		private $userValidatorFactory;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			$this->passwordSender = TestHelper::getFake(ClassNames::$PasswordSender);
			$this->passwordGenerator = TestHelper::getFake(ClassNames::$PasswordGenerator);
			$this->userValidatorFactory = TestHelper::getFake(ClassNames::$UserValidatorFactory);
			$encryption = TestHelper::getFake(ClassNames::$Encryption);
			$saltGenerator = new SaltGenerator();
			$request = TestHelper::getFake(ClassNames::$Request);
			$this->sut = new ForgotPasswordController($this->userContext, $this->userStorage, $this->passwordSender, $this->passwordGenerator, $this->userValidatorFactory, $encryption, $saltGenerator, $request);
		}

		function test_ActionForgotPost_WithValidEmailAndExistingUser_GeneratesPassword(){
			$this->setupValidValidator();
			$this->userStorage->returns("getUserByEmail", new User());

			$this->passwordGenerator->expectOnce("createPassword");

			$this->sut->action_forgot_post();
		}

		function test_ActionForgotPost_WithValidEmailAndExistingUser_CallsSend(){
			$this->setupValidValidator();
			$this->userStorage->returns("getUserByEmail", new User());

			$this->passwordSender->expectOnce("send");

			$this->sut->action_forgot_post();
		}

		function test_ActionForgotPost_WithValidEmailAndExistingUser_CallsSetEncryptedPasswordAndSetSalt(){
			$this->setupValidValidator();
			$this->userStorage->returns("getUserByEmail", new User());

			$this->userStorage->expectOnce("setEncryptedPassword");
			$this->userStorage->expectOnce("setSalt");

			$this->sut->action_forgot_post();
		}

		function test_ActionForgotPost_WithValidEmailAndExistingUser_RedirectsToConfirmation(){
			$this->setupValidValidator();
			$this->userStorage->returns("getUserByEmail", new User());

			$urlModel = $this->sut->action_forgot_post();

			$this->assertIsA($urlModel, 'app\Urls\ForgotPasswordConfirmationUrlModel');
		}

		function test_ActionForgotPost_WithInvalidEmail_DoesntRedirectToConfirmation(){
			TestHelper::setupNullUser($this->userContext);
			$this->setupInvalidValidator();

			$urlModel = $this->sut->action_forgot_post();

			$this->assertNotA($urlModel, 'app\Urls\ForgotPasswordConfirmationUrlModel');
		}

		function test_ActionForgotPost_WithValidEmailAndNonExistingUser_DoesntGeneratePassword(){
			TestHelper::setupNullUser($this->userContext);
			$this->setupInvalidValidator();

			$this->passwordGenerator->expectNever("createPassword");

			$this->sut->action_forgot_post();
		}

		function test_ActionForgotPost_WithValidEmailAndNonExistingUser_DoesntCallSend(){
			TestHelper::setupNullUser($this->userContext);
			$this->setupInvalidValidator();

			$this->passwordSender->expectNever("send");

			$this->sut->action_forgot_post();
		}

		function test_ActionForgotPost_WithValidEmailAndNonExistingUser_DoesntCallSetEncryptedPasswordAndSetSalt(){
			TestHelper::setupNullUser($this->userContext);
			$this->setupInvalidValidator();

			$this->userStorage->expectNever("setEncryptedPassword");
			$this->userStorage->expectNever("setSalt");

			$this->sut->action_forgot_post();
		}

		function test_ActionForgotPost_WithValidEmailAndNonExistingUser_RedirectsToConfirmation(){
			$this->setupValidValidator();
			$this->userStorage->returns("getUserByEmail", null);

			$urlModel = $this->sut->action_forgot_post();

			$this->assertIsA($urlModel, 'app\Urls\ForgotPasswordConfirmationUrlModel');
		}

		function test_ActionForgotPost_WithInvalidEmail_DoesntGeneratePassword(){
			TestHelper::setupNullUser($this->userContext);
			$this->setupInvalidValidator();
			$this->passwordGenerator->expectNever("createPassword");

			$this->sut->action_forgot_post();
		}

		function test_ActionForgotPost_WithInvalidEmail_DoesntCallSend(){
			TestHelper::setupNullUser($this->userContext);
			$this->setupInvalidValidator();
			$this->passwordSender->expectNever("send");

			$this->sut->action_forgot_post();
		}

		function test_ActionForgotPost_WithInvalidEmail_DoesntCallSetEncryptedPasswordAndSetSalt(){
			TestHelper::setupNullUser($this->userContext);
			$this->setupInvalidValidator();

			$this->userStorage->expectNever("setEncryptedPassword");
			$this->userStorage->expectNever("setSalt");

			$this->sut->action_forgot_post();
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
			$this->userValidatorFactory->returns("getForgotPasswordValidator", $validator);
		}

	}

}