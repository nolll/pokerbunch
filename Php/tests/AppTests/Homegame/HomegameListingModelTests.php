namespace tests\AppTests\Homegame{

	use entities\Homegame;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\Homegame\List\HomegameListModel;
	use tests\TestHelper;

	class HomegameListModelTests extends UnitTestCase {

		private $user;

		function setUp(){
			user = new User();
		}

		function test_HomegameModels_WithoutHomegames_IsEmptyList(){
			$homegames = array();

			$sut = new HomegameListModel(user, $homegames);

			assertIdentical(0, count($sut.homegameModels));
		}

		function test_HomegameModels_With3Homegames_IsListWithThreeHomegameItemModels(){
			$dummyHomegame = new Homegame();
			$homegames = array($dummyHomegame, $dummyHomegame, $dummyHomegame);

			$sut = new HomegameListModel(user, $homegames);

			assertIsA($sut.homegameModels[0], 'app\Homegame\List\HomegameItemModel');
			assertIdentical(3, count($sut.homegameModels));
		}

	}

}