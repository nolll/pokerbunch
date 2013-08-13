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
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			userValidatorFactory = TestHelper::getFake(ClassNames::$UserValidatorFactory);
			request = TestHelper::getFake(ClassNames::$Request);
			sut = new UserEditController(userContext, userStorage, userValidatorFactory, request);
		}

		function test_ActionEdit_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireUser');
			expectException();

			sut.action_edit("user1");
		}

		function test_ActionEdit_UserNotFound_ThrowsUserNotFoundException(){
			userStorage.returns("getUserByName", null);
			TestHelper::setupUser(userContext);
			expectException(new UserNotFoundException());

			sut.action_edit("user1");
		}

		function test_ActionEditPost_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireUser');
			expectException();

			sut.action_edit_post("user1");
		}

		function test_ActionEditPost_WithGoodUserAndGoodData_CallsUpdateUserWithPostedData(){
			TestHelper::setupUser(userContext);
			$user = new User();
			userStorage.returns("getUserByName", $user);
			setupValidValidator();
			$postedUser = setupPostedUser();
			userStorage.expectOnce("updateUser", array($postedUser));

			sut.action_edit_post("user1");
		}

		function test_ActionEditPost_WithGoodUserAndGoodData_RedirectsToDetails(){
			TestHelper::setupUser(userContext);
			$user = new User();
			userStorage.returns("getUserByName", $user);
			setupValidValidator();

			$urlModel = sut.action_edit_post("user1");

			assertIsA($urlModel, 'app\Urls\UserDetailsUrlModel');
		}

		function test_ActionEditPost_WithInvalidUser_DoesntCallUpdateUser(){
			TestHelper::setupUser(userContext);
			$user = new User();
			userStorage.returns("getUserByName", $user);
			setupInvalidValidator();
			userStorage.expectNever("updateUser");

			sut.action_edit_post("user1");
		}

		function setupPostedUser(){
			$postedUser = new User();
			$postedUser.setDisplayName('posted display name');
			$postedUser.setEmail('posted email');
			$postedUser.setRealName('posted real name');
			TestHelper::setupPostParameter(request, 'displayname', $postedUser.getDisplayName());
			TestHelper::setupPostParameter(request, 'email', $postedUser.getEmail());
			TestHelper::setupPostParameter(request, 'realname', $postedUser.getRealName());
			return $postedUser;
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
			userValidatorFactory.returns("getEditUserValidator", $validator);
		}

	}

}