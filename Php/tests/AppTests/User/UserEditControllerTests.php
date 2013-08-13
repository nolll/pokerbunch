namespace tests\AppTests\User{

	use app\User\Edit\UserEditController;
	use core\ClassNames;
	use core\Validation\ValidatorFake;
	use tests\TestHelper;
	use core\Validation\Validator;
	use exceptions\UserNotFoundException;
	use Domain\Classes\User;
	use tests\UnitTestCase;

	class UserEditControllerTests extends UnitTestCase {

		/** @var UserEditController */
		private $sut;
		private $userContext;
		private $request;
		private $userStorage;
		private $userValidatorFactory;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			$this->userValidatorFactory = TestHelper::getFake(ClassNames::$UserValidatorFactory);
			$this->request = TestHelper::getFake(ClassNames::$Request);
			$this->sut = new UserEditController($this->userContext, $this->userStorage, $this->userValidatorFactory, $this->request);
		}

		function test_ActionEdit_NotAuthorized_ThrowsException(){
			$this->userContext->throwOn('requireUser');
			$this->expectException();

			$this->sut->action_edit("user1");
		}

		function test_ActionEdit_UserNotFound_ThrowsUserNotFoundException(){
			$this->userStorage->returns("getUserByName", null);
			TestHelper::setupUser($this->userContext);
			$this->expectException(new UserNotFoundException());

			$this->sut->action_edit("user1");
		}

		function test_ActionEditPost_NotAuthorized_ThrowsException(){
			$this->userContext->throwOn('requireUser');
			$this->expectException();

			$this->sut->action_edit_post("user1");
		}

		function test_ActionEditPost_WithGoodUserAndGoodData_CallsUpdateUserWithPostedData(){
			TestHelper::setupUser($this->userContext);
			$user = new User();
			$this->userStorage->returns("getUserByName", $user);
			$this->setupValidValidator();
			$postedUser = $this->setupPostedUser();
			$this->userStorage->expectOnce("updateUser", array($postedUser));

			$this->sut->action_edit_post("user1");
		}

		function test_ActionEditPost_WithGoodUserAndGoodData_RedirectsToDetails(){
			TestHelper::setupUser($this->userContext);
			$user = new User();
			$this->userStorage->returns("getUserByName", $user);
			$this->setupValidValidator();

			$urlModel = $this->sut->action_edit_post("user1");

			$this->assertIsA($urlModel, 'app\Urls\UserDetailsUrlModel');
		}

		function test_ActionEditPost_WithInvalidUser_DoesntCallUpdateUser(){
			TestHelper::setupUser($this->userContext);
			$user = new User();
			$this->userStorage->returns("getUserByName", $user);
			$this->setupInvalidValidator();
			$this->userStorage->expectNever("updateUser");

			$this->sut->action_edit_post("user1");
		}

		function setupPostedUser(){
			$postedUser = new User();
			$postedUser->setDisplayName('posted display name');
			$postedUser->setEmail('posted email');
			$postedUser->setRealName('posted real name');
			TestHelper::setupPostParameter($this->request, 'displayname', $postedUser->getDisplayName());
			TestHelper::setupPostParameter($this->request, 'email', $postedUser->getEmail());
			TestHelper::setupPostParameter($this->request, 'realname', $postedUser->getRealName());
			return $postedUser;
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
			$this->userValidatorFactory->returns("getEditUserValidator", $validator);
		}

	}

}