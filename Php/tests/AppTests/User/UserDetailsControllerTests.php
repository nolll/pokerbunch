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
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			$avatarService = TestHelper::getFake(ClassNames::$AvatarService);
			$this->sut = new UserDetailsController($this->userContext, $this->userStorage, $avatarService);
		}

		function test_ActionDetails_NotAuthorized_ThrowsException(){
			$this->userContext->throwOn('requireUser');
			$this->expectException();

			$this->sut->action_details("user1");
		}

		function test_ActionDetails_UserNotFound_ThrowsUserNotFoundException(){
			$currentUser = TestHelper::setupUser($this->userContext);
			$currentUser->setUserName('differentUserName');
			$this->userStorage->returns("getUserByName", null);
			$this->expectException(new UserNotFoundException());

			$this->sut->action_details("user1");
		}

	}

}