namespace tests\AppTests\Cashgame\Chart{

	use app\Cashgame\Chart\ChartModel;
	use entities\CashgameSuite;
	use entities\Homegame;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use tests\TestHelper;

	class ChartModelTests extends UnitTestCase {

		private $homegame;
		/** @var CashgameSuite */
		private $suite;
		private $year;

		function setUp(){
			homegame = new Homegame();
			suite = new CashgameSuite();
			year = null;
		}

		function getSut(){
			return new ChartModel(new User(), homegame, suite, year);
		}

		function test_ChartDataUrl_IsSet(){
			$sut = getSut();

			assertIsA($sut.chartDataUrl, 'app\Urls\CashgameChartJsonUrlModel');
		}

	}

}