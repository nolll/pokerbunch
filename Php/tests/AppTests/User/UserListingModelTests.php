namespace tests\AppTests\User{

	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\User\List\UserListModel;
	use tests\TestHelper;

	class UserListModelTests extends UnitTestCase {

		function setUp(){
			parent::setUp();
		}

		function test_UserModels_NoUsers_IsEmptyList(){
			$user = new User();
			$users = array();

			$sut = new UserListModel($user, $users);

			assertIdentical(0, count($sut.userModels));
		}

		function test_UserModels_With3Users_Has3ItemsAndFirstItemIsCorrectType(){
			$user = new User();
			$users = array($user, $user, $user);

			$sut = new UserListModel($user, $users);

			assertIsA($sut.userModels[0], 'app\User\List\UserItemModel');
			assertIdentical(3, count($sut.userModels));
		}

	}

}