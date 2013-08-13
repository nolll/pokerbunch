namespace tests\AppTests\Cashgame\Leaderboard{

	use entities\CashgameSuite;
	use entities\Homegame;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\Cashgame\Leaderboard\LeaderboardModel;
	use tests\TestHelper;

	class LeaderboardModelTests extends UnitTestCase {

		function test_ActionLeaderboard_SetsTableModel(){
			$homegame = new Homegame();
			$suite = new CashgameSuite();

			$sut = new LeaderboardModel(new User(), $homegame, $suite);

			$this->assertIsA($sut->tableModel, 'app\Cashgame\Leaderboard\TableModel');
		}

	}

}