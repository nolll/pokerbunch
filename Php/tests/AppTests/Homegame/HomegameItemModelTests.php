namespace tests\AppTests\Homegame{

	use entities\Homegame;
	use tests\UnitTestCase;
	use app\Homegame\List\HomegameItemModel;
	use tests\TestHelper;

	class HomegameItemModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;

		function setUp(){
			homegame = new Homegame();
		}

		function test_Item_SetsName(){
			homegame.setDisplayName('a');

			$sut = new HomegameItemModel(homegame);

			assertIdentical('a', $sut.name);
		}

		function test_Item_SetsDetailsUrl(){
			$sut = new HomegameItemModel(homegame);

			assertIsA($sut.urlModel, 'app\Urls\HomegameDetailsUrlModel');
		}

	}

}