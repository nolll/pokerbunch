namespace tests\AppTests\User{

	use app\User\Edit\UserEditModel;
	use tests\TestHelper;
	use Domain\Classes\User;
	use tests\UnitTestCase;

	class UserEditModelTests extends UnitTestCase {

		function setUp(){
			parent::setUp();
		}

		function test_ActionEdit_OutputsUserData(){
			$currentUser = new User();
			$user = new User();
			$user.setUserName('a');
			$user.setDisplayName('b');
			$user.setRealName('c');
			$user.setEmail('d');

			$sut = new UserEditModel($currentUser, $user);

			assertIdentical('a', $sut.userName);
			assertIdentical('b', $sut.displayName);
			assertIdentical('c', $sut.realName);
			assertIdentical('d', $sut.email);
		}

	}

}