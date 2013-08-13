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
			$this->homegame = new Homegame();
			$this->result = new CashgameTotalResult();
			$player = new Player();
			$player->setDisplayName('player name');
			$this->result->setPlayer($player);
			$this->rank = 1;
		}

		function test_TableRow_RankIsSet(){
			$sut = $this->getSut();

			$this->assertEqual(1, $sut->rank);
		}

		function test_TableRow_PlayerNameIsSet(){
			$sut = $this->getSut();

			$this->assertEqual("player name", $sut->name);
			$this->assertEqual("player%20name", $sut->urlEncodedName);
		}

		function test_TableRow_TotalResultIsSet(){
			$this->result->setWinnings(1);

			$sut = $this->getSut();

			$this->assertEqual("+$1", $sut->totalResult);
		}

		function test_TableRow_WithPositiveResult_WinningsClassIsSetToPosResult(){
			$this->result->setWinnings(1);
			$sut = $this->getSut();

			$this->assertEqual("pos-result", $sut->resultClass);
		}

		function test_TableRow_WithPositiveResult_WinningsClassIsSetToNegResult(){
			$this->result->setWinnings(-1);
			$sut = $this->getSut();

			$this->assertEqual("neg-result", $sut->resultClass);
		}

		function test_TableRow_PlayerUrlIsSet(){
			$sut = $this->getSut();

			$this->assertIsA($sut->playerUrl, 'app\Urls\PlayerDetailsUrlModel');
		}

		function test_TableRow_CellModelsAreSet(){
			$sut = $this->getSut();

			$this->assertEqual(1, count($sut->cellModels));
			$this->assertIsA($sut->cellModels[0], 'app\Cashgame\Matrix\CellModel');
		}

		function getSut(){
			$cashgames = array(new Cashgame());
			$suite = new CashgameSuite();
			$suite->setCashgames($cashgames);
			return new RowModel($this->homegame, $suite, $this->result, $this->rank);
		}

	}

}