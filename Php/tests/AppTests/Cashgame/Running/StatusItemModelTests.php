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
			$this->homegame = new Homegame();
			$this->cashgame = new Cashgame();
			$this->cashgame->setStartTime(new DateTime());
			$this->result = new CashgameResult();
			$this->timer = new TimerFake();
			$this->isManager = false;
		}

		function test_Name_IsSets(){
			$player = new Player();
			$player->setDisplayName('a');
			$this->result->setPlayer($player);

			$sut = $this->getSut();

			$this->assertIdentical('a', $sut->name);
		}

		function test_PlayerUrl_IsSet(){
			$player = new Player();
			$this->result->setPlayer($player);

			$sut = $this->getSut();

			$this->assertIsA($sut->playerUrl, 'app\Urls\CashgameActionUrlModel');
		}

		function test_Buyin_IsSet(){
			$this->result->setBuyin(1);

			$sut = $this->getSut();

			$this->assertIdentical("$1", $sut->buyin);
		}

		function test_Stack_IsSet(){
			$this->result->setStack(1);

			$sut = $this->getSut();

			$this->assertIdentical("$1", $sut->stack);
		}

		function test_Winnings_IsSet(){
			$this->result->setWinnings(1);

			$sut = $this->getSut();

			$this->assertIdentical("+$1", $sut->winnings);
		}

		function test_Time_IsSetToDifferenceBetweenNowAndLastCheckpoint(){
			$this->setLastCheckpointTime(new DateTime('2010-01-01 01:00:00'));
			$this->timer->setTime(new DateTime('2010-01-01 01:01:00'));
			$sut = $this->getSut();

			$this->assertIdentical("1 minute", $sut->time);
		}

		function test_WinningsClass_BuyinEqualToCashout_IsEmpty(){
			$this->result->setBuyin(100);
			$this->result->setStack(100);

			$sut = $this->getSut();

			$this->assertIdentical("", $sut->winningsClass);
		}

		function test_WinningsClass_WithPositiveResult_IsPositive(){
			$this->result->setWinnings(1);

			$sut = $this->getSut();

			$this->assertIdentical('pos-result', $sut->winningsClass);
		}

		function test_WinningsClass_WithNegativeResult_IsNegative(){
			$this->result->setWinnings(-1);

			$sut = $this->getSut();

			$this->assertIdentical('neg-result', $sut->winningsClass);
		}

		function test_ManagerButtonsEnabled_WithoutManager_IsFalse(){
			$sut = $this->getSut();

			$this->assertFalse($sut->managerButtonsEnabled);
		}

		function test_ManagerButtonsEnabled_WithManager_IsTrue(){
			$this->isManager = true;

			$sut = $this->getSut();

			$this->assertTrue($sut->managerButtonsEnabled);
		}

		function test_BuyinUrl_WithManager_IsCorrectType(){
			$this->isManager = true;
			$player = new Player();
			$this->result->setPlayer($player);

			$sut = $this->getSut();

			$this->assertIsA($sut->buyinUrl, 'app\Urls\CashgameBuyinUrlModel');
		}

		function test_ReportUrl_WithManager_IsCorrectType(){
			$this->isManager = true;
			$player = new Player();
			$this->result->setPlayer($player);

			$sut = $this->getSut();

			$this->assertIsA($sut->reportUrl, 'app\Urls\CashgameReportUrlModel');
		}

		function test_CashoutUrl_WithManager_IsCorrectType(){
			$this->isManager = true;
			$player = new Player();
			$this->result->setPlayer($player);

			$sut = $this->getSut();

			$this->assertIsA($sut->cashoutUrl, 'app\Urls\CashgameCashoutUrlModel');
		}

		function test_HasCheckedOut_ResultWithoutCashoutTime_ReturnsFalse(){
			$sut = $this->getSut();

			$this->assertFalse($sut->hasCashedOut);
		}

		function test_HasCheckedOut_ResultWithCashoutTime_ReturnsTrue(){
			$this->result->setCashoutTime(new DateTime());
			$sut = $this->getSut();

			$this->assertTrue($sut->hasCashedOut);
		}

		function getSut(){
			return new StatusItemModel($this->homegame, $this->cashgame, $this->result, $this->isManager, $this->timer);
		}

		private function setLastCheckpointTime(DateTime $time){
			$this->result->setLastReportTime($time);
		}

	}

}