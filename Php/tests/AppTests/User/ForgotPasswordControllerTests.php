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
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			passwordSender = TestHelper::getFake(ClassNames::$PasswordSender);
			passwordGenerator = TestHelper::getFake(ClassNames::$PasswordGenerator);
			userValidatorFactory = TestHelper::getFake(ClassNames::$UserValidatorFactory);
			$encryption = TestHelper::getFake(ClassNames::$Encryption);
			$saltGenerator = new SaltGenerator();
			$request = TestHelper::getFake(ClassNames::$Request);
			sut = new ForgotPasswordController(userContext, userStorage, passwordSender, passwordGenerator, userValidatorFactory, $encryption, $saltGenerator, $request);
		}

		function test_ActionForgotPost_WithValidEmailAndExistingUser_GeneratesPassword(){
			setupValidValidator();
			userStorage.returns("getUserByEmail", new User());

			passwordGenerator.expectOnce("createPassword");

			sut.action_forgot_post();
		}

		function test_ActionForgotPost_WithValidEmailAndExistingUser_CallsSend(){
			setupValidValidator();
			userStorage.returns("getUserByEmail", new User());

			passwordSender.expectOnce("send");

			sut.action_forgot_post();
		}

		function test_ActionForgotPost_WithValidEmailAndExistingUser_CallsSetEncryptedPasswordAndSetSalt(){
			setupValidValidator();
			userStorage.returns("getUserByEmail", new User());

			userStorage.expectOnce("setEncryptedPassword");
			userStorage.expectOnce("setSalt");

			sut.action_forgot_post();
		}

		function test_ActionForgotPost_WithValidEmailAndExistingUser_RedirectsToConfirmation(){
			setupValidValidator();
			userStorage.returns("getUserByEmail", new User());

			$urlModel = sut.action_forgot_post();

			assertIsA($urlModel, 'app\Urls\ForgotPasswordConfirmationUrlModel');
		}

		function test_ActionForgotPost_WithInvalidEmail_DoesntRedirectToConfirmation(){
			TestHelper::setupNullUser(userContext);
			setupInvalidValidator();

			$urlModel = sut.action_forgot_post();

			assertNotA($urlModel, 'app\Urls\ForgotPasswordConfirmationUrlModel');
		}

		function test_ActionForgotPost_WithValidEmailAndNonExistingUser_DoesntGeneratePassword(){
			TestHelper::setupNullUser(userContext);
			setupInvalidValidator();

			passwordGenerator.expectNever("createPassword");

			sut.action_forgot_post();
		}

		function test_ActionForgotPost_WithValidEmailAndNonExistingUser_DoesntCallSend(){
			TestHelper::setupNullUser(userContext);
			setupInvalidValidator();

			passwordSender.expectNever("send");

			sut.action_forgot_post();
		}

		function test_ActionForgotPost_WithValidEmailAndNonExistingUser_DoesntCallSetEncryptedPasswordAndSetSalt(){
			TestHelper::setupNullUser(userContext);
			setupInvalidValidator();

			userStorage.expectNever("setEncryptedPassword");
			userStorage.expectNever("setSalt");

			sut.action_forgot_post();
		}

		function test_ActionForgotPost_WithValidEmailAndNonExistingUser_RedirectsToConfirmation(){
			setupValidValidator();
			userStorage.returns("getUserByEmail", null);

			$urlModel = sut.action_forgot_post();

			assertIsA($urlModel, 'app\Urls\ForgotPasswordConfirmationUrlModel');
		}

		function test_ActionForgotPost_WithInvalidEmail_DoesntGeneratePassword(){
			TestHelper::setupNullUser(userContext);
			setupInvalidValidator();
			passwordGenerator.expectNever("createPassword");

			sut.action_forgot_post();
		}

		function test_ActionForgotPost_WithInvalidEmail_DoesntCallSend(){
			TestHelper::setupNullUser(userContext);
			setupInvalidValidator();
			passwordSender.expectNever("send");

			sut.action_forgot_post();
		}

		function test_ActionForgotPost_WithInvalidEmail_DoesntCallSetEncryptedPasswordAndSetSalt(){
			TestHelper::setupNullUser(userContext);
			setupInvalidValidator();

			userStorage.expectNever("setEncryptedPassword");
			userStorage.expectNever("setSalt");

			sut.action_forgot_post();
		}

		function setupValidValidator(){
			$validator = new ValidatorFake(true);
			setupValidator($validator);
		}

		function setupInvalidValidator(){
			$validator = new ValidatorFake(false);
			setupValidator($validator);
		}

		function setupValidator(Validator $validator){
			userValidatorFactory.returns("getForgotPasswordValidator", $validator);
		}

	}

}