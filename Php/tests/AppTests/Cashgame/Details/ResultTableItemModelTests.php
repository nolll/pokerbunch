<?php
namespace tests\AppTests\Cashgame\Details{

	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Player;
	use entities\Homegame;
	use tests\UnitTestCase;
	use DateTime;
	use app\Cashgame\Details\ResultTableItem\ResultTableItemModel;
	use tests\TestHelper;

	class ResultTableItemModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;
		/** @var Cashgame */
		private $cashgame;
		/** @var CashgameResult */
		private $result;

		function setUp(){
			$this->homegame = new Homegame();
			$this->cashgame = new Cashgame();
			$this->cashgame->setStartTime(new DateTime());
			$this->result = new CashgameResult();
		}

		function test_Name_IsSet(){
			$player = new Player();
			$player->setDisplayName('a');
			$this->result->setPlayer($player);
			$sut = $this->getSut();

			$this->assertIdentical('a', $sut->name);
		}

		function test_PlayerUrl_IsCorrectType(){
			$this->result->setPlayer(new Player());

			$sut = $this->getSut();

			$this->assertIsA($sut->playerUrl, 'app\Urls\CashgameActionUrlModel');
		}

		function test_Buyin_IsSet(){
			$this->result->setBuyin(1);
			$sut = $this->getSut();

			$this->assertIdentical("$1", $sut->buyin);
		}

		function test_Cashout_IsSet(){
			$this->result->setStack(1);
			$sut = $this->getSut();

			$this->assertIdentical("$1", $sut->cashout);
		}

		function test_Winnings_IsSet(){
			$this->result->setWinnings(1);
			$sut = $this->getSut();

			$this->assertIdentical("+$1", $sut->winnings);
		}

		function test_WinningsClass_BuyinEqualToCashout_IsEmpty(){
			$this->result->setBuyin(100);
			$this->result->setStack(100);

			$sut = $this->getSut();

			$this->assertIdentical("", $sut->winningsClass);
		}

		function test_WinningsClass_ResultIsPositive_IsPositive(){
			$this->result->setWinnings(1);

			$sut = $this->getSut();

			$this->assertIdentical("pos-result", $sut->winningsClass);
		}

		function test_WinningsClass_BuyinBiggerThanCashout_IsNegative(){
			$this->result->setWinnings(-1);

			$sut = $this->getSut();

			$this->assertIdentical("neg-result", $sut->winningsClass);
		}

		function test_Winrate_WithDuration_IsSet(){
			$this->result->setPlayedTime(60);
			$this->result->setWinnings(1);

			$sut = $this->getSut();

			$this->assertIdentical("$1/h", $sut->winrate);
		}

		function test_Winrate_WithoutDuration_IsEmpty(){
			$sut = $this->getSut();

			$this->assertIdentical("", $sut->winrate);
		}

		function getSut(){
			return new ResultTableItemModel($this->homegame, $this->cashgame, $this->result);
		}

	}

}