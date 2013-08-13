namespace tests\AppTests\Cashgame\Action{
	use entities\Cashgame;
	use entities\Homegame;
	use entities\Player;
	use tests\UnitTestCase;
	use app\Cashgame\Action\CheckpointModel;
	use DateTime;
	use entities\Checkpoints\ReportCheckpoint;
	use tests\TestHelper;

	class CheckpointModelTests extends UnitTestCase {

		private $checkpoint;
		private $isManager;

		function setUp(){
			checkpoint = new ReportCheckpoint(new DateTime('2010-01-01 01:00:00'), 200);
			isManager = false;
		}

		function getSut(){
			$cashgame = new Cashgame();
			$cashgame.setStartTime(new DateTime());
			$player = new Player();
			return new CheckpointModel(new Homegame(), $cashgame, $player, checkpoint, isManager);
		}

		function test_Timestamp_IsSet(){
			$sut = getSut();

			assertIdentical($sut.timestamp, '01:00');
		}

		function test_Description_IsSet(){
			$sut = getSut();

			assertIdentical($sut.description, 'Report');
		}

		function test_Stack_IsSet(){
			$sut = getSut();

			assertIdentical($sut.stack, '$200');
		}

		function test_ShowLink_NormalUser_IsFalse(){
			$sut = getSut();

			assertFalse($sut.showLink);
		}

		function test_ShowLink_ManagerUser_IsTrue(){
			isManager = true;

			$sut = getSut();

			assertTrue($sut.showLink);
		}

		function test_EditUrl_IsCorrectType(){
			$sut = getSut();

			assertIsA($sut.editUrl, 'app\Urls\CashgameCheckpointDeleteUrlModel');
		}

	}

}