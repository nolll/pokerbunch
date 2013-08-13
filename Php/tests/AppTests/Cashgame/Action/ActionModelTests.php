namespace tests\AppTests\Cashgame\Action{

	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Player;
	use entities\Homegame;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use entities\Role;
	use entities\Checkpoints\ReportCheckpoint;
	use app\Cashgame\Action\ActionModel;
	use DateTime;
	use tests\TestHelper;

	class ActionModelTests extends UnitTestCase {

		private $homegame;
		/** @var Player */
		private $player;
		/** @var Cashgame */
		private $cashgame;
		/** @var CashgameResult */
		private $result;

		function setUp(){
			homegame = new Homegame();
			player = new Player();
			cashgame = new Cashgame();
			result = new CashgameResult();
			result.setPlayer(player);
		}

		function getSut(){
			return new ActionModel(new User(), homegame, cashgame, player, result, Role::$player);
		}

		function test_Heading_IsSet(){
			player.setDisplayName('a');
			cashgame.setStartTime(new DateTime("2010-01-01 01:00:00"));

			$sut = getSut();

			assertEqual($sut.heading, 'Cashgame Jan 1 2010, a');
		}

		function test_Checkpoints_WithOneCheckpoint_HasOneCheckpoint(){
			$timestamp = new DateTime("2010-01-01 01:00:00");
			$stack = 1;
			$checkpoint = new ReportCheckpoint($timestamp, $stack);
			result.setCheckpoints(array($checkpoint));

			$sut = getSut();

			$checkpoints = $sut.checkpoints;
			assertIdentical(count($checkpoints), 1);
		}

		function test_ChartDataUrl_IsSet(){
			$sut = getSut();

			assertIsA($sut.chartDataUrl, 'app\Urls\CashgameActionChartJsonUrlModel');
		}

	}

}