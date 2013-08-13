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
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			userValidatorFactory = TestHelper::getFake(ClassNames::$UserValidatorFactory);
			$encryption = TestHelper::getFake(ClassNames::$Encryption);
			$saltGenerator = new SaltGenerator();
			$request = TestHelper::getFake(ClassNames::$Request);
			sut = new ChangePasswordController(userContext, userStorage, userValidatorFactory, $encryption, $saltGenerator, $request);
		}

		function test_ActionChange_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireUser');
			expectException();

			sut.action_change();
		}

		function test_ActionChangePost_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireUser');
			expectException();

			sut.action_change_post();
		}

		function test_ActionChangePost_WithEqualValidPasswords_CallsSetEncryptedPasswordAndSetSalt(){
			TestHelper::setupUser(userContext);
			setupValidValidator();

			userStorage.expectOnce("setEncryptedPassword");
			userStorage.expectOnce("setSalt");

			sut.action_change_post();
		}

		function test_ActionChangePost_WithEqualValidPasswords_RedirectsToConfirmation(){
			TestHelper::setupUser(userContext);
			setupValidValidator();

			$urlModel = sut.action_change_post();

			assertIsA($urlModel, 'app\Urls\ChangePasswordConfirmationUrlModel');
		}

		function test_ActionChangePost_WithDifferentValidPasswords_DoesntUpdateUser(){
			TestHelper::setupUser(userContext);
			setupInvalidValidator();

			userStorage.expectNever("updateUser");

			sut.action_change_post();
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
			userValidatorFactory.returns("getChangePasswordValidator", $validator);
		}

	}

}