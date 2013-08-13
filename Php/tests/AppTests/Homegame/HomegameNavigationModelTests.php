namespace tests\AppTests\Homegame{

	use entities\Cashgame;
	use tests\UnitTestCase;
	use entities\Homegame;
	use app\Homegame\HomegameNavigationModel;
	use tests\TestHelper;

	class HomegameNavigationModelTests extends UnitTestCase {

		function test_Heading_IsSetToHomegameName(){
			$homegame = new Homegame();
			$homegame.setDisplayName('a');

			$sut = new HomegameNavigationModel($homegame);

			assertIdentical('a', $sut.heading);
		}

		function test_HeadingLink_IsCorrectUrlModel(){
			$homegame = new Homegame();

			$sut = new HomegameNavigationModel($homegame);

			assertIsA($sut.headingLink, 'app\Urls\HomegameDetailsUrlModel');
		}

		function test_CashgameLink_IsCorrectUrlModel(){
			$homegame = new Homegame();

			$sut = new HomegameNavigationModel($homegame);

			assertIsA($sut.cashgameLink, 'app\Urls\CashgameIndexUrlModel');
		}

		function test_PlayerLink_IsCorrectUrlModel(){
			$homegame = new Homegame();

			$sut = new HomegameNavigationModel($homegame);

			assertIsA($sut.playerLink, 'app\Urls\PlayerIndexUrlModel');
		}

		function test_CreateLink_IsCorrectUrlModel(){
			$homegame = new Homegame();

			$sut = new HomegameNavigationModel($homegame);

			assertIsA($sut.createLink, 'app\Urls\CashgameAddUrlModel');
		}

		function test_RunningLink_WithRunningGame_IsCorrectUrlModel(){
			$homegame = new Homegame();

			$sut = new HomegameNavigationModel($homegame);

			assertIsA($sut.runningLink, 'app\Urls\RunningCashgameUrlModel');
		}

		function test_CashgameIsRunning_WithRunningGame_IsTrue(){
			$homegame = new Homegame();
			$cashgame = new Cashgame();

			$sut = new HomegameNavigationModel($homegame, $cashgame);

			assertTrue($sut.cashgameIsRunning);
		}

		function test_CashgameIsRunning_WithoutRunningGame_IsFalse(){
			$homegame = new Homegame();

			$sut = new HomegameNavigationModel($homegame);

			assertFalse($sut.cashgameIsRunning);
		}

	}

}