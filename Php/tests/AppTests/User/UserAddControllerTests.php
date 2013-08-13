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
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			registrationConfirmationSender = TestHelper::getFake(ClassNames::$RegistrationConfirmationSender);
			passwordGenerator = TestHelper::getFake(ClassNames::$PasswordGenerator);
			userValidatorFactory = TestHelper::getFake(ClassNames::$UserValidatorFactory);
			$encryption = TestHelper::getFake(ClassNames::$Encryption);
			userFactory = TestHelper::getFake(ClassNames::$UserFactory);
			$saltGenerator = new SaltGenerator();
			$request = TestHelper::getFake(ClassNames::$Request);
			sut = new UserAddController(userContext, userStorage, registrationConfirmationSender, passwordGenerator, userValidatorFactory, $encryption, userFactory, $saltGenerator, $request);
		}

		function test_ActionAddPost_WithValidParameters_CallsAddUser(){
			setupValidValidator();
			userFactory.returns("createUser", new User());
			userStorage.expectOnce("addUser");

			sut.action_add_post();
		}

		function test_ActionAddPost_WithValidParameters_GeneratesPassword(){
			setupValidValidator();
			userFactory.returns("createUser", new User());
			passwordGenerator.expectOnce("createPassword");

			sut.action_add_post();
		}

		function test_ActionAddPost_WithValidParameters_SendsEmail(){
			setupValidValidator();
			userFactory.returns("createUser", new User());

			registrationConfirmationSender.expectOnce("send", array("*", "*"));

			sut.action_add_post();
		}

		function test_ActionAddPost_WithValidParameters_RedirectsToConfirmation(){
			setupValidValidator();
			userFactory.returns("createUser", new User());

			$urlModel = sut.action_add_post();

			assertIsA($urlModel, 'app\Urls\UserAddConfirmationUrlModel');
		}

		function test_ActionAddPost_WithInvalidParameters_DoesntCallAddUser(){
			TestHelper::setupNullUser(userContext);
			setupInvalidValidator();
			userFactory.returns("createUser", new User());
			userStorage.expectNever("addUser");

			sut.action_add_post();
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
			userValidatorFactory.returns("getAddUserValidator", $validator);
		}

	}

}