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
			$this->userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			$encryption = TestHelper::getFake(ClassNames::$Encryption);
			$this->userValidatorFactory = TestHelper::getFake(ClassNames::$UserValidatorFactory);
			$this->response = TestHelper::getFake(ClassNames::$Response);
			$this->request = TestHelper::getFake(ClassNames::$Request);
			$this->sut = new AuthController($this->userStorage, $encryption, $this->userValidatorFactory, $this->response, $this->request);
		}

		function test_ActionLoginPost_UserExistsButNoReturnUrl_RedirectsToRoot(){
			$user = new User();
			$this->userStorage->returns("getUserByCredentials", $user);
			$this->setupValidValidator();

			$urlModel = $this->sut->action_login_post();

			$this->assertIsA($urlModel, 'app\Urls\HomeUrlModel');
		}

		function test_ActionLoginPost_UserExistsAndWithReturnUrl_RedirectsToReturnUrl(){
			$user = new User();
			$this->userStorage->returns("getUserByCredentials", $user);
			$this->setupValidValidator();
			TestHelper::setupPostParameter($this->request, "return", "/returnurl");

			$urlModel = $this->sut->action_login_post();

			$this->assertIsA($urlModel, 'app\Urls\BaseClasses\UrlModel');
			$this->assertIdentical($urlModel->getUrl(), '/returnurl');
		}

		function test_ActionLoginPost_UserNotFound_ShowsForm(){
			$this->userStorage->returns("getUserByCredentials", null);
			$this->setupInvalidValidator();

			$viewResult = $this->sut->action_login_post();

			$this->assertIsA($viewResult->model, 'app\Auth\AuthLoginModel');
		}

		function test_ActionLoginPost_WithUserNameAndPassword_SetsSessionCookie(){
			$user = new User();
			$this->userStorage->returns("getUserByCredentials", $user);
			$this->setupValidValidator();
			$this->response->expectOnce("setSessionCookie", array("token", "*"));

			$this->sut->action_login_post();
		}

		function test_ActionLoginPost_UserFoundAndRememberChecked_SetsPersistentCookie(){
			$user = new User();
			$this->userStorage->returns("getUserByCredentials", $user);
			$this->setupValidValidator();
			TestHelper::setupPostParameter($this->request, "remember", "1");
			$this->response->expectOnce("setPersistentCookie", array("token", "*"));
			$this->sut->action_login_post();
		}

		function test_ActionLogout_ClearsCookies(){
			$this->response->expectOnce("clearCookie", array("token"));

			$this->sut->action_logout();
		}

		function test_ActionLogout_RedirectsToHome(){
			$urlModel = $this->sut->action_logout();

			$this->assertIsA($urlModel, 'app\Urls\HomeUrlModel');
		}

		function setupPostedUserAndPassword(){
			TestHelper::setupPostParameter($this->request, "ln", "posted login name");
			TestHelper::setupPostParameter($this->request, "pw", "posted password");
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
			$this->userValidatorFactory->returns("getLoginValidator", $validator);
		}

	}

}