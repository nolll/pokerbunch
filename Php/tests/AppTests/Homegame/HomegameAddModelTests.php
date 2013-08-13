namespace tests\AppTests\Homegame{

	use entities\CurrencySettings;
	use entities\Homegame;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\Homegame\Add\HomegameAddModel;
	use tests\TestHelper;

	class HomegameAddModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;

		function setUp(){
			homegame = new Homegame();
		}

		function test_DisplayName_IsSet(){
			homegame.setDisplayName('a');

			$sut = getSut();

			assertIdentical('a', $sut.displayName);
		}

		function test_Description_IsSet(){
			homegame.setDescription('a');

			$sut = getSut();

			assertIdentical('a', $sut.description);
		}

		function test_CurrencySymbol_IsSet(){
			$currency = new CurrencySettings();
			$currency.setSymbol('a');
			homegame.setCurrency($currency);

			$sut = getSut();

			assertIdentical('a', $sut.currencySymbol);
		}

		function test_CurrencyLayoutSelectModel_IsCorrectType(){
			$sut = getSut();

			assertIsA($sut.currencyLayoutSelectModel, 'core\FormFields\CurrencyLayoutFieldModel');
		}

		function test_TimezoneSelectModel_IsCorrectType(){
			$sut = getSut();

			assertIsA($sut.timezoneSelectModel, 'core\FormFields\TimezoneFieldModel');
		}

		private function getSut(){
			return new HomegameAddModel(new User(), homegame);
		}

	}

}