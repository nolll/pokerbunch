namespace tests\AppTests\User{

	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\User\List\UserItemModel;
	use tests\TestHelper;

	class UserItemModelTests extends UnitTestCase {

		/** @var User */
		private $user;

		function setUp(){
			user = new User();
		}

		function test_Item_SetsName(){
			user.setDisplayName('a');

			$sut = getSut();

			assertIdentical('a', $sut.name);
		}

		function test_Item_SetsDetailsUrl(){
			$sut = getSut();

			assertIsA($sut.urlModel, 'app\Urls\UserDetailsUrlModel');
		}

		private function getSut(){
			return new UserItemModel(user);
		}

	}

}