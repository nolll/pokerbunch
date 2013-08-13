<?php
namespace tests\AppTests\Cashgame\Action{

	use DateTime;
	use entities\Cashgame;
	use entities\Homegame;
	use entities\Player;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\Cashgame\Action\CashoutModel;
	use tests\TestHelper;

	class CashoutModelTests extends UnitTestCase {

		private $homegame;
		/** @var Player */
		private $player;
		private $postedAmount;

		function setUp(){
			$this->homegame = new Homegame();
			$this->player = new Player();
			$this->postedAmount = null;
		}

		function getSut(){
			$runningGame = new Cashgame();
			$runningGame->setStartTime(new DateTime());
			return new CashoutModel(new User(), $this->homegame, $this->player, null, $runningGame, $this->postedAmount);
		}

		function test_CashoutUrl_IsSet(){
			$sut = $this->getSut();

			$this->assertIsA($sut->cashoutUrl, 'app\Urls\CashgameCashoutUrlModel');
		}

		function test_CashoutAmount_WithPostedAmount_IsSetToEmptyString(){
			$sut = $this->getSut();

			$this->assertEqual($sut->cashoutAmount, '');
		}

		function test_CashoutAmount_WithPostedAmount_IsSet(){
			$this->postedAmount = 1;

			$sut = $this->getSut();

			$this->assertEqual($sut->cashoutAmount, 1);
		}

	}

}