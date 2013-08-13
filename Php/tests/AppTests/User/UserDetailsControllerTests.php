namespace tests\AppTests\User{

	use app\User\Details\UserDetailsController;
	use core\ClassNames;
	use tests\TestHelper;
	use exceptions\UserNotFoundException;
	use tests\UnitTestCase;

	class UserDetailsControllerTests extends UnitTestCase {

		/** @var UserDetailsController */
		private $sut;
		private $userContext;
		private $userStorage;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			$avatarService = TestHelper::getFake(ClassNames::$AvatarService);
			sut = new UserDetailsController(userContext, userStorage, $avatarService);
		}

		function test_ActionDetails_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireUser');
			expectException();

			sut.action_details("user1");
		}

		function test_ActionDetails_UserNotFound_ThrowsUserNotFoundException(){
			$currentUser = TestHelper::setupUser(userContext);
			$currentUser.setUserName('differentUserName');
			userStorage.returns("getUserByName", null);
			expectException(new UserNotFoundException());

			sut.action_details("user1");
		}

	}

}