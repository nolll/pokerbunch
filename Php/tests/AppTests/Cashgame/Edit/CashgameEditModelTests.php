<?php
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
			$this->user = new User();
			$this->homegame = new Homegame();
			$this->cashgame = new Cashgame();
			$this->locations = array();
		}

		function test_IsoDate_IsSet(){
			$this->cashgame->setStartTime(new DateTime("2010-01-01 01:00:00"));

			$sut = $this->getSut();

			$this->assertEqual("2010-01-01", $sut->isoDate);
		}

		function test_CancelUrl_IsSet(){
			$sut = $this->getSut();

			$this->assertIsA($sut->cancelUrl, 'app\Urls\CashgameDetailsUrlModel');
		}

		function test_DeleteUrl_IsSet(){
			$sut = $this->getSut();

			$this->assertIsA($sut->deleteUrl, 'app\Urls\CashgameDeleteUrlModel');
		}

		function test_EnableDelete_WithPublishedGame_IsFalse(){
			$this->cashgame->setStatus(GameStatus::published);

			$sut = $this->getSut();

			$this->assertFalse($sut->enableDelete);
		}

		function test_EnableDelete_WithFinishedGame_IsTrue(){
			$this->cashgame->setStatus(GameStatus::finished);

			$sut = $this->getSut();

			$this->assertTrue($sut->enableDelete);
		}

        function test_LocationSelectModel_IsCorrectType(){
            $this->locations = array('location 1', 'location 2', 'location 3');

			$sut = $this->getSut();

			$this->assertIsA($sut->locationSelectModel, 'core\FormFields\LocationFieldModel');
        }

		private function getSut(){
			return new CashgameEditModel($this->user, $this->homegame, $this->cashgame, $this->locations);
		}

	}

}