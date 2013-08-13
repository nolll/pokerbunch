namespace tests\AppTests\User{

	use app\User\Listing\UserListingController;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class UserListingControllerTests extends UnitTestCase {

		/** @var UserListingController */
		private $sut;
		private $userContext;

		function setUp(){
			$userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			sut = new UserListingController(userContext, $userStorage);
		}

		function test_ActionIndex_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireAdmin');
			expectException();

			sut.action_listing();
		}

	}

}