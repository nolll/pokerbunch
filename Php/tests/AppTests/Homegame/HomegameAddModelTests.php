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
			$this->homegame = new Homegame();
		}

		function test_DisplayName_IsSet(){
			$this->homegame->setDisplayName('a');

			$sut = $this->getSut();

			$this->assertIdentical('a', $sut->displayName);
		}

		function test_Description_IsSet(){
			$this->homegame->setDescription('a');

			$sut = $this->getSut();

			$this->assertIdentical('a', $sut->description);
		}

		function test_CurrencySymbol_IsSet(){
			$currency = new CurrencySettings();
			$currency->setSymbol('a');
			$this->homegame->setCurrency($currency);

			$sut = $this->getSut();

			$this->assertIdentical('a', $sut->currencySymbol);
		}

		function test_CurrencyLayoutSelectModel_IsCorrectType(){
			$sut = $this->getSut();

			$this->assertIsA($sut->currencyLayoutSelectModel, 'core\FormFields\CurrencyLayoutFieldModel');
		}

		function test_TimezoneSelectModel_IsCorrectType(){
			$sut = $this->getSut();

			$this->assertIsA($sut->timezoneSelectModel, 'core\FormFields\TimezoneFieldModel');
		}

		private function getSut(){
			return new HomegameAddModel(new User(), $this->homegame);
		}

	}

}