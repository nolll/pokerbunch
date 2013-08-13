namespace tests\AppTests\Cashgame\Matrix{

	use DateTime;
	use entities\Cashgame;
	use entities\CashgameSuite;
	use entities\CashgameTotalResult;
	use entities\Homegame;
	use tests\UnitTestCase;
	use entities\GameStatus;
	use app\Cashgame\Matrix\TableModel;
	use tests\TestHelper;

	class TableModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;
		private $cashgames;
		/** @var CashgameSuite */
		private $suite;

		function bind(){}

		function setUp(){
			homegame = new Homegame();
			cashgames = getCashgames();
		}

		function test_Suite_IsCorrectType(){
			$sut = getModel();

			assertIsA($sut.suite, 'entities\CashgameSuite');
		}

		function test_Results_IsCorrectLength(){
			$sut = getModel();

			assertEqual(2, count($sut.results));
		}

		function test_Cashgames_IsCorrectLengthAndFirstItemIsCorrectType(){
			$sut = getModel();

			assertEqual(3, count($sut.cashgames));
			assertIsA($sut.cashgames[0], 'entities\Cashgame');
		}

		function test_ShowYear_IsFalse(){
			$sut = getModel();

			assertFalse($sut.showYear);
		}

		function test_ShowYear_SpansMultipleYears_IsTrue(){
			cashgames = getCashgames(2010, 2011);
			$sut = getModel();

			assertTrue($sut.showYear);
		}

		function getModel(){
			$suite = new CashgameSuite();
			$suite.setCashgames(cashgames);
			$totalResult = new CashgameTotalResult();
			$suite.setTotalResults(array($totalResult, $totalResult));
			return new TableModel(homegame, $suite);
		}

		function getCashgames($yearOne = 2010, $yearTwo = 2010){
			$cashgame1 = new Cashgame();
			$cashgame1.setStatus(GameStatus::finished);
			$cashgame1.setStartTime(getTestDate($yearOne));
			$cashgame2 = new Cashgame();
			$cashgame2.setStatus(GameStatus::published);
			$cashgame2.setStartTime(getTestDate($yearOne));
			$cashgame3 = new Cashgame();
			$cashgame3.setStatus(GameStatus::published);
			$cashgame3.setStartTime(getTestDate($yearTwo));
			return array($cashgame1, $cashgame2, $cashgame3);
		}

		function getTestDate($year){
			return new DateTime($year . '-01-01');
		}

	}

}