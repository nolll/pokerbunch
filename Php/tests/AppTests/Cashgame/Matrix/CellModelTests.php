<?php
namespace tests\AppTests\Cashgame\Matrix{

	use entities\Cashgame;
	use entities\CashgameResult;
	use tests\UnitTestCase;
	use app\Cashgame\Matrix\CellModel;
	use tests\TestHelper;

	class CellModelTests extends UnitTestCase {

		/** @var Cashgame */
		private $cashgame;
		/** @var CashgameResult */
		private $result;

		function setUp(){
			$this->cashgame = new Cashgame();
			$this->result = new CashgameResult();
		}

		function test_ShowWinnings_WithResult_IsTrue(){
			$sut = $this->getSut();

			$this->assertTrue($sut->showResult);
		}

		function test_Buyin_WithResult_IsSet(){
			$this->result->setBuyin(1);

			$sut = $this->getSut();

			$this->assertEqual('1', $sut->buyin);
		}

		function test_Cashout_WithResult_IsSet(){
			$this->result->setStack(1);

			$sut = $this->getSut();

			$this->assertEqual('1', $sut->cashout);
		}

		function test_Winnings_WithResult_IsSet(){
			$this->result->setWinnings(1);

			$sut = $this->getSut();

			$this->assertIdentical('+1', $sut->winnings);
		}

		function test_ShowWinnings_WithoutResult_IsFalse(){
			$this->result = null;

			$sut = $this->getSut();

			$this->assertFalse($sut->showResult);
		}

		function test_ShowTransactions_ResultWithBuyin_IsTrue(){
			$this->result->setBuyin(1);

			$sut = $this->getSut();

			$this->assertTrue($sut->showTransactions);
		}

		function test_ShowTransactions_ResultWithZeroBuyin_IsFalse(){
			$this->result->setBuyin(0);

			$sut = $this->getSut();

			$this->assertFalse($sut->showTransactions);
		}

		function test_WinningsClass_WithPositiveResult_IsPosResult(){
			$this->result->setWinnings(1);

			$sut = $this->getSut();

			$this->assertEqual("pos-result", $sut->resultClass);
		}

		function test_WinningsClass_WithNegativeResult_IsNegResult(){
			$this->result->setWinnings(-1);

			$sut = $this->getSut();

			$this->assertEqual("neg-result", $sut->resultClass);
		}

		function test_HasBestResult_PlayerWithBestResult_IsTrue(){
			$cashgameResult = new CashgameResult();
			$this->cashgame->setResults(array($cashgameResult));

			$sut = $this->getSut();

			$this->assertTrue($sut->hasBestResult);
		}

		function test_HasBestResult_PlayerWithoutBestResult_IsFalse(){
			$sut = $this->getSut();

			$this->assertFalse($sut->hasBestResult);
		}

		function getSut(){
			return new CellModel($this->cashgame, $this->result);
		}

	}

}