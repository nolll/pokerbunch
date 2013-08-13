namespace tests\AppTests\Cashgame\Leaderboard{

	use entities\CashgameSuite;
	use entities\CashgameTotalResult;
	use entities\Homegame;
	use tests\UnitTestCase;
	use app\Cashgame\Leaderboard\TableModel;
	use tests\TestHelper;

	class TableModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;
		private $suite;

		function setUp(){
			$this->homegame = new Homegame();
		}

		function test_Table_ItemModelsAreSet(){
			$this->suite = new CashgameSuite();
			$totalResult = new CashgameTotalResult();
			$totalResults = array($totalResult, $totalResult);
			$this->suite->setTotalResults($totalResults);

			$sut = $this->getSut();

			$this->assertEqual(2, count($sut->itemModels));
			$this->assertIsA($sut->itemModels[0], 'app\Cashgame\Leaderboard\ItemModel');
		}

		function getSut(){
			return new TableModel($this->homegame, $this->suite);
		}

	}

}