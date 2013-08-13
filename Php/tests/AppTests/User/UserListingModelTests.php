namespace tests\AppTests\User{

	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\User\Listing\UserListingModel;
	use tests\TestHelper;

	class UserListingModelTests extends UnitTestCase {

		function setUp(){
			parent::setUp();
		}

		function test_UserModels_NoUsers_IsEmptyList(){
			$user = new User();
			$users = array();

			$sut = new UserListingModel($user, $users);

			$this->assertIdentical(0, count($sut->userModels));
		}

		function test_UserModels_With3Users_Has3ItemsAndFirstItemIsCorrectType(){
			$user = new User();
			$users = array($user, $user, $user);

			$sut = new UserListingModel($user, $users);

			$this->assertIsA($sut->userModels[0], 'app\User\Listing\UserItemModel');
			$this->assertIdentical(3, count($sut->userModels));
		}

	}

}