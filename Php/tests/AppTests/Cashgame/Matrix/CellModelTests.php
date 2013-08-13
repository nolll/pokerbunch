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
			cashgame = new Cashgame();
			result = new CashgameResult();
		}

		function test_ShowWinnings_WithResult_IsTrue(){
			$sut = getSut();

			assertTrue($sut.showResult);
		}

		function test_Buyin_WithResult_IsSet(){
			result.setBuyin(1);

			$sut = getSut();

			assertEqual('1', $sut.buyin);
		}

		function test_Cashout_WithResult_IsSet(){
			result.setStack(1);

			$sut = getSut();

			assertEqual('1', $sut.cashout);
		}

		function test_Winnings_WithResult_IsSet(){
			result.setWinnings(1);

			$sut = getSut();

			assertIdentical('+1', $sut.winnings);
		}

		function test_ShowWinnings_WithoutResult_IsFalse(){
			result = null;

			$sut = getSut();

			assertFalse($sut.showResult);
		}

		function test_ShowTransactions_ResultWithBuyin_IsTrue(){
			result.setBuyin(1);

			$sut = getSut();

			assertTrue($sut.showTransactions);
		}

		function test_ShowTransactions_ResultWithZeroBuyin_IsFalse(){
			result.setBuyin(0);

			$sut = getSut();

			assertFalse($sut.showTransactions);
		}

		function test_WinningsClass_WithPositiveResult_IsPosResult(){
			result.setWinnings(1);

			$sut = getSut();

			assertEqual("pos-result", $sut.resultClass);
		}

		function test_WinningsClass_WithNegativeResult_IsNegResult(){
			result.setWinnings(-1);

			$sut = getSut();

			assertEqual("neg-result", $sut.resultClass);
		}

		function test_HasBestResult_PlayerWithBestResult_IsTrue(){
			$cashgameResult = new CashgameResult();
			cashgame.setResults(array($cashgameResult));

			$sut = getSut();

			assertTrue($sut.hasBestResult);
		}

		function test_HasBestResult_PlayerWithoutBestResult_IsFalse(){
			$sut = getSut();

			assertFalse($sut.hasBestResult);
		}

		function getSut(){
			return new CellModel(cashgame, result);
		}

	}

}