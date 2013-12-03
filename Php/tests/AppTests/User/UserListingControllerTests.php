namespace tests\AppTests\User{

	use app\User\List\UserListController;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class UserListControllerTests extends UnitTestCase {

		/** @var UserListController */
		private $sut;
		private $userContext;

		function setUp(){
			$userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			sut = new UserListController(userContext, $userStorage);
		}

		function test_ActionIndex_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireAdmin');
			expectException();

			sut.action_list();
		}

	}

}