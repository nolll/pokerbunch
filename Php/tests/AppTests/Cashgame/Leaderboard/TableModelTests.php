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
			homegame = new Homegame();
		}

		function test_Table_ItemModelsAreSet(){
			suite = new CashgameSuite();
			$totalResult = new CashgameTotalResult();
			$totalResults = array($totalResult, $totalResult);
			suite.setTotalResults($totalResults);

			$sut = getSut();

			assertEqual(2, count($sut.itemModels));
			assertIsA($sut.itemModels[0], 'app\Cashgame\Leaderboard\ItemModel');
		}

		function getSut(){
			return new TableModel(homegame, suite);
		}

	}

}