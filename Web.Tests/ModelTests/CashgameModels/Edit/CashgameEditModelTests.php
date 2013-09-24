namespace tests\AppTests\Cashgame\Edit{

	use DateTime;
	use entities\Cashgame;
	use entities\Homegame;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\Cashgame\Edit\CashgameEditModel;
	use entities\GameStatus;
	use tests\TestHelper;

	class CashgameEditModelTests extends UnitTestCase {

		private $user;
		/** @var Homegame */
		private $homegame;
		/** @var Cashgame */
		private $cashgame;
		private $locations;

		function setUp(){
			user = new User();
			homegame = new Homegame();
			cashgame = new Cashgame();
			locations = array();
		}

		function test_IsoDate_IsSet(){
			cashgame.setStartTime(new DateTime("2010-01-01 01:00:00"));

			$sut = getSut();

			assertEqual("2010-01-01", $sut.isoDate);
		}

		function test_CancelUrl_IsSet(){
			$sut = getSut();

			assertIsA($sut.cancelUrl, 'app\Urls\CashgameDetailsUrlModel');
		}

		function test_DeleteUrl_IsSet(){
			$sut = getSut();

			assertIsA($sut.deleteUrl, 'app\Urls\CashgameDeleteUrlModel');
		}

		function test_EnableDelete_WithPublishedGame_IsFalse(){
			cashgame.setStatus(GameStatus::published);

			$sut = getSut();

			assertFalse($sut.enableDelete);
		}

		function test_EnableDelete_WithFinishedGame_IsTrue(){
			cashgame.setStatus(GameStatus::finished);

			$sut = getSut();

			assertTrue($sut.enableDelete);
		}

        function test_LocationSelectModel_IsCorrectType(){
            locations = array('location 1', 'location 2', 'location 3');

			$sut = getSut();

			assertIsA($sut.locationSelectModel, 'core\FormFields\LocationFieldModel');
        }

		private function getSut(){
			return new CashgameEditModel(user, homegame, cashgame, locations);
		}

	}

}