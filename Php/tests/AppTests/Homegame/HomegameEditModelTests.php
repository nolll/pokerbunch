namespace tests\AppTests\Homegame{

	use entities\CurrencySettings;
	use entities\Homegame;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\Homegame\Edit\HomegameEditModel;
	use tests\TestHelper;

	class HomegameEditModelTests extends UnitTestCase {

		private $user;
		/** @var Homegame */
		private $homegame;

		function setUp(){
			user = new User();
			homegame = new Homegame();
		}

		function test_ActionEdit_SetsHeading(){
			homegame.setDisplayName('a');

			$sut = getSut();

			assertIdentical("a Settings", $sut.heading);
		}

		function test_ActionEdit_SetsCurrencySymbol(){
			$currency = new CurrencySettings();
			$currency.setSymbol('a');
			homegame.setCurrency($currency);

			$sut = getSut();

			assertIdentical('a', $sut.currencySymbol);
		}

		function test_ActionEdit_SetsCurrencyLayout(){
			$sut = getSut();

			assertIsA($sut.currencyLayoutSelectModel, 'core\FormFields\CurrencyLayoutFieldModel');
		}

		function test_ActionEdit_SetsTimezoneSelectModel(){
			$sut = getSut();

			assertIsA($sut.timezoneSelectModel, 'core\FormFields\SelectFieldModel');
		}

		function test_ActionEdit_SetsDefaultBuyin(){
			homegame.setDefaultBuyin(1);

			$sut = getSut();

			assertIdentical(1, $sut.defaultBuyin);
		}

		function test_ActionEdit_SetsHouseRules(){
			homegame.setHouseRules('a');

			$sut = getSut();

			assertIdentical('a', $sut.houseRules);
		}

		function test_ActionEdit_SetsCashgamesEnabledStatus(){
			homegame.cashgamesEnabled = true;

			$sut = getSut();

			assertTrue($sut.cashgamesEnabled);
		}

		function test_ActionEdit_SetsTournamentsEnabledStatus(){
			homegame.tournamentsEnabled = true;

			$sut = getSut();

			assertTrue($sut.tournamentsEnabled);
		}

		function test_ActionEdit_SetsVideosEnabledStatus(){
			homegame.videosEnabled = true;

			$sut = getSut();

			assertTrue($sut.videosEnabled);
		}

		function test_ActionEdit_SetsDescription(){
			homegame.setDescription('a');

			$sut = getSut();

			assertIdentical('a', $sut.description);
		}

		function test_ActionEdit_SetsCancelUrl(){
			$sut = getSut();

			assertIsA($sut.cancelUrl, 'app\Urls\HomegameDetailsUrlModel');
		}

		private function getSut(){
			return new HomegameEditModel(user, homegame);
		}

	}

}