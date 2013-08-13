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
			homegame = new Homegame();
			result = new CashgameTotalResult();
			$player = new Player();
			$player.setDisplayName('player name');
			result.setPlayer($player);
			rank = 1;
		}

		function test_TableItem_RankIsSet(){
			$sut = getSut();

			assertEqual(1, $sut.rank);
		}

		function test_TableItem_PlayerNameIsSet(){
			$sut = getSut();

			assertEqual("player name", $sut.name);
			assertEqual("player%20name", $sut.urlEncodedName);
		}

		function test_TableItem_TotalResultIsSet(){
			result.setWinnings(1);

			$sut = getSut();

			assertEqual("+$1", $sut.totalResult);
		}

		function test_TableItem_WithPositiveResult_WinningsClassIsSetToPosResult(){
			result.setWinnings(1);
			$sut = getSut();

			assertEqual("pos-result", $sut.resultClass);
		}

		function test_TableItem_WithPositiveResult_WinningsClassIsSetToNegResult(){
			result.setWinnings(-1);
			$sut = getSut();

			assertEqual("neg-result", $sut.resultClass);
		}

		function test_TableItem_WithDuration_DurationIsSet(){
			result.setTimePlayed(60);

			$sut = getSut();

			assertEqual("1h", $sut.gameTime);
		}

		function test_TableItem_WithDuration_WinrateIsSet(){
			result.setWinRate(1);

			$sut = getSut();

			assertEqual("$1/h", $sut.winRate);
		}

		function test_TableItem_PlayerUrlIsSet(){
			$sut = getSut();

			assertIsA($sut.playerUrl, 'app\Urls\PlayerDetailsUrlModel');
		}

		function getSut(){
			return new ItemModel(homegame, result, rank);
		}

	}

}