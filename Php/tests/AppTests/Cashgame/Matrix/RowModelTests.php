namespace tests\AppTests\Cashgame\Matrix{

	use entities\Cashgame;
	use entities\CashgameSuite;
	use entities\CashgameTotalResult;
	use entities\Player;
	use entities\Homegame;
	use tests\UnitTestCase;
	use app\Cashgame\Matrix\RowModel;
	use tests\TestHelper;

	class RowModelTests extends UnitTestCase {

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

		function test_TableRow_RankIsSet(){
			$sut = getSut();

			assertEqual(1, $sut.rank);
		}

		function test_TableRow_PlayerNameIsSet(){
			$sut = getSut();

			assertEqual("player name", $sut.name);
			assertEqual("player%20name", $sut.urlEncodedName);
		}

		function test_TableRow_TotalResultIsSet(){
			result.setWinnings(1);

			$sut = getSut();

			assertEqual("+$1", $sut.totalResult);
		}

		function test_TableRow_WithPositiveResult_WinningsClassIsSetToPosResult(){
			result.setWinnings(1);
			$sut = getSut();

			assertEqual("pos-result", $sut.resultClass);
		}

		function test_TableRow_WithPositiveResult_WinningsClassIsSetToNegResult(){
			result.setWinnings(-1);
			$sut = getSut();

			assertEqual("neg-result", $sut.resultClass);
		}

		function test_TableRow_PlayerUrlIsSet(){
			$sut = getSut();

			assertIsA($sut.playerUrl, 'app\Urls\PlayerDetailsUrlModel');
		}

		function test_TableRow_CellModelsAreSet(){
			$sut = getSut();

			assertEqual(1, count($sut.cellModels));
			assertIsA($sut.cellModels[0], 'app\Cashgame\Matrix\CellModel');
		}

		function getSut(){
			$cashgames = array(new Cashgame());
			$suite = new CashgameSuite();
			$suite.setCashgames($cashgames);
			return new RowModel(homegame, $suite, result, rank);
		}

	}

}