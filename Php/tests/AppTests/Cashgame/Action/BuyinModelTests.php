<?php
namespace tests\AppTests\Cashgame\Action{

	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Homegame;
	use entities\Player;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\Cashgame\Action\BuyinModel;
	use tests\TestHelper;

	class BuyinModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;
		/** @var Player */
		private $player;
		/** @var Cashgame */
		private $cashgame;
		private $postedValue;

		function setUp(){
			$this->homegame = new Homegame();
			$this->player = new Player();
			$this->cashgame = new Cashgame();
			$this->postedValue = null;
		}

		function getSut(){
			return new BuyinModel(new User(), $this->homegame, $this->player, null, $this->cashgame, $this->postedValue);
		}

		function test_BuyinUrl_IsSet(){
			$sut = $this->getSut();

			$this->assertIsA($sut->buyinUrl, 'app\Urls\CashgameBuyinUrlModel');
		}

		function test_StackFieldEnabled_WithPlayerInGame_IsTrue(){
			$this->player->setId(1);
			$cashgameResult = new CashgameResult();
			$cashgameResult->setPlayer($this->player);
			$this->cashgame->setResults(array($cashgameResult));

			$sut = $this->getSut();

			$this->assertTrue($sut->stackFieldEnabled);
		}

		function test_StackFieldEnabled_WithPlayerNotInGame_IsFalse(){
			$this->player->setId(2);

			$sut = $this->getSut();

			$this->assertFalse($sut->stackFieldEnabled);
		}

		function test_BuyinAmount_WithoutPostedValue_IsSetToDefaultBuyin(){
			$this->homegame->setDefaultBuyin(1);

			$sut = $this->getSut();

			$this->assertIdentical(1, $sut->buyinAmount);
		}

		function test_BuyinAmount_WithPostedValue_IsSetToPostedValue(){
			$this->homegame->setDefaultBuyin(1);
			$this->postedValue = 2;

			$sut = $this->getSut();

			$this->assertIdentical(2, $sut->buyinAmount);
		}

	}

}