<?php
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
			$this->user = new User();
			$this->homegame = new Homegame();
			$this->cashgame = new Cashgame();
			$this->locations = array();
		}

		function test_Location_WithCashgame_IsSet(){
			$this->cashgame->setLocation('a');
			$sut = $this->getSut();

			$this->assertIdentical($sut->location, 'a');
		}

		function test_Location_WithoutCashgame_IsNull(){
			$this->cashgame = null;
			$sut = $this->getSut();

			$this->assertNull($sut->location);
		}

		function test_LocationSelectModel_IsCorrectType(){
			$sut = $this->getSut();

			$this->assertIsA($sut->locationSelectModel, 'core\FormFields\LocationFieldModel');
		}

		private function getSut(){
			return new AddModel($this->user, $this->homegame, $this->cashgame, $this->locations);
		}

	}

}