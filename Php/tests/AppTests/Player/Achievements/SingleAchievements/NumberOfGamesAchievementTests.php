<?php
namespace tests\AppTests\Player\Achievements\SingleAchievements{

	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Player;
	use tests\SharbatUnitTestCase;
	use app\Player\Achievements\SingleAchievements\NumberOfGamesAchievement;
	use tests\TestHelper;

	class NumberOfGamesAchievementTests extends SharbatUnitTestCase {

		function test_Earned_CheckOneWithNullCashgames_ReturnsFalse(){
			$player = new Player();
			$cashgames = null;
			$numberToCheck = 1;
			$sut = new NumberOfGamesAchievement($player, $cashgames, $numberToCheck);

			$result = $sut->earned();

			$this->assertFalse($result);
		}

		function test_Earned_CheckOneWithNoCashgames_ReturnsFalse(){
			$player = new Player();
			$cashgames = array();
			$numberToCheck = 1;
			$sut = new NumberOfGamesAchievement($player, $cashgames, $numberToCheck);

			$result = $sut->earned();

			$this->assertFalse($result);
		}

		function test_Earned_CheckOneWithOneCashgameMatchingPlayer_ReturnsTrue(){
			$player = new Player();
			$player->setId(1);
			$cashgameResult = new CashgameResult();
			$cashgameResult->setPlayer($player);
			$cashgame = new Cashgame();
			$cashgame->setResults(array($cashgameResult));
			$cashgames = array($cashgame);
			$numberToCheck = 1;
			$sut = new NumberOfGamesAchievement($player, $cashgames, $numberToCheck);

			$result = $sut->earned();

			$this->assertTrue($result);
		}

		function test_Earned_CheckTenWithOneCashgameMatchingPlayer_ReturnsFalse(){
			$player = new Player();
			$player->setId(1);
			$cashgameResult = new CashgameResult();
			$cashgameResult->setPlayer($player);
			$cashgame = new Cashgame();
			$cashgame->setResults(array($cashgameResult));
			$cashgames = array($cashgame);
			$numberToCheck = 10;
			$sut = new NumberOfGamesAchievement($player, $cashgames, $numberToCheck);

			$result = $sut->earned();

			$this->assertFalse($result);
		}

		function test_Earned_CheckOneWithOneCashgameNotMatchingPlayer_ReturnsFalse(){
			$player = new Player();
			$player->setId(1);
			$cashgameResult = new CashgameResult();
			$cashgameResult->setPlayer($player);
			$cashgame = new Cashgame();
			$cashgame->setResults(array($cashgameResult));
			$cashgames = array($cashgame);
			$player = new Player();
			$numberToCheck = 1;
			$sut = new NumberOfGamesAchievement($player, $cashgames, $numberToCheck);

			$result = $sut->earned();

			$this->assertFalse($result);
		}

	}

}