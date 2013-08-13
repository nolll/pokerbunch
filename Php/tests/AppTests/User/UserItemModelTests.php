namespace tests\AppTests\User{

	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\User\Listing\UserItemModel;
	use tests\TestHelper;

	class UserItemModelTests extends UnitTestCase {

		/** @var User */
		private $user;

		function setUp(){
			$this->user = new User();
		}

		function test_Item_SetsName(){
			$this->user->setDisplayName('a');

			$sut = $this->getSut();

			$this->assertIdentical('a', $sut->name);
		}

		function test_Item_SetsDetailsUrl(){
			$sut = $this->getSut();

			$this->assertIsA($sut->urlModel, 'app\Urls\UserDetailsUrlModel');
		}

		private function getSut(){
			return new UserItemModel($this->user);
		}

	}

}