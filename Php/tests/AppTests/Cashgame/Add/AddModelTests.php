namespace tests\AppTests\Cashgame\Add{

	use entities\Cashgame;
	use app\Cashgame\Add\AddModel;
	use entities\Homegame;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use tests\TestHelper;

	class AddModelTests extends UnitTestCase {

		private $user;
		private $homegame;
		/** @var Cashgame */
		private $cashgame;
		private $locations;

		function setUp(){
			parent::setUp();
			user = new User();
			homegame = new Homegame();
			cashgame = new Cashgame();
			locations = array();
		}

		function test_Location_WithCashgame_IsSet(){
			cashgame.setLocation('a');
			$sut = getSut();

			assertIdentical($sut.location, 'a');
		}

		function test_Location_WithoutCashgame_IsNull(){
			cashgame = null;
			$sut = getSut();

			assertNull($sut.location);
		}

		function test_LocationSelectModel_IsCorrectType(){
			$sut = getSut();

			assertIsA($sut.locationSelectModel, 'core\FormFields\LocationFieldModel');
		}

		private function getSut(){
			return new AddModel(user, homegame, cashgame, locations);
		}

	}

}