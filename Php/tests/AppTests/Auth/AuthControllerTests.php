namespace tests\AppTests\Auth{

	use app\Auth\AuthController;
	use core\ClassNames;
	use core\Validation\ValidatorFake;
	use tests\TestHelper;
	use core\Validation\Validator;
	use Domain\Classes\User;
	use tests\UnitTestCase;

	class AuthControllerTests extends UnitTestCase {

		/** @var AuthController */
		private $sut;
		private $request;
		private $response;
		private $userStorage;
		private $userValidatorFactory;

		function setUp(){
			userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			$encryption = TestHelper::getFake(ClassNames::$Encryption);
			userValidatorFactory = TestHelper::getFake(ClassNames::$UserValidatorFactory);
			response = TestHelper::getFake(ClassNames::$Response);
			request = TestHelper::getFake(ClassNames::$Request);
			sut = new AuthController(userStorage, $encryption, userValidatorFactory, response, request);
		}

		function test_ActionLoginPost_UserExistsButNoReturnUrl_RedirectsToRoot(){
			$user = new User();
			userStorage.returns("getUserByCredentials", $user);
			setupValidValidator();

			$urlModel = sut.action_login_post();

			assertIsA($urlModel, 'app\Urls\HomeUrlModel');
		}

		function test_ActionLoginPost_UserExistsAndWithReturnUrl_RedirectsToReturnUrl(){
			$user = new User();
			userStorage.returns("getUserByCredentials", $user);
			setupValidValidator();
			TestHelper::setupPostParameter(request, "return", "/returnurl");

			$urlModel = sut.action_login_post();

			assertIsA($urlModel, 'app\Urls\BaseClasses\UrlModel');
			assertIdentical($urlModel.getUrl(), '/returnurl');
		}

		function test_ActionLoginPost_UserNotFound_ShowsForm(){
			userStorage.returns("getUserByCredentials", null);
			setupInvalidValidator();

			$viewResult = sut.action_login_post();

			assertIsA($viewResult.model, 'app\Auth\AuthLoginModel');
		}

		function test_ActionLoginPost_WithUserNameAndPassword_SetsSessionCookie(){
			$user = new User();
			userStorage.returns("getUserByCredentials", $user);
			setupValidValidator();
			response.expectOnce("setSessionCookie", array("token", "*"));

			sut.action_login_post();
		}

		function test_ActionLoginPost_UserFoundAndRememberChecked_SetsPersistentCookie(){
			$user = new User();
			userStorage.returns("getUserByCredentials", $user);
			setupValidValidator();
			TestHelper::setupPostParameter(request, "remember", "1");
			response.expectOnce("setPersistentCookie", array("token", "*"));
			sut.action_login_post();
		}

		function test_ActionLogout_ClearsCookies(){
			response.expectOnce("clearCookie", array("token"));

			sut.action_logout();
		}

		function test_ActionLogout_RedirectsToHome(){
			$urlModel = sut.action_logout();

			assertIsA($urlModel, 'app\Urls\HomeUrlModel');
		}

		function setupPostedUserAndPassword(){
			TestHelper::setupPostParameter(request, "ln", "posted login name");
			TestHelper::setupPostParameter(request, "pw", "posted password");
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
			userValidatorFactory.returns("getLoginValidator", $validator);
		}

	}

}