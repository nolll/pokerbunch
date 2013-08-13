<?php
namespace tests\AppTests\Cashgame\Leaderboard{

	use entities\CashgameTotalResult;
	use entities\Player;
	use entities\Homegame;
	use tests\UnitTestCase;
	use app\Cashgame\Leaderboard\ItemModel;
	use tests\TestHelper;

	class ItemModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;
		/** @var CashgameTotalResult */
		private $result;
		private $rank;

		function setUp(){
			$this->homegame = new Homegame();
			$this->result = new CashgameTotalResult();
			$player = new Player();
			$player->setDisplayName('player name');
			$this->result->setPlayer($player);
			$this->rank = 1;
		}

		function test_TableItem_RankIsSet(){
			$sut = $this->getSut();

			$this->assertEqual(1, $sut->rank);
		}

		function test_TableItem_PlayerNameIsSet(){
			$sut = $this->getSut();

			$this->assertEqual("player name", $sut->name);
			$this->assertEqual("player%20name", $sut->urlEncodedName);
		}

		function test_TableItem_TotalResultIsSet(){
			$this->result->setWinnings(1);

			$sut = $this->getSut();

			$this->assertEqual("+$1", $sut->totalResult);
		}

		function test_TableItem_WithPositiveResult_WinningsClassIsSetToPosResult(){
			$this->result->setWinnings(1);
			$sut = $this->getSut();

			$this->assertEqual("pos-result", $sut->resultClass);
		}

		function test_TableItem_WithPositiveResult_WinningsClassIsSetToNegResult(){
			$this->result->setWinnings(-1);
			$sut = $this->getSut();

			$this->assertEqual("neg-result", $sut->resultClass);
		}

		function test_TableItem_WithDuration_DurationIsSet(){
			$this->result->setTimePlayed(60);

			$sut = $this->getSut();

			$this->assertEqual("1h", $sut->gameTime);
		}

		function test_TableItem_WithDuration_WinrateIsSet(){
			$this->result->setWinRate(1);

			$sut = $this->getSut();

			$this->assertEqual("$1/h", $sut->winRate);
		}

		function test_TableItem_PlayerUrlIsSet(){
			$sut = $this->getSut();

			$this->assertIsA($sut->playerUrl, 'app\Urls\PlayerDetailsUrlModel');
		}

		function getSut(){
			return new ItemModel($this->homegame, $this->result, $this->rank);
		}

	}

}