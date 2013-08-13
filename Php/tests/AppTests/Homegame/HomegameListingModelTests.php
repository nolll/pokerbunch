namespace tests\AppTests\Homegame{

	use entities\Homegame;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\Homegame\Listing\HomegameListingModel;
	use tests\TestHelper;

	class HomegameListingModelTests extends UnitTestCase {

		private $user;

		function setUp(){
			$this->user = new User();
		}

		function test_HomegameModels_WithoutHomegames_IsEmptyList(){
			$homegames = array();

			$sut = new HomegameListingModel($this->user, $homegames);

			$this->assertIdentical(0, count($sut->homegameModels));
		}

		function test_HomegameModels_With3Homegames_IsListWithThreeHomegameItemModels(){
			$dummyHomegame = new Homegame();
			$homegames = array($dummyHomegame, $dummyHomegame, $dummyHomegame);

			$sut = new HomegameListingModel($this->user, $homegames);

			$this->assertIsA($sut->homegameModels[0], 'app\Homegame\Listing\HomegameItemModel');
			$this->assertIdentical(3, count($sut->homegameModels));
		}

	}

}