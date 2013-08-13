namespace tests\AppTests\Cashgame\Running{

	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Player;
	use tests\Fakes\TimerFake;
	use entities\Homegame;
	use tests\UnitTestCase;
	use app\Cashgame\Running\StatusItemModel;
	use DateTime;
	use tests\TestHelper;

	class StatusItemModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;
		/** @var Cashgame */
		private $cashgame;
		/** @var CashgameResult */
		private $result;
		/** @var TimerFake */
		private $timer;
		private $isManager;

		function setUp(){
			homegame = new Homegame();
			cashgame = new Cashgame();
			cashgame.setStartTime(new DateTime());
			result = new CashgameResult();
			timer = new TimerFake();
			isManager = false;
		}

		function test_Name_IsSets(){
			$player = new Player();
			$player.setDisplayName('a');
			result.setPlayer($player);

			$sut = getSut();

			assertIdentical('a', $sut.name);
		}

		function test_PlayerUrl_IsSet(){
			$player = new Player();
			result.setPlayer($player);

			$sut = getSut();

			assertIsA($sut.playerUrl, 'app\Urls\CashgameActionUrlModel');
		}

		function test_Buyin_IsSet(){
			result.setBuyin(1);

			$sut = getSut();

			assertIdentical("$1", $sut.buyin);
		}

		function test_Stack_IsSet(){
			result.setStack(1);

			$sut = getSut();

			assertIdentical("$1", $sut.stack);
		}

		function test_Winnings_IsSet(){
			result.setWinnings(1);

			$sut = getSut();

			assertIdentical("+$1", $sut.winnings);
		}

		function test_Time_IsSetToDifferenceBetweenNowAndLastCheckpoint(){
			setLastCheckpointTime(new DateTime('2010-01-01 01:00:00'));
			timer.setTime(new DateTime('2010-01-01 01:01:00'));
			$sut = getSut();

			assertIdentical("1 minute", $sut.time);
		}

		function test_WinningsClass_BuyinEqualToCashout_IsEmpty(){
			result.setBuyin(100);
			result.setStack(100);

			$sut = getSut();

			assertIdentical("", $sut.winningsClass);
		}

		function test_WinningsClass_WithPositiveResult_IsPositive(){
			result.setWinnings(1);

			$sut = getSut();

			assertIdentical('pos-result', $sut.winningsClass);
		}

		function test_WinningsClass_WithNegativeResult_IsNegative(){
			result.setWinnings(-1);

			$sut = getSut();

			assertIdentical('neg-result', $sut.winningsClass);
		}

		function test_ManagerButtonsEnabled_WithoutManager_IsFalse(){
			$sut = getSut();

			assertFalse($sut.managerButtonsEnabled);
		}

		function test_ManagerButtonsEnabled_WithManager_IsTrue(){
			isManager = true;

			$sut = getSut();

			assertTrue($sut.managerButtonsEnabled);
		}

		function test_BuyinUrl_WithManager_IsCorrectType(){
			isManager = true;
			$player = new Player();
			result.setPlayer($player);

			$sut = getSut();

			assertIsA($sut.buyinUrl, 'app\Urls\CashgameBuyinUrlModel');
		}

		function test_ReportUrl_WithManager_IsCorrectType(){
			isManager = true;
			$player = new Player();
			result.setPlayer($player);

			$sut = getSut();

			assertIsA($sut.reportUrl, 'app\Urls\CashgameReportUrlModel');
		}

		function test_CashoutUrl_WithManager_IsCorrectType(){
			isManager = true;
			$player = new Player();
			result.setPlayer($player);

			$sut = getSut();

			assertIsA($sut.cashoutUrl, 'app\Urls\CashgameCashoutUrlModel');
		}

		function test_HasCheckedOut_ResultWithoutCashoutTime_ReturnsFalse(){
			$sut = getSut();

			assertFalse($sut.hasCashedOut);
		}

		function test_HasCheckedOut_ResultWithCashoutTime_ReturnsTrue(){
			result.setCashoutTime(new DateTime());
			$sut = getSut();

			assertTrue($sut.hasCashedOut);
		}

		function getSut(){
			return new StatusItemModel(homegame, cashgame, result, isManager, timer);
		}

		private function setLastCheckpointTime(DateTime $time){
			result.setLastReportTime($time);
		}

	}

}