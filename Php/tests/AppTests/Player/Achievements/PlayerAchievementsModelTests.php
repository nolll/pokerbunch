namespace tests\AppTests\Player\Achievements{

	use entities\Cashgame;
	use entities\Player;
	use tests\UnitTestCase;
	use app\Player\Achievements\PlayerAchievementsModel;
	use tests\TestHelper;

	class PlayerAchievementsModelTests extends UnitTestCase {

		function test_AllAchievements_AreNotNull(){
			$player = new Player();
			$cashgames = array(new Cashgame());
			$sut = new PlayerAchievementsModel($player, $cashgames);

			assertNotNull($sut.playedOneGame);
			assertNotNull($sut.playedTenGames);
			assertNotNull($sut.played50Games);
			assertNotNull($sut.played100Games);
            assertNotNull($sut.played200Games);
			assertNotNull($sut.played500Games);
		}

	}

}