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
			homegame = new Homegame();
			cashgame = new Cashgame();
			cashgame.setStartTime(new DateTime());
			result = new CashgameResult();
		}

		function test_Name_IsSet(){
			$player = new Player();
			$player.setDisplayName('a');
			result.setPlayer($player);
			$sut = getSut();

			assertIdentical('a', $sut.name);
		}

		function test_PlayerUrl_IsCorrectType(){
			result.setPlayer(new Player());

			$sut = getSut();

			assertIsA($sut.playerUrl, 'app\Urls\CashgameActionUrlModel');
		}

		function test_Buyin_IsSet(){
			result.setBuyin(1);
			$sut = getSut();

			assertIdentical("$1", $sut.buyin);
		}

		function test_Cashout_IsSet(){
			result.setStack(1);
			$sut = getSut();

			assertIdentical("$1", $sut.cashout);
		}

		function test_Winnings_IsSet(){
			result.setWinnings(1);
			$sut = getSut();

			assertIdentical("+$1", $sut.winnings);
		}

		function test_WinningsClass_BuyinEqualToCashout_IsEmpty(){
			result.setBuyin(100);
			result.setStack(100);

			$sut = getSut();

			assertIdentical("", $sut.winningsClass);
		}

		function test_WinningsClass_ResultIsPositive_IsPositive(){
			result.setWinnings(1);

			$sut = getSut();

			assertIdentical("pos-result", $sut.winningsClass);
		}

		function test_WinningsClass_BuyinBiggerThanCashout_IsNegative(){
			result.setWinnings(-1);

			$sut = getSut();

			assertIdentical("neg-result", $sut.winningsClass);
		}

		function test_Winrate_WithDuration_IsSet(){
			result.setPlayedTime(60);
			result.setWinnings(1);

			$sut = getSut();

			assertIdentical("$1/h", $sut.winrate);
		}

		function test_Winrate_WithoutDuration_IsEmpty(){
			$sut = getSut();

			assertIdentical("", $sut.winrate);
		}

		function getSut(){
			return new ResultTableItemModel(homegame, cashgame, result);
		}

	}

}