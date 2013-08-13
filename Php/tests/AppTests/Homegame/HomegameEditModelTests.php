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
			$this->user = new User();
			$this->homegame = new Homegame();
		}

		function test_ActionEdit_SetsHeading(){
			$this->homegame->setDisplayName('a');

			$sut = $this->getSut();

			$this->assertIdentical("a Settings", $sut->heading);
		}

		function test_ActionEdit_SetsCurrencySymbol(){
			$currency = new CurrencySettings();
			$currency->setSymbol('a');
			$this->homegame->setCurrency($currency);

			$sut = $this->getSut();

			$this->assertIdentical('a', $sut->currencySymbol);
		}

		function test_ActionEdit_SetsCurrencyLayout(){
			$sut = $this->getSut();

			$this->assertIsA($sut->currencyLayoutSelectModel, 'core\FormFields\CurrencyLayoutFieldModel');
		}

		function test_ActionEdit_SetsTimezoneSelectModel(){
			$sut = $this->getSut();

			$this->assertIsA($sut->timezoneSelectModel, 'core\FormFields\SelectFieldModel');
		}

		function test_ActionEdit_SetsDefaultBuyin(){
			$this->homegame->setDefaultBuyin(1);

			$sut = $this->getSut();

			$this->assertIdentical(1, $sut->defaultBuyin);
		}

		function test_ActionEdit_SetsHouseRules(){
			$this->homegame->setHouseRules('a');

			$sut = $this->getSut();

			$this->assertIdentical('a', $sut->houseRules);
		}

		function test_ActionEdit_SetsCashgamesEnabledStatus(){
			$this->homegame->cashgamesEnabled = true;

			$sut = $this->getSut();

			$this->assertTrue($sut->cashgamesEnabled);
		}

		function test_ActionEdit_SetsTournamentsEnabledStatus(){
			$this->homegame->tournamentsEnabled = true;

			$sut = $this->getSut();

			$this->assertTrue($sut->tournamentsEnabled);
		}

		function test_ActionEdit_SetsVideosEnabledStatus(){
			$this->homegame->videosEnabled = true;

			$sut = $this->getSut();

			$this->assertTrue($sut->videosEnabled);
		}

		function test_ActionEdit_SetsDescription(){
			$this->homegame->setDescription('a');

			$sut = $this->getSut();

			$this->assertIdentical('a', $sut->description);
		}

		function test_ActionEdit_SetsCancelUrl(){
			$sut = $this->getSut();

			$this->assertIsA($sut->cancelUrl, 'app\Urls\HomegameDetailsUrlModel');
		}

		private function getSut(){
			return new HomegameEditModel($this->user, $this->homegame);
		}

	}

}