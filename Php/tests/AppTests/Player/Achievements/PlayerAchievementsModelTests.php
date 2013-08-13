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

			$this->assertNotNull($sut->playedOneGame);
			$this->assertNotNull($sut->playedTenGames);
			$this->assertNotNull($sut->played50Games);
			$this->assertNotNull($sut->played100Games);
            $this->assertNotNull($sut->played200Games);
			$this->assertNotNull($sut->played500Games);
		}

	}

}