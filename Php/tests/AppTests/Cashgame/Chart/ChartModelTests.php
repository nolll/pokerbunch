<?php
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
			$this->homegame = new Homegame();
			$this->suite = new CashgameSuite();
			$this->year = null;
		}

		function getSut(){
			return new ChartModel(new User(), $this->homegame, $this->suite, $this->year);
		}

		function test_ChartDataUrl_IsSet(){
			$sut = $this->getSut();

			$this->assertIsA($sut->chartDataUrl, 'app\Urls\CashgameChartJsonUrlModel');
		}

	}

}