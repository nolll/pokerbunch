namespace tests\AppTests\Cashgame\Leaderboard{

	use app\Cashgame\Leaderboard\LeaderboardController;
	use entities\CashgameSuite;
	use entities\Homegame;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class LeaderboardControllerTests extends UnitTestCase {

		/** @var LeaderboardController */
		private $sut;
		private $userContext;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			sut = new LeaderboardController(userContext, homegameRepositoryMock, cashgameRepositoryMock);
		}

		function test_ActionLeaderboard_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_leaderboard("homegame1");
		}

		function test_ActionLeaderboard_SetsTableModel(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights(userContext);
			cashgameRepositoryMock.returns('getSuite', new CashgameSuite());

			$viewResult = sut.action_leaderboard("homegame1");

			assertIsA($viewResult.model.tableModel, 'app\Cashgame\Leaderboard\TableModel');
		}

	}

}